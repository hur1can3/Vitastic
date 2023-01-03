using Vitastic.Domain.Data.Models;
using VitasticCore.SharedKernal.Data;

namespace Vitastic.Domain.Data.Queries;

public class RecipesByIdSpecification : QuerySpecificationAbstract<Recipe>
{
    public RecipesByIdSpecification(int id) : base()
    {
        AddCriteria(r => r.Id == id);
    }
}
