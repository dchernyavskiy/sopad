using System.Net.Mime;
using System.Reflection.Metadata;
using System.Text;
using LB3.Models;
using LB3.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace LB3.Controllers;

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
    public async Task<ActionResult<HashResponse>> Hash(HashRequest request)
    {
        return new HashResponse(_hashService.Hash(request.Data, request.BitSize));
    }

    [HttpPost("/collision", Name = "Collision")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [SwaggerOperation(
        Summary = "Collision",
        Description = "Collision",
        OperationId = "Collision",
        Tags = new[]
        {
            "Hash"
        })]
    public async Task<string> Collision(HashRequest request)
    {
        var data = Encoding.UTF8.GetBytes(request.Data);
        var collision = _hashService.Collision(data, request.BitSize);
        var result = Encoding.UTF8.GetString(collision);
        return result;
    }

    [HttpPost("/hash-file", Name = "HashFile")]
    [ProducesResponseType(typeof(HashResponse), StatusCodes.Status200OK)]
    [SwaggerOperation(
        Summary = "HashFile",
        Description = "HashFile",
        OperationId = "HashFile",
        Tags = new[]
        {
            "Hash"
        })]
    public async Task<ActionResult<HashResponse>> HashFile(IFormFile file, int bitSize)
    {
        await using (var ms = new MemoryStream())
        {
            await file.OpenReadStream().CopyToAsync(ms);
            return new HashResponse(_hashService.Hash(ms.ToArray(), bitSize));
        }
    }

    [HttpPost("/collision-file", Name = "CollisionFile")]
    [ProducesResponseType(typeof(HashResponse), StatusCodes.Status200OK)]
    [SwaggerOperation(
        Summary = "CollisionFile",
        Description = "CollisionFile",
        OperationId = "CollisionFile",
        Tags = new[]
        {
            "Hash"
        })]
    public async Task<IActionResult> CollisionFile(IFormFile file, int bitSize = 2)
    {
        await using (var ms = new MemoryStream())
        {
            await file.OpenReadStream().CopyToAsync(ms);
            var data = ms.ToArray();
            byte[] collision = null;
            if (file.ContentType.Contains("image"))
            {
                collision = _hashService.CollisionInTheMiddle(ms.ToArray(), bitSize);
            }
            else if (file.ContentType.Contains("document"))
            {
                var doc = new Aspose.Words.Document(ms);
                collision = _hashService.CollisionInWord(doc, bitSize);
            }
            else
            {
                collision = _hashService.Collision(ms.ToArray(), bitSize);
            }

            var newName = Path.GetFileNameWithoutExtension(file.FileName) + "-upd" + Path.GetExtension(file.FileName);
            return File(new MemoryStream(collision), file.ContentType, newName);
        }
    }
}