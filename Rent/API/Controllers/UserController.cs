using Data.DBContexts;
using Data.Dtos;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
    
public class UserController : ControllerBase
{
    private readonly AppDBContext _context;
    private readonly IUserService _service;
    
    public UserController(AppDBContext context, IUserService service)
    {
        _context = context;
        _service = service;
    }
    
    
    [HttpGet("get/user/contacts")]
    public async Task<IActionResult> GetUserContacts()
    {
        try
        {
            var userIdClaims = User.FindFirst("UserId")?.Value;
            if (string.IsNullOrEmpty(userIdClaims) || !int.TryParse(userIdClaims, out var userId))
            {
                return Unauthorized("Invalid User");
            }
            return await getResponse(await _service.GetUserContacts(userIdClaims));
        }
        catch (Exception ex)
        {
            return BadRequest(new { Status = "Error", Message = ex.Message });
        }
    }
    
    public async Task<IActionResult> AddUserContacts(UserContactDto data)
    {
        try
        {
            var userIdClaims = User.FindFirst("UserId")?.Value;
            if (string.IsNullOrEmpty(userIdClaims) || !int.TryParse(userIdClaims, out var userId))
            {
                return Unauthorized("Invalid User");
            }
            return await getResponse(await _service.AddUserContacts(data, userIdClaims));
        }
        catch (Exception ex)
        {
            return BadRequest(new { Status = "Error", Message = ex.Message });
        }
    }
}