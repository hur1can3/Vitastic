using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vitastic.Domain.Events.Images;
using VitasticCore.AspNet.ClientApp;
using VitasticCore.AspNet.EndpointMapper;
using VitasticCore.SharedKernal.Functional;
using VitasticCore.SharedKernal.Guards;
using VitasticCore.SharedKernal.Responses.Collections;
using VitasticCore.SharedKernal.Responses.Messages;

namespace Vitastic.Endpionts;
public sealed class ImagesEndpoint : IEndpoint
{
    public class PostImageClass
    {
        public int RecipeId { get; set; }
        public IFormFile File { get; set; }
    }



    /// <summary>
    /// Get an image.
    /// </summary>
    /// <param name="getPipeline"></param>
    /// <param name="id">The Id of the image to download</param>
    [HttpMapGet("/images/{id}")]
    [ProducesResponseType(typeof(IItemSet<IFailure>), 400)]    
    public async Task<Microsoft.AspNetCore.Http.IResult> Get([FromServices] GetImagePipeline getPipeline, int id)
    {
        var request = new GetImageRequest(id);

        return await getPipeline
            .Handle(request)
            .MapAsync(TypedHttpResponder.Respond);
    }


    /// <summary>
    /// Upload an image using a multi-part form file.
    /// </summary>
    /// <param name="savePipeline"></param>
    /// <param name="recipeId">The Id of the recipe the image is of</param>
    /// <param name="file">The file to upload</param>
    //[HttpPost]
    [HttpMapPost("/images")]
    [ProducesResponseType(typeof(EntityMessage<int>), 200)]
    [ProducesResponseType(typeof(IItemSet<IFailure>), 400)]   
    public async Task<Microsoft.AspNetCore.Http.IResult> Upload([FromServices] SaveImagePipeline savePipeline, HttpContext ctx)
    {
        byte[] content;
        int.TryParse(ctx.Request.Form["recipeId"], out var recipeId);

        if (!ctx.Request.Form.Files.Any())
            return TypedResults.BadRequest("There are no files");

        var file = ctx.Request.Form.Files.FirstOrDefault();

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
            .MapAsync(TypedHttpResponder.Respond)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Delete an image.
    /// </summary>
    /// <param name="deletePipeline"></param>
    /// <param name="id">ID of the image</param>
    //[Route("{id}")]
    //[HttpDelete]
    [HttpMapDelete("/images/{id}")]
    [ProducesResponseType(typeof(EntityMessage<int>), 200)]
    [ProducesResponseType(typeof(IItemSet<IFailure>), 400)]
    public Task<Microsoft.AspNetCore.Http.IResult> Delete([FromServices] DeleteImagePipeline deletePipeline, int id)
    {
        var request = new DeleteImageRequest(id);

        return deletePipeline
            .Handle(request)
            .MapAsync(TypedHttpResponder.Respond)
            ;
    }

    /// <summary>
    /// Pin an image for a recipe. This image will be the default image for the recipe.
    /// </summary>
    /// <param name="pinPipeline"></param>
    /// <param name="request">The request containing which image to pin</param>
    //[Route("pin")]
    //[HttpPost]
    [HttpMapPost("/images/pin")]
    [ProducesResponseType(typeof(EntityMessage<int>), 200)]
    [ProducesResponseType(typeof(IItemSet<IFailure>), 400)]
    public Task<Microsoft.AspNetCore.Http.IResult> Pin([FromServices] PinImagePipeline pinPipeline,[FromBody] PinImageRequest request)
    {
        return pinPipeline
            .Handle(request)
            .MapAsync(TypedHttpResponder.Respond)
            ;
    }

}
