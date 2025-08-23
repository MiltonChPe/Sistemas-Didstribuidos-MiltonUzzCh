using PokemonApi.Infrastructure;

using SoapCore;
using PokemonApi.Infrastructure;
using Microsoft.EntityFrameworkCore;
using PokemonApi.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSoapCore();
//configuracion del aplicativo de soap

builder.Services.AddSingleton<IPokemonService, PokemonService>();

builder.Services.AddDbContext<RelationalDbContext>(options =>
{
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
    ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection")));
});

var app = builder.Build();
app.UseSoapEndpoint<IPokemonService>("/PokemonService.svc", new SoapEncoderOptions());
app.Run();