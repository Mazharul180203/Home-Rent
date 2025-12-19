using System.Diagnostics;
using Data.DBContexts;
using Data.Dtos;
using Data.Models;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly AppDBContext _context;
    private readonly IAuthService _service;
    
    public AuthController(AppDBContext context, IAuthService service)
    {
        _context = context;
        _service = service;
    }


    [HttpPost("register")]

    public async Task<IActionResult> CreateRegister(UserRegistrationDto data)
    {
        try
        {
            return await getResponse(await _service.CreateRegister(data),"Registered Successfully");
        }
        catch (Exception ex)
        {
            return BadRequest(new { Status = "Error", Message = ex.Message });
        }
    }

    [HttpPost("doLogin")]

    public async Task<IActionResult> DoLogin(LoginDto data)
    {
        try
        {
            var response = await _service.DoLoginRequest(data);
            return response == "Invalid credentials" ? 
                await getResponse(response,"Invalid credentials") 
                : await getResponse(response, "Login successful");
        }
        catch (Exception ex)
        {
            return BadRequest(new { Status = "Error", Message = ex.Message });
        }
    }

    [HttpPost("refreshToken")]

    public async Task<object> RefreshToken(string refreshToken)
    {
       return  await _service.RefreshToken(refreshToken);
    }
}