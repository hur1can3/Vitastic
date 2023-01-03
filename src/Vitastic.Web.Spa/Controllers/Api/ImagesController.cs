using Vitastic.Domain.Events.Images;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;
using VitasticCore.AspNet.Routing;
using VitasticCore.SharedKernal.Functional;
using VitasticCore.SharedKernal.Guards;
using VitasticCore.SharedKernal.Responses.Collections;
using VitasticCore.SharedKernal.Responses.Messages;
using VitasticCore.AspNet.ClientApp;

namespace Vitastic.Web.Spa.Controllers.Api;

/// <summary>
/// Manage images.
/// </summary>
[ApiRoute("images")]
public class ImagesController : ControllerBase
{
    /// <summary>
    /// Get an image.
    /// </summary>
    /// <param name="getPipeline"></param>
    /// <param name="id">The Id of the image to download</param>
    [Route("{id}")]
    [HttpGet]
    [ProducesResponseType(typeof(FileContentResult), 200)]
    [ProducesResponseType(typeof(IItemSet<IFailure>), 400)]
    public Task<IActionResult> Get([FromServices] GetImagePipeline getPipeline, int id)
    {
        var request = new GetImageRequest(id);

        return getPipeline
            .Handle(request)
            .MapAsync(MvcHttpResponder.RespondWithFile);
    }

    /// <summary>
    /// Upload an image using a multi-part form file.
    /// </summary>
    /// <param name="savePipeline"></param>
    /// <param name="recipeId">The Id of the recipe the image is of</param>
    /// <param name="file">The file to upload</param>
    [HttpPost]
    [ProducesResponseType(typeof(EntityMessage<int>), 200)]
    [ProducesResponseType(typeof(IItemSet<IFailure>), 400)]
    public async Task<IActionResult> Upload([FromServices] SaveImagePipeline savePipeline, int recipeId, IFormFile file)
    {
        byte[] content;

        using (var memoryStream = new MemoryStream())
        {
            await file
                .EnsureNotNull()
                .CopyToAsync(memoryStream)
                .ConfigureAwait(false);

            content = memoryStream.ToArray();
        }

        var request = new SaveImageRequest(recipeId, content);

        return await savePipeline
            .Handle(request)
            .MapAsync(MvcHttpResponder.Respond)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Delete an image.
    /// </summary>
    /// <param name="deletePipeline"></param>
    /// <param name="id">ID of the image</param>
    [Route("{id}")]
    [HttpDelete]
    [ProducesResponseType(typeof(EntityMessage<int>), 200)]
    [ProducesResponseType(typeof(IItemSet<IFailure>), 400)]
    public Task<IActionResult> Delete([FromServices] DeleteImagePipeline deletePipeline, int id)
    {
        var request = new DeleteImageRequest(id);

        return deletePipeline
            .Handle(request)
            .MapAsync(MvcHttpResponder.Respond);
    }

    /// <summary>
    /// Pin an image for a recipe. This image will be the default image for the recipe.
    /// </summary>
    /// <param name="pinPipeline"></param>
    /// <param name="request">The request containing which image to pin</param>
    [Route("pin")]
    [HttpPost]
    [ProducesResponseType(typeof(EntityMessage<int>), 200)]
    [ProducesResponseType(typeof(IItemSet<IFailure>), 400)]
    public Task<IActionResult> Pin([FromServices] PinImagePipeline pinPipeline, [FromBody] PinImageRequest request)
    {
        return pinPipeline
            .Handle(request)
            .MapAsync(MvcHttpResponder.Respond);
    }
}
