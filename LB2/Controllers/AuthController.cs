using LB2.Models.Requests;
using LB2.Models.Responses;
using LB2.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace LB2.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("/login", Name = "Login")]
    [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
    [SwaggerOperation(
        Summary = "Login",
        Description = "Login",
        OperationId = "Login",
        Tags = new[]
        {
            "Auth"
        })]
    public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest request)
    {
        return _authService.Login(request.EncryptedLogin, request.EncryptedPassword);
    }
}