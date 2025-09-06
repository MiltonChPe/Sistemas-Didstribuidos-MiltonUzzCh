using Microsoft.AspNetCore.Mvc;
using PokedexApi.Dtos;

namespace PokedexApi.Controllers;

[ApiController]
[Route("api/v1/[controller]")] //el controler lee el nombre de la clase y quita el controoler y deja el pokemons
public class PokemonsController : ControllerBase
{
    //localhost:PORT/api/v1/pokemons (Al hace un get siempre mandar un id par ala ruta)
    [HttpGet("{id:guid}")] // aqui le digo que del metodo Get voy a recibir un id de tipo guid
    public async Task<ActionResult<PokemonResponse>> GetPokemonByIdAsync(Guid id, CancellationToken cancellationToken)
    {   //El okey se traduce a un http 200 basicamente nos facilita para el estado
        return Ok();
    }
}