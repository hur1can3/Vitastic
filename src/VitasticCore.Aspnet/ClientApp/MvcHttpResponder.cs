using Microsoft.AspNetCore.Mvc;
using VitasticCore.SharedKernal.Functional;
using VitasticCore.SharedKernal.Guards;
using VitasticCore.SharedKernal.Responses.Collections;
using VitasticCore.SharedKernal.Responses.Files;

namespace VitasticCore.AspNet.ClientApp;

/// <summary>
/// Create ActionResults from Results.
/// </summary>
public static class MvcHttpResponder
{
    /// <summary>
    /// Create a ObjectResult based on pass or fail of the domain result. Returns the success value on success.
    /// </summary>
    /// <param name="result">The domain result</param>
    /// <typeparam name="TSuccessValue">The type of success value in the result</typeparam>
    /// <returns>An IActionResult</returns>
    public static IActionResult Respond<TSuccessValue>(IResult<TSuccessValue> result)
    {
        _ = result.EnsureNotNull();
        return result.IsSuccess ? Ok(result.Value) : Fail(result);
    }

    /// <summary>
    /// Create an ObjectResult based on pass or fail of the domain result. Returns an empty 200 on success.
    /// </summary>
    /// <param name="result">The domain result</param>
    /// <returns>An IActionResult</returns>
    public static IActionResult Respond(IResult result)
    {
        _ = result.EnsureNotNull();
        return result.IsSuccess ? Ok() : Fail(result);
    }

    /// <summary>
    /// Respond with an object in an Object result. Always responds with a successful 200.
    /// </summary>
    /// <param name="obj">An object</param>
    /// <returns>An IActionResult</returns>
    public static IActionResult Respond(object obj)
    {
        return Ok(obj);
    }

    /// <summary>
    /// Create a downloadable FileContentResult.
    /// </summary>
    /// <param name="result">The domain result</param>
    /// <returns>An IActionResult</returns>
    public static IActionResult RespondWithFile(IResult<SimpleFile> result)
    {
        _ = result.EnsureNotNull();

        if (result.IsFailed)
        {
            return Fail(result);
        }

        SimpleFile file = result.Value;
        return new FileContentResult(file.Content.AsBytes, "application/force-download") { FileDownloadName = file.Name };
    }

    private static IActionResult Fail(IResult result)
    {
        return new ObjectResult(result.Failures.ToItemSet()) { StatusCode = 400 };
    }

    private static IActionResult Ok()
    {
        return new ObjectResult(null) { StatusCode = 200 };
    }

    private static IActionResult Ok(object result)
    {
        return new ObjectResult(result) { StatusCode = 200 };
    }
}
