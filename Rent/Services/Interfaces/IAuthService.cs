using Data.Dtos;
using Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace Services.Interfaces;

public interface IAuthService
{
    public Task<string> CreateRegister(UserRegistrationDto data);
    public Task<object> DoLoginRequest(LoginDto data);
    public Task<object> RefreshToken(string refreshToken);
}