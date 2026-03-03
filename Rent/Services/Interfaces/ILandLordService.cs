using Data.Dtos;
using Data.Models;


namespace Services.Interfaces;

public interface ILandLordService
{
   Task<CommonResponseDto> CreatePropertiesService(propetiesDto data, string useridClaims);
   Task<CommonResponseDto> GetPropertyService(long id);
   Task<CommonResponseDto>CreateUnitsService(double UserID, unitDto data);
}