using Data.DBContexts;
using Data.Dtos;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Services.Interfaces;

namespace Services.Implementations;

public class AuthService : IAuthService
{
    private readonly AppDBContext _context;
    
    public AuthService(AppDBContext context, IConfiguration configuration)
    {
        _context = context;
    }

    public async Task<object> AddCreateRequest(Register data)
    {
        try
        {
            if (await _context.Registers.AnyAsync(u => u.Email == data.Email))
                return "user already exists";

            var userRegister = new Register
            {
                Username = data.Username,
                Email = data.Email,
                Phone = data.Phone,
                Password_Hash = BCrypt.Net.BCrypt.HashPassword(data.Password_Hash),
                Role = data.Role ?? "user",
                Is_Active = true,
                Created_At = DateTime.Now,
                Updated_At = DateTime.Now
            };

            await _context.Registers.AddAsync(userRegister);
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

           if (isPhone)
           {
               var userDetails = await _context.Registers.AsNoTracking().FirstOrDefaultAsync(u => u.Phone == data.UserName);
               if (userDetails == null || !BCrypt.Net.BCrypt.Verify(data.Password, userDetails.Password_Hash))
               {
                   return "Invalid phone number or password";
               }
               else
               {
                   
               }
           }

           if (isEmail)
           {
               var userDetails = await _context.Registers.AsNoTracking().FirstOrDefaultAsync(u=> u.Email == data.UserName);
           }
           
            return "Login successful"; 
        }
        catch (Exception e)
        {
            return $"An error occurred: {e.Message}";
        }
    }

}