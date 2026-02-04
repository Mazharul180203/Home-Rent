using Data.Dtos;
using Data.Models;


namespace Services.Interfaces;

public interface ILandLordService
{
   public Task<CommonResponseDto> CreatePropertiesService(propetiesDto data, string useridClaims);
   public Task<CommonResponseDto> GetPropertyService(long id);
}