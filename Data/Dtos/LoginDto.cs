using System.Runtime.InteropServices.JavaScript;

namespace Data.Dtos;

public class LoginDto
{
    public string username { get; set; } = null!;
    public string password_hash { get; set; } = null!;

}

public class UserInfoDto
{
    public long  id { get; set; }
    public string ? role { get; set; }
}