#pragma warning disable IDE0130 // Namespace does not match the folder structure

using System.Diagnostics.CodeAnalysis;

namespace VitasticCore.AspNet.EndpointMapper;

/// <summary>
/// Map an endpoint to a specific to a GET Http Verb and a Route
/// </summary>
[AttributeUsage(AttributeTargets.Method, Inherited = false)]
public sealed class HttpMapGetAttribute : HttpMapAttribute
{
    /// <summary>
    /// Map route(s) to the GET Http Verb
    /// </summary>
    /// <param name="routes">ASP.NET route strings</param>
    public HttpMapGetAttribute([StringSyntax("Route")] params string[] routes) : base(HttpMethod.Get, routes) { }
}
