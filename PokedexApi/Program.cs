using PokedexApi.Infrastructure.Soap.Contracts;
using PokedexApi.Services;
using PokedexApi.Gateways;


var builder = WebApplication.CreateBuilder(args);

//Librerias para generar de forma dinamica la documentacion de la API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddScoped<IPokemonService, PokemonService>();
builder.Services.AddScoped<IPokemonGateway, PokemonGateway>();

var app = builder.Build();

// A parte de la documentacion tambien quiero que levantes una ruta para ver una Interfaz bonita
app.UseSwagger();
app.UseSwaggerUI();
//redirección a HTTPS desde el local host
app.UseHttpsRedirection();
app.MapControllers();
app.Run();