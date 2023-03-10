using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace VitasticCore.AspNet.EndpointMapper;
/// <summary>
/// Middleware to resolve Dependency Injection into the endpoints
/// </summary>
public sealed class EndpointMapperMiddleware
{
    private readonly RequestDelegate _next;

    /// <summary>
    /// Do not Initializzate this class manually. Use UseMiddleware on an <see cref="Microsoft.AspNetCore.Builder.WebApplication"/> instance
    /// </summary>
    /// <param name="next">ASP.NET Request Delegate</param>
    public EndpointMapperMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    /// <summary>
    /// Middleware code.
    /// </summary>
    /// <param name="context">HttpContext for the incoming request</param>
    /// <returns>A <see cref="Task"/></returns>
    public Task InvokeAsync(HttpContext context)
    {
        var endpoint = context.GetEndpoint();

        if (endpoint is null)
        {
            return _next(context);
        }

        var endpointInstace = endpoint.Metadata.GetMetadata<IEndpoint>();

        // if can't get the instance of the endpoint then continue with the pipeline
        if (endpointInstace is null)
        {
            return _next(context);
        }

        var endpointType = endpointInstace.GetType();

        // if it's not an IEndpoint continue the pipeline
        if (endpointType is null || !endpointType.IsAssignableTo(typeof(IEndpoint)))
        {
            return _next(context);
        }

        // Update the services injected
        var constructor = endpointType.GetConstructors()[0];
        var constructorParams = constructor.GetParameters().AsSpan();

        var services = new object[constructorParams.Length];

        for (var i = 0; i < constructorParams.Length; i++)
        {
            services[i] = context.RequestServices.GetRequiredService(constructorParams[i].ParameterType);
        }

        // Call the constructor with the new services
        _ = constructor.Invoke(endpointInstace, services);

        // Continue with the pipeline
        return _next(context);
    }
}
