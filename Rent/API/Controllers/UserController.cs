using Data.Dtos;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace API.Controllers;

public class UserController : ControllerBase
{
    private readonly IUserService _service;

    public UserController(IUserService service)
    {
        _service = service;
    }
    
    [HttpPost("update/profile")]

    public async Task<IActionResult> UpdateProfile(ProfileUpdateDto data)
    {
        try
        {
            var UserIDClaims = User.FindFirst("UserId")?.Value;
            if (string.IsNullOrEmpty(UserIDClaims) || !int.TryParse(UserIDClaims, out var userId))
            {
                return await getResponse("","fail",  "Invalid User ID"); 
            }
            long UserID = long.Parse(UserIDClaims);
            CommonResponseDto response = await _service.UpdateProfile(UserID,data);
            return await getResponse(response.Data, response.Status, response.Message);
            
        }
        catch (Exception ex)
        {
            return await getResponse("", "fail", ex.Message);
        }
    }
}