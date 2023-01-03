using Vitastic.Domain.Data.Models;
using VitasticCore.SharedKernal.Data;

namespace Vitastic.Domain.Data.Queries;

public class ImagesByIdWithRecipesSpecification : QuerySpecificationAbstract<Image>
{
    public ImagesByIdWithRecipesSpecification(int id) : base()
    {
        AddCriteria(i => i.Id == id);
        AddInclude(nameof(Image.Recipe));
    }
}
