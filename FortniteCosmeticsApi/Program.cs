
using FortniteCosmeticsApi.Gateways;
using FortniteCosmeticsApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddScoped<ICosmeticService, CosmeticService>();
builder.Services.AddScoped<ICosmeticGateway, CosmeticGateway>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.MapControllers();
app.Run();