namespace Services.Interfaces;

public interface IMapService
{
    public Task<Object> GetCoordinate(string address);
}