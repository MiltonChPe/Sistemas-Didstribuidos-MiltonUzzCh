using System.Security.Permissions;
using FortniteCosmeticsApi.Dtos;
using FortniteCosmeticsApi.Exceptions;
using FortniteCosmeticsApi.Mappers;
using FortniteCosmeticsApi.Services;
using Microsoft.AspNetCore.Mvc; 

namespace FortniteCosmeticsApi.Controllers;

[ApiController]
[Route("api/v1/[controller]")]

public class CosmeticsController : ControllerBase
{
    private readonly ICosmeticService _cosmeticService;

    public CosmeticsController(ICosmeticService cosmeticService)
    {
        _cosmeticService = cosmeticService;
    }

    [HttpPost]
    public async Task<ActionResult<CosmeticResponse>> CreateCosmeticAsync([FromBody] CreateCosmeticRequest createCosmetic, CancellationToken cancellationToken)
    {
        try
        {
            var cosmetic = await _cosmeticService.CreateCosmeticAsync(createCosmetic.ToModel(), cancellationToken);
            return CreatedAtRoute(nameof(GetCosmeticByIdAsync), new { id = cosmetic.Id }, cosmetic.ToResponse());
        }
        catch (CosmeticAlreadyExistsException ex)
        {
            return Conflict(new { Message = ex.Message });
        }
    }

    [HttpGet("{id}", Name = "GetCosmeticByIdAsync")]
    public async Task<ActionResult<CosmeticResponse>> GetCosmeticByIdAsync(Guid id, CancellationToken cancellationToken)
    {

        var cosmetic = await _cosmeticService.GetCosmeticByIdAsync(id, cancellationToken);
        return cosmetic is null ? NotFound() : Ok(cosmetic.ToResponse());
    }

    [HttpGet]
    public async Task<ActionResult<PagedCosmeticResponse>> GetCosmeticsAsync([FromQuery] string name, [FromQuery] string type, [FromQuery] int pageSize, [FromQuery] int pageNumber, [FromQuery] string orderBy, [FromQuery] string orderDirection, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(type))
        {
            return BadRequest(new { Message = "Type query parameter is required" });
        }

        var paginated = await _cosmeticService.GetCosmeticsAsync(name, type, pageSize, pageNumber, orderBy, orderDirection, cancellationToken);
        return Ok(paginated.ToPagedResponse());
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCosmeticAsync(Guid id, [FromBody] UpdateCosmeticRequest cosmetic, CancellationToken cancellationToken)
    {
        try
        {
            await _cosmeticService.UpdateCosmeticAsync(cosmetic.ToModel(id), cancellationToken);
            return NoContent();
        }
        catch (CosmeticNotFoundException)
        {
            return NotFound();
        }
        catch (CosmeticAlreadyExistsException ex)
        {
            return Conflict(new { Message = ex.Message });
        }
    }

    [HttpPatch("{id}")]
    public async Task<ActionResult<CosmeticResponse>> PatchCosmeticAsync(Guid id, [FromBody] PatchCosmeticRequest cosmeticRequest, CancellationToken cancellationToken)
    {
        try
        {
            var cosmetic = await _cosmeticService.PatchCosmeticAsync(id, cosmeticRequest.Name,cosmeticRequest.Type, cosmeticRequest.Rarity,cancellationToken);
            return Ok(cosmetic.ToResponse());
        }
        catch (CosmeticNotFoundException)
        {
            return NotFound();
        }
        catch (CosmeticAlreadyExistsException ex)
        {
            return Conflict(new { Message = ex.Message });
        }
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCosmeticAsync(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            await _cosmeticService.DeleteCosmeticAsync(id, cancellationToken);
            return NoContent();
         }
        catch (CosmeticNotFoundException)
        {
            return NotFound();
        }
    }
}