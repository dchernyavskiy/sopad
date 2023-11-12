using LB1.Models.Requests;
using LB1.Models.Responses;
using LB1.Services;
using LB1.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace LB1.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DesController : ControllerBase
{
    private readonly IDesService _desService;

    public DesController(IDesService desService)
    {
        _desService = desService;
    }

    [HttpPost("/encrypt", Name = "Encrypt")]
    [ProducesResponseType(typeof(EncryptResponse), StatusCodes.Status200OK)]
    [SwaggerOperation(
        Summary = "Encrypt",
        Description = "Encrypt",
        OperationId = "Encrypt",
        Tags = new[]
        {
            "Des"
        })]
    public async Task<ActionResult<EncryptResponse>> Encrypt([FromBody] EncryptRequest request)
    {
        var encryptedText = _desService.Encrypt(request.PlainText, request.Key);
        return new EncryptResponse(encryptedText, _desService.Entropies);
    }

    [HttpPost("/decrypt", Name = "Decrypt")]
    [ProducesResponseType(typeof(DecryptResponse), StatusCodes.Status200OK)]
    [SwaggerOperation(
        Summary = "Decrypt",
        Description = "Decrypt",
        OperationId = "Decrypt",
        Tags = new[]
        {
            "Des"
        })]
    public async Task<ActionResult<DecryptResponse>> Decrypt([FromBody] DecryptRequest request)
    {
        var decryptedText = _desService.Decrypt(request.CipheredText, request.Key);
        return new DecryptResponse(decryptedText, _desService.Entropies);
    }

    [HttpGet("/stream", Name = "Stream")]
    [ProducesResponseType(typeof(ICollection<int>), StatusCodes.Status200OK)]
    [SwaggerOperation(
        Summary = "Stream",
        Description = "Stream",
        OperationId = "Stream",
        Tags = new[]
        {
            "Des"
        })]
    public async IAsyncEnumerable<int> Stream()
    {
        for (int i = 0; i < 100; i++)
        {
            await Task.Delay(1000);
            yield return i;
        }
    }
}