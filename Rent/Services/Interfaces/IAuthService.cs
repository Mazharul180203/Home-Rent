using Data.Dtos;
using Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace Services.Interfaces;

public interface IAuthService
{
    public Task<string> AddCreateRequest(UserRegistrationDto data);
    public Task<object> DoLoginRequest(user data);
}