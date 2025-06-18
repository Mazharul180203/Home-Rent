namespace Services.Interfaces;

public interface IAuthService
{
    public Task<object> AddCreateRequest(object data);
}