using Data.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Services.Interfaces;

public interface IUserService
{
   public Task<object> GetUserContacts(string userIdClaims);
   public Task<string> AddUserContacts(UserContactDto data, string userIdClaims);
   
}