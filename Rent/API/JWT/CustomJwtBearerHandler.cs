using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace API.JWT;

public class CustomJwtBearerHandler : JwtBearerHandler
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    
    public CustomJwtBearerHandler(HttpClient httpClient, IOptionsMonitor<JwtBearerOptions> options, ILoggerFactory logger, UrlEncoder encoder,IConfiguration configuration)
        : base(options, logger, encoder)
    {
        _httpClient = httpClient;
        this._configuration = configuration;

    }
    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
{
    // Get the token from the Authorization header
    if (!Context.Request.Headers.TryGetValue("Authorization", out var authorizationHeaderValues))
    {
        return AuthenticateResult.Fail("Authorization header not found.");
    }

    var authorizationHeader = authorizationHeaderValues.FirstOrDefault();
    if (string.IsNullOrEmpty(authorizationHeader) || !authorizationHeader.StartsWith("Bearer "))
    {
        return AuthenticateResult.Fail("Bearer token not found in Authorization header.");
    }

    var token = authorizationHeader.Substring("Bearer ".Length).Trim();


   Context.Request.Headers.TryGetValue("Authorization", out var stoken);

    // Return an authentication failure if the response is not successful
    if (!ValidateToken(token, stoken))
    {
        return AuthenticateResult.Fail("Token validation failed.");
    }

    // Set the authentication result with the claims from the API response
    var principal = GetClaims(token);

    return AuthenticateResult.Success(new AuthenticationTicket(principal, "CustomJwtBearer"));
}

public bool ValidateToken(string token,string stoken)
{
    if (token == null)
        return false;

    var tokenHandler = new JwtSecurityTokenHandler();
    
    try
    {
        string Audience = _configuration["Jwt:Audience"];
        string Issuer = _configuration["Jwt:Issuer"];
        string Key = _configuration["Jwt:Key"];

        tokenHandler.ValidateToken(token, new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidAudience = Audience,
            ValidIssuer = Issuer,
            IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key))
        }, out Microsoft.IdentityModel.Tokens.SecurityToken validatedToken);

        var jwtToken = (JwtSecurityToken)validatedToken;
        return true;
    }
    catch(Exception ex)
    {
        // return null if validation fails
        return false;
    }
}
private ClaimsPrincipal GetClaims(string Token)
{
    var handler = new JwtSecurityTokenHandler();
    var token = handler.ReadToken(Token) as JwtSecurityToken;

    var claimsIdentity = new ClaimsIdentity(token.Claims, "Token");
    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);



    return claimsPrincipal;
}
}