using Data.Dtos;

namespace Services.Interfaces;

public interface ILandLordService
{
   public Task<object> createProperties(propetiesDto data, string UserIDClaims);
}