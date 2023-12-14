using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("ocelot.json", true, true)
    .AddJsonFile($"ocelot.{builder.Environment.EnvironmentName}.json", false, true)
    .AddEnvironmentVariables();

builder.Services.AddOcelot();

var app = builder.Build();

await app.UseOcelot();

app.Run();