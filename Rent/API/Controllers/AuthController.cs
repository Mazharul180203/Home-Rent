using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class AuthController : Controller
{
    private readonly ILogger<AuthController> _logger;

    public AuthController(ILogger<AuthController> logger)
    {
        _logger = logger;
    }
}