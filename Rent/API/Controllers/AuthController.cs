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


    [HttpPost("auth/Register")]

    public async Task<ActionResult> CreateRegister(UserRegistrationDto data)
    {
        try
        {
            return await getResponse(await _service.AddCreateRequest(data));
        }
        catch (Exception ex)
        {
            return BadRequest(new { Status = "Error", Message = ex.Message });
        }
    }

    [HttpPost("auth/DoLogin")]

    public async Task<ActionResult> DoLogin(LoginDto data)
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
}