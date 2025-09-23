namespace PokedexApi.Dtos;

//los dtos son los objetos que se van a enviar al cliente
public class PokemonResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Attack { get; set; }

}
