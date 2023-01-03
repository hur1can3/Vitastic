using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vitastic.Domain.Endpoints.Recipes;
public class ListRecipesResponse
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public IEnumerable<string>? Categories { get; set; }
    public int? ImageId { get; set; }
}
