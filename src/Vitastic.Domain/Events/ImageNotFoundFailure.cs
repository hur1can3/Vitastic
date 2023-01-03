
using VitasticCore.SharedKernal.Functional;

namespace Vitastic.Domain.Events;

public class ImageNotFoundFailure : Failure
{
    public ImageNotFoundFailure() : base(errorMessage: "Image not found.", uiHandle: "imageId")
    {
    }
}
