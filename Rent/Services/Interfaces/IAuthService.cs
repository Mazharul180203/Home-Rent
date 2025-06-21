using Data.Dtos;
using Data.Models;

namespace Services.Interfaces;

public interface IAuthService
{
    public Task<object> AddCreateRequest(UserRegistrationDto data);
    public Task<object> DoLoginRequest(LoginDto data);
}