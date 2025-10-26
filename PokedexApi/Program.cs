using PokedexApi.Infrastructure.Soap.Contracts;
using PokedexApi.Services;
using PokedexApi.Gateways;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;


var builder = WebApplication.CreateBuilder(args);

//Librerias para generar de forma dinamica la documentacion de la API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddScoped<IPokemonService, PokemonService>();
builder.Services.AddScoped<IPokemonGateway, PokemonGateway>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = builder.Configuration.GetValue<string>("Authentication:Authority");
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration.GetValue<string>("Authentication:Issuer"),
            ValidateActor = false,
            ValidateLifetime = true,
            ValidateAudience = true,
            ValidAudience = "pokedex-api",
            ValidateIssuerSigningKey = true
        };
        options.RequireHttpsMetadata = false;
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Read", policy => policy.RequireClaim("http://schemas.microsoft.com/identity/claims/scope", 
                    "read"));
    options.AddPolicy("Write", policy => policy.RequireClaim("http://schemas.microsoft.com/identity/claims/scope", 
                    "write"));
});

var app = builder.Build();

// A parte de la documentacion tambien quiero que levantes una ruta para ver una Interfaz bonita
app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthentication();
app.UseAuthorization();
//redirección a HTTPS desde el local host
app.UseHttpsRedirection();
app.MapControllers();
app.Run();