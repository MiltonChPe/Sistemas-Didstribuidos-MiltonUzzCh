using FortniteCosmeticsApi.Dtos;
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


    [HttpGet("{id}", Name = "GetCosmeticByIdAsync")]
    public async Task<ActionResult<CosmeticResponse>> GetCosmeticByIdAsync(Guid id, CancellationToken cancellationToken)
    {
    
        var cosmetic = await _cosmeticService.GetCosmeticByIdAsync(id, cancellationToken);
        return cosmetic is null ? NotFound() : Ok(cosmetic.ToResponse());
    }
}