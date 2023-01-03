using Vitastic.Domain.Events.Images;
using VitasticCore.SharedKernal.Functional;
using VitasticCore.SharedKernal.Responses.Files;
using VitasticCore.SharedKernal.Responses.Messages;

namespace Vitastic.Domain.Services;
public class ImageService
{
    private readonly DeleteImagePipeline _deletePipeline;
    private readonly PinImagePipeline _pinPipeline;
    private readonly GetImagePipeline _getPipeline;
    private readonly SaveImagePipeline _savePipeline;

    public ImageService(
        DeleteImagePipeline deletePipeline,
        PinImagePipeline pinPipeline,
        GetImagePipeline getPipeline,
        SaveImagePipeline savePipelien)
    {
        _deletePipeline = deletePipeline;
        _pinPipeline = pinPipeline;
        _getPipeline = getPipeline;
        _savePipeline = savePipelien;
    }


    /// <summary>
    /// Get an image.
    /// </summary>
    /// <param name="getPipeline"></param>
    /// <param name="id">The Id of the image to download</param>
    public Task<IResult<SimpleFile>> Get(int id)
    {
        var request = new GetImageRequest(id);

        return _getPipeline
            .Handle(request);
    }

    /// <summary>
    /// Upload an image using a multi-part form file.
    /// </summary>
    /// <param name="recipeId">The Id of the recipe the image is of</param>
    /// <param name="imageContent">The image file contents in byte array</param>
    public Task<IResult<EntityMessage<int>>> Upload(int recipeId, byte[] imageContent)
    {
        var request = new SaveImageRequest(recipeId, imageContent);

        return _savePipeline
            .Handle(request);
    }

    /// <summary>
    /// Delete an image.
    /// </summary>
    /// <param name="id">ID of the image</param>
    public Task<IResult<EntityMessage<int>>> Delete(int id)
    {
        var request = new DeleteImageRequest(id);

        return _deletePipeline
            .Handle(request);
    }

    /// <summary>
    /// Pin an image for a recipe. This image will be the default image for the recipe.
    /// </summary>
    /// <param name="request">The request containing which image to pin</param>
    public Task<IResult<EntityMessage<int>>> Pin(PinImageRequest request)
    {
        return _pinPipeline
            .Handle(request);
    }

}
