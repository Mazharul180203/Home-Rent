using Data.DBContexts;
using Data.Dtos;
using Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;

namespace API.Controllers;

[Authorize(Roles = "owner,admin")]
[Route("api/[controller]")]
[ApiController]
public class LandLordController : ControllerBase
{
     private readonly AppDBContext _context;
     private readonly ILandLordService _service;

     public LandLordController(AppDBContext context, ILandLordService service)
     {
          _context = context;
          _service = service;
     }

     [HttpPost("properties")]

     public async Task<IActionResult> CreateProperties(propetiesDto data)
     {
          try
          {
               var UserIDClaims = User.FindFirst("UserId")?.Value;
               if (string.IsNullOrEmpty(UserIDClaims) || !int.TryParse(UserIDClaims, out var userId))
               {
                    return Unauthorized("Invalid User");
               }

               return await getResponse(_service.CreatePropertiesService(data, UserIDClaims));


          }
          catch (Exception ex)
          {
               return BadRequest(new { Status = "Error", Message = ex.Message });
          }
     }

     [HttpGet("properties/{id}")]
     public async Task<IActionResult> GetProperties(long id)
     {
          try
          {
               return await getResponse(_service.GetPropertyService(id));
          }
          catch (Exception ex)
          {
               return BadRequest(new { Status = "Error", Message = ex.Message });
          }
     }
     
}