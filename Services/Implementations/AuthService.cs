using System.Collections.Immutable;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Data.DBContexts;
using Data.Dtos;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Services.Interfaces;
using System.Text;
using Microsoft.AspNetCore.Http.Metadata;


namespace Services.Implementations;

public class AuthService : IAuthService
{
    private readonly AppDBContext _context;
    private readonly IConfiguration _configuration;
    
    public AuthService(AppDBContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public async Task<CommonResponseDto> CreateRegister(UserRegistrationDto data)
    {
        try
        {
            var userName = await _context.users.FirstOrDefaultAsync(x => x.username == data.username);
            
            if (userName != null)
            {
               return new CommonResponseDto
               {
                   Status = "fail",
                   Message = "Username already exists",
                   Data = null
               };
            }
            
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(data.password);

            var userData = new user
            {
                username = data.username,
                password_hash = passwordHash,
                email = data.email,
                role = data.role,
                nid = data.nid,
                address = data.address,
                created_at = DateTime.UtcNow,
                updated_at = DateTime.UtcNow
            };

            _context.users.AddAsync(userData);
            await _context.SaveChangesAsync();
            return new CommonResponseDto
            {
                Status = "success",
                Message = "User registered successfully",
                Data = null
            };
        }
        catch (Exception e)
        {
            return new CommonResponseDto
            {
                Status = "error",
                Message = $"An error occurred: {e.Message}",
                Data = null
            };
        }
    }

    public async Task<LoginResponseDto> DoLoginRequest(LoginDto data)
    {
        try
        {
           
           if(string.IsNullOrEmpty(data.username) || string.IsNullOrEmpty(data.password_hash))
               return new LoginResponseDto
               {
                   Status = "fail",
                   Message = "Username or password cannot be empty",
                   Tokens = null
               };
           user useDetails = null;

           useDetails = await _context.users.FirstOrDefaultAsync(u => u.username == data.username);
           
           bool isVarified = BCrypt.Net.BCrypt.Verify(data.password_hash, useDetails.password_hash);

           if (!isVarified)
           {
               return new LoginResponseDto
               {
                   Status = "fail",
                   Message = "Invalid credentials",
                   Tokens = null
               };
           }
           
           var userInfo = new UserInfoDto
           {
               id = useDetails.id,
               role = useDetails.role,
           };

           var accesstoken = GenerateWebToken(userInfo);
           
           var refreshTokenEntity = new RefreshToken();
           string rawRefreshToken = GenerateRefreshToken(userInfo, out refreshTokenEntity);
           
           _context.RefreshTokens.Add(refreshTokenEntity);
           await _context.SaveChangesAsync();
           return new LoginResponseDto
           {
               Status = "Success",
               Message = "Login successfully",
              Tokens = new List<TokenDto>
              {
                    new TokenDto
                    {
                        AccessToken = accesstoken,
                        RefreshToken = rawRefreshToken
                    }
              }
               
           };

        }
        catch (Exception e)
        {
            return new LoginResponseDto
            {
                Status = "error",
                Message = $"An error occurred: {e.Message}",
                Tokens = null
            };
        }
    }
    
    
    private string GenerateWebToken(UserInfoDto tokenDataList)
    {
        string key = _configuration["Jwt:Key"].ToString();
        string issuer = _configuration["Jwt:Issuer"].ToString();
        string audience = _configuration["Jwt:Audience"].ToString();
        string subject = _configuration["Jwt:Subject"].ToString();
        
        
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new []
        {
            new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Sub, subject),
            new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Iat,
                ((DateTimeOffset)DateTime.UtcNow).ToUnixTimeSeconds().ToString()),
            new Claim("UserId", tokenDataList.id.ToString()),
            new Claim("role", tokenDataList.role)
           
            
        };
        
        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(2),
            signingCredentials: credentials
        );
          
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    

    private string GenerateRefreshToken(UserInfoDto tokenDataList, out RefreshToken tokenEntity)
    {
        
        byte[] randomBytes = RandomNumberGenerator.GetBytes(64);
        string rawToken = Convert.ToBase64String(randomBytes);

        tokenEntity = new RefreshToken
        {
            UserId = tokenDataList.id,
            TokenHash = HashToken(rawToken),
            ExpiresAt = DateTime.UtcNow.AddDays(7),
            CreatedAt = DateTime.UtcNow,

        };
        return rawToken;
    }

    private string HashToken(string rawToken)
    {
        using var sha256Hash = SHA256.Create();
        var hashBytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawToken));
        return Convert.ToBase64String(hashBytes);
    }


    public async Task<object> RefreshToken(string refreshToken)
    {
        if(string.IsNullOrEmpty(refreshToken))
            throw new Exception("refreshToken is empty");
        
        var hashedToken = HashToken(refreshToken);

        var existingToken = await _context.RefreshTokens
            .Include(t => t.User)
            .SingleOrDefaultAsync(t => t.TokenHash == hashedToken);
        
        if(existingToken == null) throw new Exception("RefreshToken not found");

        if (existingToken.ExpiresAt <= DateTime.UtcNow)
        {
            throw new Exception("RefreshToken is expired");
        }

        if (existingToken.RevokedAt != null)
        {
            if (existingToken.ReplacedByTokenHash != null)
            {
                // Suspicious: Reuse of a rotated token → Possible theft!
                // Revoke the entire chain
                await RevokeTokenChainAsync(existingToken.ReplacedByTokenHash);
                // Optional: Log/alert, force user password reset, etc.
            }
            throw new Exception("Refresh token already revoked");
        }
        var userInfo = new UserInfoDto
        {
            id = existingToken.UserId,
            role = existingToken.User.role,
        };
        
        var refreshTokenEntity = new RefreshToken();
        string rawRefreshToken = GenerateRefreshToken(userInfo, out refreshTokenEntity);
        existingToken.RevokedAt = DateTime.UtcNow;
        existingToken.ReplacedByTokenHash = refreshTokenEntity.TokenHash;
        
        await _context.RefreshTokens.AddAsync(refreshTokenEntity);
        await _context.SaveChangesAsync();
        
        var newAccessToken = GenerateWebToken(userInfo);
        
        return new
        {
            AccessToken = newAccessToken,
            RefreshToken = rawRefreshToken,
        };
        
    }
    private async Task RevokeTokenChainAsync(string? startingHash)
    {
        while (!string.IsNullOrEmpty(startingHash))
        {
            var token = await _context.RefreshTokens
                .FirstOrDefaultAsync(t => t.TokenHash == startingHash);

            if (token == null) break;

            token.RevokedAt = DateTime.UtcNow;
            startingHash = token.ReplacedByTokenHash;  
        }
        await _context.SaveChangesAsync();
    }

}