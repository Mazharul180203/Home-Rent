using System.Diagnostics;
using Data.DBContexts;
using Data.Models;
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


    [HttpGet("create/register")]

    public async Task<ActionResult> CreateRegister(Register data)
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
}