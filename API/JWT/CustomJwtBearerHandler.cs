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
        // 1. Extract Token
        if (!Context.Request.Headers.TryGetValue("Authorization", out var authHeader))
        {
            return AuthenticateResult.Fail("Authorization header not found.");
        }

        var token = authHeader.ToString().Replace("Bearer ", "").Trim();
        if (string.IsNullOrEmpty(token))
        {
            return AuthenticateResult.Fail("Token is empty.");
        }

        // 2. Validate and Get Principal in one go
        try
        {
            var principal = ValidateAndGetPrincipal(token);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);
            return AuthenticateResult.Success(ticket);
        }
        catch (Exception ex)
        {
            return AuthenticateResult.Fail($"Token validation failed: {ex.Message}");
        }
    }

    private ClaimsPrincipal ValidateAndGetPrincipal(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);

        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = _configuration["Jwt:Issuer"],
            ValidateAudience = true,
            ValidAudience = _configuration["Jwt:Audience"],
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
        
            // This tells the validator how to map the JSON to User.IsInRole()
            RoleClaimType = "role", 
            NameClaimType = "sub"
        };

        // This method validates AND creates the ClaimsPrincipal with the correct settings
        var principal = tokenHandler.ValidateToken(token, validationParameters, out _);

        return principal;
    }
}