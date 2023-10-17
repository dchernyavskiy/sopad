using LB1.Models.Requests;
using LB1.Models.Responses;
using LB1.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace LB1.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DesController : ControllerBase
{
    private readonly IConversionService _conversionService;
    private readonly IDataProcessor _dataProcessor;

    public DesController(IConversionService conversionService, IDataProcessor dataProcessor)
    {
        _conversionService = conversionService;
        _dataProcessor = dataProcessor;
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
    public async Task<ActionResult<EncryptResponse>> Login([FromBody] EncryptRequest request)
    {
        var encryptedText = _conversionService.BinToHex(_dataProcessor.Encrypt(request.Key, request.PlainText));
        return new EncryptResponse(encryptedText);
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
    public async Task<ActionResult<DecryptResponse>> Login([FromBody] DecryptRequest request)
    {
        var decryptedText =
            _conversionService.BinToUTF(_dataProcessor.Decrypt(request.Key,
                _conversionService.HexToBinary(request.CipheredText)));
        return new DecryptResponse(decryptedText);
    }
}