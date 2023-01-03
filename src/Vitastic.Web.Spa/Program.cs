
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;
using Vitastic.Data.EntityFramework;
using Vitastic.Domain.Data;
using Vitastic.Web.Spa.Auth;
using Vitastic.Web.Spa.Configuration;
using VitasticCore.AspNet.ClientApp;
using VitasticCore.AspNet.Configuration;
using VitasticCore.AspNet.Logging;
using VitasticCore.AspNet.Routing;
using VitasticCore.AspNet.Security;
using VitasticCore.SharedKernal.Auth;
using VitasticCore.SharedKernal.Configuration;
using VitasticCore.SharedKernal.Time;

var builder = WebApplication.CreateBuilder(args);
var env = builder.Environment;
var config = builder.Configuration;
var services = builder.Services;

// Logging
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(config)
    .CreateLogger();

builder.Host.UseSerilog();

try
{
    Log.Information("Configuring host for {Name} v{Version}", ThisAssembly.AssemblyTitle, ThisAssembly.AssemblyInformationalVersion);

    // Settings
    services.AddSettingsSingleton<WebApplicationSettings>(config, true).Validate();

    // Infrastructure
    _ = services.AddControllers();
    services.AddSpaSecurityServices(env);
    services.AddApiExceptionFilter();

    // Authorization

    // Dependencies
    _ = services.AddHttpContextAccessor();
    services.AddSingleton<ICurrentUserAccessor, SingleUserAccessor>();
    _ = services.AddSingleton<IDateTimeService, NowDateTimeService>();

    config.GetRequiredConnectionString<FoodStuffsContext>();
    _ = services.AddDbContext<FoodStuffsContext>();
    services.AddScoped<IFoodStuffsData, FoodStuffsEfData>();

    // Auto-register Domain Events
    services.AddDomainEvents(
        ServiceLifetime.Scoped,
        typeof(GetWebClientInfo).Assembly,
        typeof(IFoodStuffsData).Assembly);

    _ = services.AddSwaggerWithCsp(env);

    var app = builder.Build();

    // Middleware pipeline
    app.UseSpaExceptionPage(env)
        .UseSecureTransport(env)
        .UseSecurityHeaders(env)
        .UseStaticFiles()
        .UseRouting()
        .UseRequestLoggingScope()
        .UseSerilogRequestLogging()
        .UseCurrentUserLogging()
        .UseSwaggerAndUi(env)
        .UseSpaEndpoints();

    Log.Information("Starting host.");
    app.Run();
    return 0;
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly.");
    return 1;
}
finally
{
    Log.Information("Stopping host.");
    Log.CloseAndFlush();
}
