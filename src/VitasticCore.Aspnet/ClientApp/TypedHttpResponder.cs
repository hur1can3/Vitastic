using Microsoft.AspNetCore.Http;
using VitasticCore.SharedKernal.Functional;
using VitasticCore.SharedKernal.Guards;
using VitasticCore.SharedKernal.Responses.Collections;
using VitasticCore.SharedKernal.Responses.Files;
using VitasticCore.SharedKernal.Responses.Messages;
using IResult = VitasticCore.SharedKernal.Functional.IResult;

namespace VitasticCore.AspNet.ClientApp;


/// <summary>
/// Create Microsoft.AspNetCore.Http.IResult from Results.
/// https://learn.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis/responses?view=aspnetcore-7.0
/// </summary>
public static class TypedHttpResponder
{
    /// <summary>
    /// Create a ObjectResult based on pass or fail of the domain result. Returns the success value on success.
    /// </summary>
    /// <param name="result">The domain result</param>
    /// <typeparam name="TSuccessValue">The type of success value in the result</typeparam>
    /// <returns>An IActionResult</returns>
    public static Microsoft.AspNetCore.Http.IResult Respond<TSuccessValue>(IResult<TSuccessValue> result)
    {
        _ = result.EnsureNotNull();
        return result.IsSuccess ? Ok(result.Value) : Fail(result);
    }

    /// <summary>
    /// Create an ObjectResult based on pass or fail of the domain result. Returns an empty 200 on success.
    /// </summary>
    /// <param name="result">The domain result</param>
    /// <returns>An IActionResult</returns>
    public static Microsoft.AspNetCore.Http.IResult Respond(IResult result)
    {
        _ = result.EnsureNotNull();
        return result.IsSuccess ? Ok() : Fail(result);
    }

    /// <summary>
    /// Respond with an object in an Object result. Always responds with a successful 200.
    /// </summary>
    /// <param name="obj">An object</param>
    /// <returns>An IActionResult</returns>
    public static Microsoft.AspNetCore.Http.IResult Respond(object obj)
    {
        return Ok(obj);
    }

    /// <summary>
    /// Create a ObjectResult based on pass or fail of the domain result. Returns the success value on success.
    /// </summary>
    /// <param name="result">The domain result</param>
    /// <typeparam name="TSuccessValue">The type of success value in the result</typeparam>
    /// <returns>An IActionResult</returns>
    public static Microsoft.AspNetCore.Http.IResult RespondUpdateOrCreate<TSuccessValue>(IResult<EntityMessage<TSuccessValue>> result)
    {
        _ = result.EnsureNotNull();
        if (result.IsSuccess)
        {
            if (string.IsNullOrEmpty(result.Value.UriString))
            {
                return Ok(result.Value);
            }
            else
            {
                return TypedResults.Created($"{result.Value.UriString}", result.Value);
            }
        }
        else
        {
            return Fail(result);
        }
    }



    /// <summary>
    /// Create a downloadable FileContentResult.
    /// </summary>
    /// <param name="result">The domain result</param>
    /// <returns>An IActionResult</returns>
    public static Microsoft.AspNetCore.Http.IResult RespondWithFile(IResult<SimpleFile> result)
    {
        _ = result.EnsureNotNull();

        if (result.IsFailed)
        {
            return Fail(result);
        }

        var file = result.Value;
        return Microsoft.AspNetCore.Http.TypedResults.File(fileContents: file.Content.AsBytes, contentType: "application/force-download", fileDownloadName: file.Name);
    }

    private static Microsoft.AspNetCore.Http.IResult Fail(IResult result)
    {
        return Microsoft.AspNetCore.Http.TypedResults.BadRequest(result.Failures.ToItemSet());
    }

    private static Microsoft.AspNetCore.Http.IResult Ok()
    {
        return Microsoft.AspNetCore.Http.TypedResults.Ok();
    }

    private static Microsoft.AspNetCore.Http.IResult Ok(object result)
    {
        return Microsoft.AspNetCore.Http.TypedResults.Ok(result);
    }


}
