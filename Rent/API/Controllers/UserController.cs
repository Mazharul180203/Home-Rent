using Data.DBContexts;
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
            var contacts = await _service.GetUserContacts();
            return Ok(new { Status = "Success", Data = contacts });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Status = "Error", Message = ex.Message });
        }
    }
}