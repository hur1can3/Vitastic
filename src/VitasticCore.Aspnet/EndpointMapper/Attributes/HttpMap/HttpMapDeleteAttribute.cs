#pragma warning disable IDE0130 // Namespace does not match the folder structure

using System.Diagnostics.CodeAnalysis;

namespace VitasticCore.AspNet.EndpointMapper;

/// <summary>
/// Map an endpoint to a specific to a DELETE Http Verb and a Route
/// </summary>
[AttributeUsage(AttributeTargets.Method, Inherited = false)]
public sealed class HttpMapDeleteAttribute : HttpMapAttribute
{
    /// <summary>
    /// Map route(s) to the DELETE Http Verb
    /// </summary>
    /// <param name="routes">ASP.NET route strings</param>
    public HttpMapDeleteAttribute([StringSyntax("Route")] params string[] routes) : base(HttpMethod.Delete, routes) { }
}
