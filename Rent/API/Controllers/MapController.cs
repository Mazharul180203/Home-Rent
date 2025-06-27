using Data.DBContexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace API.Controllers;
[Authorize]
[Route("api/[controller]")]
[ApiController]

public class MapController : ControllerBase
{
    private readonly AppDBContext _context;
    private readonly IMapService _service;
    
    public MapController(AppDBContext context, IMapService service)
    {
        _context = context;
        _service = service;
    }
    
    [HttpGet("get/coordinates")]
    public async Task<ActionResult> GetCoordinates([FromQuery] string address)
    {
        try
        {
            var coordinates = await _service.GetCoordinate(address);
            return await getResponse(coordinates, "Coordinates retrieved successfully");
        }
        catch (Exception ex)
        {
            return BadRequest(new { Status = "Error", Message = ex.Message });
        }
    }
    
    [HttpGet("get/distace/time")]
    
    public async Task<ActionResult> GetDistanceAndTime([FromQuery] string origin, [FromQuery] string destination)
    {
        try
        {
            var result = await _service.GetDistanceAndTime(origin, destination);
            return await getResponse(result, "Distance and time retrieved successfully");
        }
        catch (Exception ex)
        {
            return BadRequest(new { Status = "Error", Message = ex.Message });
        }
    }
}