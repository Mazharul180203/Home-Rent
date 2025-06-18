using Data.Dtos;
using Data.Models;

namespace Services.Interfaces;

public interface IAuthService
{
    public Task<object> AddCreateRequest(Register data);
    public Task<object> DoLoginRequest(LoginDto data);
}