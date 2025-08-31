

using FortniteApi.Infrastructure;
using FortniteApi.Services;
using Microsoft.EntityFrameworkCore;
using FortniteApi.Repositories;
using SoapCore;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSoapCore();


builder.Services.AddScoped<IFortniteRepository, FortniteRepository>();
builder.Services.AddScoped<IFortniteService, FortniteService>();

builder.Services.AddDbContext<RelationalDbContext>(options =>
{
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
    ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection")));
}
);

var app = builder.Build();
app.UseSoapEndpoint<IFortniteService>("/FortniteService.svc", new SoapEncoderOptions());
app.Run();

