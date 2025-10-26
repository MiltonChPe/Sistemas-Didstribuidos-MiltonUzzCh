using System.ServiceModel.Channels;
using Microsoft.AspNetCore.Mvc;
using PokedexApi.Dtos;
using PokedexApi.Mappers;
using PokedexApi.Models;
using PokedexApi.Services;
using PokedexApi.Exceptions;
using Microsoft.AspNetCore.Authorization;


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
    [Authorize(Policy ="Read")]
    public async Task<ActionResult<PokemonResponse>> GetPokemonByIdAsync(Guid id, CancellationToken cancellationToken)
    {   //El okey se traduce a un http 200 basicamente nos facilita para el estado
        var pokemon = await _pokemonService.GetPokemonByIdAsync(id, cancellationToken);
        return pokemon is null ? NotFound() : Ok(pokemon.ToResponse());
    }

    //localhost:PORT/api/v1/pokemons?name=nombre
    //HTTP status - Get 
    //200 - OK (si encuentra pokemons o no)
    [HttpGet] //aqui no le paso nada porque el id lo paso por query
    [Authorize(Policy ="Read")]
    public async Task<ActionResult<PagedPokemonResponse>> GetPokemonsAsync([FromQuery] string name, [FromQuery] string type, [FromQuery] int pageSize, [FromQuery] int pageNumber, [FromQuery] string orderBy, [FromQuery] string orderDirection, CancellationToken cancellationToken)
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
    [Authorize(Policy ="Write")]

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
    [Authorize(Policy ="Write")]
    public async Task<ActionResult<PokemonResponse>> CreatePokemonAsync([FromBody] CreatePokemonRequest createPokemon, CancellationToken cancellationToken)
    {
        try
        {
            if (!InvalidAttack(createPokemon.Stats.Attack))
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

    //update
    //localhost:PORT/api/v1/pokemons/ID
    // HTTP Verb - Put
    // HTTP Status
    // 204- No content (si se actualizo correctamente y si sirve para rest)
    // 200- Ok (Retornar la entidad actualizada pero no sigue las reglas de REST)
    // 404- Not Found (si el pokemon no existe)
    // 400- Bad Request (si el body request no es valido)

    [HttpPut("{id}")]
    [Authorize(Policy ="Write")]

    public async Task<IActionResult> UpdatePokemonAsync(Guid id, [FromBody] UpdatePokemonRequest pokemon, CancellationToken cancellationToken)
    {
        try
        {
            if (!InvalidAttack(pokemon.Stats.Attack))
            {
                return BadRequest(new { Message = "Attack does not have a valid value" });
            }

            await _pokemonService.UpdatePokemonAsync(pokemon.ToModel(id), cancellationToken);
            return NoContent(); //204
        }
        catch (PokemonNotFoundException)
        {
            return NotFound(); //404
        }
        catch (PokemonAlreadyExistsException ex)
        {
            return Conflict(new { Message = ex.Message }); // 409
        }
    }

    //localhost:PORT/api/v1/pokemons/ID
    // HTTP Verb - PATCH
    // 209

    [HttpPatch("{id}")]
    [Authorize(Policy ="Write")]

    public async Task<ActionResult<PokemonResponse>> PatchPokemonAsync(Guid id, [FromBody] PatchPokemonRequest pokemonRequest, CancellationToken cancellationToken)
    {
        try
        {
            if (pokemonRequest.attack.HasValue && !InvalidAttack(pokemonRequest.attack.Value))
            {
                return BadRequest(new { Message = "Attack does not have a valid value" });
            }
            var pokemon = await _pokemonService.PatchPokemonAsync(id, pokemonRequest.Name, pokemonRequest.Type, pokemonRequest.attack, pokemonRequest.defense, pokemonRequest.speed, cancellationToken);
            return Ok(pokemon.ToResponse());
        }
        catch (PokemonNotFoundException)
        {
            return NotFound(); //404
        }
        catch (PokemonAlreadyExistsException ex)
        {
            return Conflict(new { Message = ex.Message }); // 409
        }
    }
    private static bool InvalidAttack(int attack)
    {
        return attack > 0;
    }
 
    
}

