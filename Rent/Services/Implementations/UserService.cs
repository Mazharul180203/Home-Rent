using Data.DBContexts;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;

namespace Services.Implementations;

public class UserService : IUserService
{
    private readonly AppDBContext _context;
    
    public UserService(AppDBContext context)
    {
        _context = context;
    }

    public async Task<object> GetUserContacts(string userId)
    {
        try
        {
            int intUsedId = int.Parse(userId);
            var userData = await _context.Users.FindAsync(intUsedId);
            var usetDetails = await _context.Users
                .Where(ud => ud.Id == userData.Id)
                .Select(ud => new
                {
                    ud.Id,
                    ud.FirstName,
                    ud.LastName,
                    ud.Email,
                    
                })
                .ToListAsync();
            return usetDetails;
        }
        catch (Exception e)
        {
            throw new Exception($"An error occurred while fetching distance and time: {e.Message}");
        }
    }

}