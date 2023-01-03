using Serilog;
using Vitastic.Data.EntityFramework;
using Vitastic.Domain.Data;
using Vitastic.Endpionts;
using Vitastic.Web.Auth;
using Vitastic.Web.Configuration;
using VitasticCore.AspNet.Configuration;
using VitasticCore.AspNet.EndpointMapper;
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

    _ = builder.Services.AddEndpointsApiExplorer();
    _ = builder.Services.AddSwaggerGen(c =>
    {
        //c.OperationFilter<SecureEndpointAuthRequirementFilter>();

        //c.AddSecurityDefinition("Bearer", new()
        //{
        //    Name = "Bearer JWT",
        //    Type = SecuritySchemeType.Http,
        //    In = ParameterLocation.Header,
        //    Scheme = "Bearer"
        //});
    });

    //add endpoints
    _ = builder.Services.AddEndpointMapper<RecipesEndpoint>(e =>
    {
        e.RoutePrefix = "/api";
    });

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
        .UseSpaEndpoints();
        ;

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseEndpointMapper();

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
