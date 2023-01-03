using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using VitasticCore.AspNet.ClientApp;
using VitasticCore.AspNet.Routing;
using VitasticCore.SharedKernal.Functional;

namespace Vitastic.Web.Spa.Controllers.Api;

/// <summary>
/// Application metadata.
/// </summary>
[ApiRoute("app")]
public class ApplicationController : ControllerBase
{
    /// <summary>
    /// Get information to bootstrap the SPA client like application name and user data.
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Route("info")]
    [ProducesResponseType(typeof(GetWebClientInfo.WebClientInfo), 200)]
    public Task<IActionResult> GetInfo([FromServices] GetWebClientInfo.Pipeline getPipeline)
    {
        return getPipeline
            .Handle(new GetWebClientInfo.Request())
            .MapAsync(MvcHttpResponder.Respond);
    }

    /// <summary>
    /// Get the version of the application.
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Route("version")]
    [ProducesResponseType(typeof(AppVersion), 200)]
    public IActionResult GetVersion()
    {
        return new AppVersion(
                ThisAssembly.AssemblyInformationalVersion.Split('+').FirstOrDefault(),
                ThisAssembly.IsPublicRelease,
                ThisAssembly.IsPrerelease,
                //ThisAssembly.GitCommitId,
                //ThisAssembly.GitCommitDate,
                ThisAssembly.AssemblyConfiguration)
            .Map(MvcHttpResponder.Respond);
    }
}

internal record AppVersion(
    string? Version,
    bool IsPublicRelease,
    bool IsPrerelease,
    //string GitCommitId,
    //DateTime GitCommitDate,
    string AssemblyConfiguration);
