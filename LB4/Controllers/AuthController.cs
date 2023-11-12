using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace LB4.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    [HttpPost("/login", Name = "Login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [SwaggerOperation(
        Summary = "Login",
        Description = "Login",
        OperationId = "Login",
        Tags = new[]
        {
            "Auth"
        })]
    public async Task<ActionResult> Login()
    {
        return Ok();
    }
}