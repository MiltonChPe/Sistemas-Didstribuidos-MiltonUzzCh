using System.ServiceModel.Channels;
using Microsoft.AspNetCore.Mvc;
using PokedexApi.Dtos;
using PokedexApi.Mappers;
using PokedexApi.Models;
using PokedexApi.Services;
using PokedexApi.Exceptions;


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
    [HttpGet("{id}", Name = "GetPokemonByIdAsync")] // aqui le digo que del metodo Get voy a recibir un id de tipo guid
    public async Task<ActionResult<PokemonResponse>> GetPokemonByIdAsync(Guid id, CancellationToken cancellationToken)
    {   //El okey se traduce a un http 200 basicamente nos facilita para el estado
        var pokemon = await _pokemonService.GetPokemonByIdAsync(id, cancellationToken);
        return pokemon is null ? NotFound() : Ok(pokemon.ToResponse());
    }

    //localhost:PORT/api/v1/pokemons?name=nombre
    //HTTP status - Get 
    //200 - OK (si encuentra pokemons o no)
    [HttpGet] //aqui no le paso nada porque el id lo paso por query

    public async Task<ActionResult<PagedPokemonResponse>> GetPokemonsAsync([FromQuery] string name, [FromQuery] string type, [FromQuery] int pageSize, [FromQuery] int pageNumber, [FromQuery] string orderBy, [FromQuery] string orderDirection, CancellationToken cancellationToken = default)
    {
        
        if (string.IsNullOrEmpty(type))
        {
            return BadRequest(new { Message = "Type query parameter is required" });
        }

        var paginated = await _pokemonService.GetPokemonsAsync(name, type, pageSize, pageNumber, orderBy, orderDirection, cancellationToken);
        return Ok(paginated.ToPagedResponse());
    }

    //localhost:PORT/api/v1/pokemons/{id}
    // HTTP Verb - Delete
    // HTTP Status 
    // 204 - No Content (si se borro correctamente)
    // 200 Ok (si se borro correctamente)- no sigue las reglas de REST pero es valido
    // 404 - Not Found (si el pokemon no existe)
    // 500 - Internal Server Error
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePokemonAsync(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            await _pokemonService.DeletePokemonAsync(id, cancellationToken);
            return NoContent(); //204
        }
        catch (PokemonNotFoundException)
        {
            return NotFound(); //404
        }
    }


    //localhost:PORT/api/v1/pokemons
    //debe recibir un body request - JSON {}
    //Http verb - post
    //400-Bad Request (si el body request no es valido)
    //409 - Conflict (si el pokemon ya existe)
    // 422 - Unprocessable Entity (Regla de negocio interna no se cumple)
    //500 - Internal Server Error   
    //200 - OK (El pokemon se creo mas el id) -- no sigue muy bien las reglas de REST pero es valido
    //201-Created (El pokemon creado + el id) -- sigue las reglas de REST porque devuelve un href para devolder el recurso
    //key: localhost:PORT/api/v1/pokemons/ aqui va el id
    //202 - Accepted (Procesamiento asincrono) no hace nada en base de datos
    [HttpPost]
    public async Task<ActionResult<PokemonResponse>> CreatePokemonAsync([FromBody] CreatePokemonRequest createPokemon, CancellationToken cancellationToken)
    {
        try
        {
            if (!InvalidAttack(createPokemon))
            {
                return BadRequest(new { Message = "Attack does not have a valid value" });
            }

            var pokemon = await _pokemonService.CreatePokemonAsync(createPokemon.ToModel(), cancellationToken);
            return CreatedAtRoute(nameof(GetPokemonByIdAsync),
                new { id = pokemon.Id }, pokemon.ToResponse());
        }

        catch (PokemonAlreadyExistsException ex)
        {
            return Conflict(new { Message = ex.Message });
        }
    }

    private static bool InvalidAttack(CreatePokemonRequest createPokemon)
    {
        return createPokemon.Stats.Attack > 0;
    }
    
}

