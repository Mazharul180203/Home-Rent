using Data.Dtos;
using Data.Models;


namespace Services.Interfaces;

public interface ILandLordService
{
   Task<CommonResponseDto> CreatePropertiesService(propetiesDto data, string useridClaims);
   Task<CommonResponseDto> GetPropertyService(long id);
   Task<CommonResponseDto>CreateUnitsService(long UserID, unitDto data);
   Task<CommonResponseDto> UpdatePhotoService(long UserID, long unitID, long photoID);
}