using Microsoft.AspNetCore.Mvc;
using PokedexApi.Dtos;
using PokedexApi.Mappers;
using PokedexApi.Services;


namespace PokedexApi.Controllers;

[ApiController]
[Route("api/v1/[controller]")] //el controler lee el nombre de la clase y quita el controoler y deja el pokemons
public class PokemonsController : ControllerBase
{   
    private readonly IPokemonService _pokemonService;

    public PokemonsController(IPokemonService pokemonsService)
    {
        _pokemonService = pokemonsService;
    }

    //HTTP status 
    //200 - OK
    //404 - Not Found
    //400 - Bad Request (si el formato del id no es guid o incorrecto)
    //500 - Internal Server Error
    //localhost:PORT/api/v1/pokemons (Al hace un get siempre mandar un id par ala ruta)
    [HttpGet("{id:guid}")] // aqui le digo que del metodo Get voy a recibir un id de tipo guid
    public async Task<ActionResult<PokemonResponse>> GetPokemonByIdAsync(Guid id, CancellationToken cancellationToken)
    {   //El okey se traduce a un http 200 basicamente nos facilita para el estado
        var pokemon = await _pokemonService.GetPokemonByIdAsync(id, cancellationToken);
        return pokemon is null ? NotFound() : Ok(pokemon.ToResponse());
    }
}

