using FastEndpoints;
using FastEndpoints.ClientGen;
using FastEndpoints.Swagger;
using Serilog;
using Vitastic.Data.EntityFramework;
using Vitastic.Domain.Data;
using Vitastic.Web.Auth;
using Vitastic.Web.Configuration;
using VitasticCore.AspNet.Configuration;
using VitasticCore.AspNet.Logging;
using VitasticCore.AspNet.Routing;
using VitasticCore.AspNet.Security;
using VitasticCore.SharedKernal.Auth;
using VitasticCore.SharedKernal.Configuration;
using VitasticCore.SharedKernal.Functional;
using VitasticCore.SharedKernal.Responses.Collections;
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
    _ = services.AddFastEndpoints();
    _ = services.AddSwaggerDoc(settings =>
    {
        settings.Title = "Vitastic";
        settings.DocumentName = "version 1";
        settings.Version = "v1";        
    }, removeEmptySchemas: false);

    // Settings
    services.AddSettingsSingleton<WebApplicationSettings>(config, true).Validate();

    // Infrastructure
    _ = services.AddControllers();
    services.AddApiExceptionFilter();

    // Authorization

    // Dependencies
    _ = services.AddHttpContextAccessor();
    _ = services.AddSingleton<ICurrentUserAccessor, SingleUserAccessor>();
    _ = services.AddSingleton<IDateTimeService, NowDateTimeService>();

    _ = config.GetRequiredConnectionString<FoodStuffsContext>();
    _ = services.AddDbContext<FoodStuffsContext>();
    _ = services.AddScoped<IFoodStuffsData, FoodStuffsEfData>();

    // Auto-register Domain Events
    services.AddDomainEvents(
        ServiceLifetime.Scoped,
        //typeof(GetWebClientInfo).Assembly,
        typeof(IFoodStuffsData).Assembly);


    var app = builder.Build();

    // Middleware pipeline
    _ = app
        .UseSpaExceptionPage(env)
        .UseSecureTransport(env)
        .UseHttpsRedirection()
        .UseSecurityHeaders(env)
        .UseStaticFiles()
        .UseRouting()
        .UseRequestLoggingScope()
        .UseSerilogRequestLogging()
        .UseCurrentUserLogging()
        //.UseSwaggerAndUi(env)
        .UseFastEndpoints(c =>
        {
            c.Endpoints.RoutePrefix = "api";
            c.Endpoints.ShortNames = true;

            c.Endpoints.Configurator = ep =>
            {
                if (ep.Routes?.FirstOrDefault()?.StartsWith("/api") is true)
                {
                    ep.AllowAnonymous();
                    ep.Description(b => b.Produces<IItemSet<IFailure>>(400, "application/json"));
                }
            };
        })
        .UseSwaggerGen()
        .UseSpaEndpoints();
    ;

    if (app.Environment.IsDevelopment())
    {
        //app.UseSwagger();
        _ = app.UseSwaggerUI();

        _ = app.MapTypeScriptClientEndpoint("/ts-client", "version 1", s =>
        {
            s.ClassName = "ApiClient";
            s.TypeScriptGeneratorSettings.Namespace = "";
        });

        //called from commandline with dotnet run --generateclients true
        await app.GenerateClientsAndExitAsync(
        documentName: "version 1", //must match doc name above
        //destinationPath: builder.Environment.WebRootPath,
        destinationPath: Path.Combine(builder.Environment.ContentRootPath, "ClientApp", "src", "webApi"),
        csSettings: null,
        tsSettings: t => { t.ClassName = "ApiClient"; }
        );
    }



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
