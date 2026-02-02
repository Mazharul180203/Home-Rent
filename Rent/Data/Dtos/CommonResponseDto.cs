namespace Data.Dtos;

public class CommonResponseDto
{
    public string? Message { get; set; }
    public string? Status { get; set; }
    public object? Data { get; set; }
}

public class TokenDto
{
    public string? AccessToken { get; set; }
    public string? RefreshToken { get; set; }
}
public class LoginResponseDto
{
    public string? Message { get; set; }
    public string? Status { get; set; }
    public List<TokenDto>? Tokens { get; set; }
}