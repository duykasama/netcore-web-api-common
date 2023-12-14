using NetCore.Architecture.Gateway.Yarp.Constants;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection(CoreConstants.REVERSE_PROXY_SECTION));

var app = builder.Build();

app.MapReverseProxy();

app.Run();