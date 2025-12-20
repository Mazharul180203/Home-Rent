using Data.Dtos;
using Data.Models;


namespace Services.Interfaces;

public interface ILandLordService
{
   public Task<object> CreatePropertiesService(propetiesDto data, string useridClaims);
   public Task<object> GetPropertyService(long id);
}