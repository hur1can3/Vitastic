using Vitastic.Domain.Data.Models;
using VitasticCore.SharedKernal.Data;

namespace Vitastic.Domain.Data.Queries;

public class ImagesByIdWithBlobsSpecification : QuerySpecificationAbstract<Image>
{
    public ImagesByIdWithBlobsSpecification(int id) : base()
    {
        AddCriteria(i => i.Id == id);
        AddInclude(nameof(Image.Blob));
    }
}
