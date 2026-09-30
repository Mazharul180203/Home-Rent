using Data.Dtos;
using Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace Services.Interfaces;

public interface IAuthService
{
    public Task<CommonResponseDto> CreateRegister(UserRegistrationDto data);
    public Task<LoginResponseDto> DoLoginRequest(LoginDto data);
    public Task<object> RefreshToken(string refreshToken);
}