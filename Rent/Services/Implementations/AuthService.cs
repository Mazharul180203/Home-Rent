using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Data.DBContexts;
using Data.Dtos;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Services.Interfaces;
using System.Text;

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

    public async Task<object> AddCreateRequest(UserRegistrationDto data)
    {
        try
        {
            if (await _context.Users.AnyAsync(u => u.Email == data.Email))
                return "user already exists";

            var userRegister = new User()
            {
                UserName = data.UserName,
                FirstName = data.FirstName,
                LastName = data.LastName,
                Email = data.Email,
                Phone = data.Phone,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(data.PasswordHash),
                UserType = data.UserType ?? "user",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            await _context.Users.AddAsync(userRegister);
            await _context.SaveChangesAsync();

            return "user created successfully";
        }
        catch (Exception e)
        {
            return $"An error occurred: {e.Message}";
        }
    }

    public async Task<object> DoLoginRequest(LoginDto data)
    {
        try
        {
           bool isEmail = System.Text.RegularExpressions.Regex.IsMatch(data.UserName, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
           bool isPhone = System.Text.RegularExpressions.Regex.IsMatch(data.UserName, @"^\+?\d{10,15}$");

           var userDetails = isPhone
               ? await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Phone == data.UserName)
               : isEmail
                   ? await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Email == data.UserName)
                   : null;
           
           if (userDetails == null || !BCrypt.Net.BCrypt.Verify(data.Password, userDetails.PasswordHash))
           {
               return "Invalid credentials";
           }
           
           await _context.LoginUsers.AddAsync(new LoginUser { UserName = userDetails.UserName, entryTime = DateTime.Now });
           await _context.SaveChangesAsync();
           List<LoginUserDto> loginUser = new List<LoginUserDto>();
           
           loginUser.Add(new LoginUserDto
           {
               userName = userDetails.UserName,
               createdAt = DateTime.Now,
           });
           return new
           {
               Token = GenerateWebToken(loginUser),
           };
           
        }
        catch (Exception e)
        {
            return $"An error occurred: {e.Message}";
        }
    }
    
    
    private string GenerateWebToken(List<LoginUserDto> loginUser)
    {
        string key = _configuration["Jwt:Key"].ToString();
        string issuer = _configuration["Jwt:Issuer"].ToString();
        string audience = _configuration["Jwt:Audience"].ToString();
        string subject = _configuration["Jwt:Subject"].ToString();
        
        
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Sub, subject),
            new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Iat,
                ((DateTimeOffset)DateTime.UtcNow).ToUnixTimeSeconds().ToString()),
            new Claim("UserName", loginUser[0].userName),
            new Claim("CreatedAt", loginUser[0].createdAt.ToString("o"))
        };
        
        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: credentials
        );
          
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

}