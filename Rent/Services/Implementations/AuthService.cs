using Data.DBContexts;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;

namespace Services.Implementations;

public class AuthService : IAuthService
{
    private readonly AppDBContext _context;

    public async Task<object> AddCreateRequest(Register data)
    {
        try
        {
            var userExists = await _context.Registers.AnyAsync(u => u.Email == data.Email);
            if (userExists)
            {
                return "user already exists";
            }

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(data.Password_Hash);
            
            var userRegister = new Register
            {
                Username = data.Username,
                Email = data.Email,
                Password_Hash = passwordHash,
                Role = data.Role ?? "user",
                Is_Active = true,
                Created_At = DateTime.Now,
                Updated_At = DateTime.Now
            };
            _context.Registers.Add(userRegister);
            await _context.SaveChangesAsync();
            return "user created successfully";
        }
        catch (Exception e)
        {
            return $"An error occurred: {e.Message}";
        }
    }
}