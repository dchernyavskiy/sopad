using LB5.Models;
using LB5.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace LB5.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HashController : ControllerBase
{
    private readonly IHashService _hashService;

    public HashController(IHashService hashService)
    {
        _hashService = hashService;
    }

    [HttpPost("/hash", Name = "Hash")]
    [ProducesResponseType(typeof(HashResponse), StatusCodes.Status200OK)]
    [SwaggerOperation(
        Summary = "Hash",
        Description = "Hash",
        OperationId = "Hash",
        Tags = new[]
        {
            "Hash"
        })]
    public async Task<ActionResult<HashResponse>> Login(HashRequest request)
    {
        return new HashResponse(_hashService.Hash(request.Data, request.BitSize));
    }
}