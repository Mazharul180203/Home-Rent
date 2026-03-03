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
     private readonly ILandLordService _service;

     public LandLordController(ILandLordService service)
     {
          _service = service;
     }

     [HttpPost("create/properties")]

     public async Task<IActionResult> CreateProperties(propetiesDto data)
     {
          try
          {
               var UserIDClaims = User.FindFirst("UserId")?.Value;
               if (string.IsNullOrEmpty(UserIDClaims) || !int.TryParse(UserIDClaims, out var userId))
               {
                    return await getResponse("", "fail", "Invalid UserID");
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
               return await getResponse("", "fail", ex.Message);
          }
     }

     [HttpPost("create/units")]

     public async Task<IActionResult> CreateUnits([FromBody]unitDto data)
     {
          try
          {
               var userIdClaims = User.FindFirst("UserId")?.Value;
               if (string.IsNullOrEmpty(userIdClaims) || !int.TryParse(userIdClaims, out var userId))
               {
                    return await getResponse("", "fail", "Invalid UserID");
               }
               
               double UserID = double.Parse(userIdClaims);
               CommonResponseDto response = await _service.CreateUnitsService(UserID,data);
               return await getResponse(response.Data, response.Status, response.Message);
          }
          catch (Exception e)
          {
               return await getResponse("","fail",  e.Message );
          }
     }
     
}