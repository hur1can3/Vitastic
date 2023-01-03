using System.Linq.Expressions;
using Vitastic.Domain.Data.Models;
using VitasticCore.SharedKernal.Data;

namespace Vitastic.Domain.Data.Queries;

public class CategoriesSpecification : QuerySpecificationAbstract<Category>
{
    public CategoriesSpecification(params Expression<Func<Category, bool>>[] criteria) : base(criteria) { }
}
