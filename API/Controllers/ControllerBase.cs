using Data.Dtos;
using Microsoft.AspNetCore.Mvc;
using Utility;

public class ControllerBase : Microsoft.AspNetCore.Mvc.ControllerBase
{
    protected async Task<ActionResult> getResponse(object data, string status, string Message)
    {
        CommonResponseDto response = new CommonResponseDto();
        response.Status = status;
        response.Message = Message;
        response.Data = data;
        
        return Content(new JSONSerialize().getJSONSFromObject(response,true), "application/json");
    }
}