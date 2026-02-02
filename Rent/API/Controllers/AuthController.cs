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
            CommonResponseDto response = await _service.CreateRegister(data);
            return await getResponse(response.Data, response.Status, response.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(getResponse( null,"fail", "ex.Message" ));
        }
    }

    [HttpPost("doLogin")]

    public async Task<IActionResult> DoLogin(LoginDto data)
    {
        try
        {
            LoginResponseDto response = await _service.DoLoginRequest(data);
            return await getResponse(response, response.Status, response.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(getResponse( null,"fail", "ex.Message" ));
        }
    }

    [HttpPost("refreshToken")]

    public async Task<object> RefreshToken(string refreshToken)
    {
       return  await _service.RefreshToken(refreshToken);
    }
}