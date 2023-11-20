using LB2.Models.Requests;
using LB2.Models.Responses;
using LB2.Services;
using LB2.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Annotations;

namespace LB2.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    private readonly RsaOptions _rsaOptions;
    // private readonly IRsaService _rsaService;

    public AuthController(IAuthService authService, IOptions<RsaOptions> rsaOptions)
    {
        _authService = authService;
        _rsaOptions = rsaOptions.Value;
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
        return _authService.Login(request.EncryptedLogin, request.EncryptedPassword, request.PublicKey);
    }

    [HttpPost("/get-public-key", Name = "GetPublicKey")]
    [ProducesResponseType(typeof(RsaPublicKey), StatusCodes.Status200OK)]
    [SwaggerOperation(
        Summary = "GetPublicKey",
        Description = "GetPublicKey",
        OperationId = "GetPublicKey",
        Tags = new[]
        {
            "Auth"
        })]
    public async Task<ActionResult<RsaPublicKey>> GetPublicKey()
    {
        return new RsaPublicKey(_rsaOptions.PublicKey);
    }
}