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
using Microsoft.AspNetCore.Mvc;

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

    public async Task<string> AddCreateRequest(UserRegistrationDto data)
    {
        try
        {
            return "dfa";
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

           return "dfd";
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
            new Claim("UserId", loginUser[0].id.ToString()),
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