using Data.Dtos;

namespace Services.Interfaces;

public interface IMapService
{
    public Task<CommonResponseDto> GetCoordinate(string address);
    public Task<CommonResponseDto>GetDistanceAndTime(string origin, string destination);
}