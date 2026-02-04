using Data.DBContexts;
using Data.Dtos;
using Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;

namespace API.Controllers;

[Authorize(Roles = "Admin, Owner")]
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
               
               CommonResponseDto response = await _service.CreatePropertiesService(data, UserIDClaims);
               return await getResponse(response.Data, response.Status, response.Message);
          }
          catch (Exception ex)
          {
               return await getResponse("","fail",  ex.Message );
          }
     }

     [HttpGet("properties/{id}")]
     public async Task<IActionResult> GetProperties(long id)
     {
          try
          {
               CommonResponseDto response = await _service.GetPropertyService(id);
               return await getResponse(response.Data, response.Status, response.Message);
          }
          catch (Exception ex)
          {
               return BadRequest(new { Status = "Error", Message = ex.Message });
          }
     }
     
}