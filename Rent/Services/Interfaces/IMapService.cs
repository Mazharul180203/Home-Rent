namespace Services.Interfaces;

public interface IMapService
{
    public Task<Object> GetCoordinate(string address);
    public Task<Object>GetDistanceAndTime(string origin, string destination);
}