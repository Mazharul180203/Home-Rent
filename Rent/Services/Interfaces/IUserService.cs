namespace Services.Interfaces;

public interface IUserService
{
   public Task<object> GetUserContacts();
}