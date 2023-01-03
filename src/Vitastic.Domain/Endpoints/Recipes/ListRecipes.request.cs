using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VitasticCore.SharedKernal.Responses.Collections;

namespace Vitastic.Domain.Endpoints.Recipes;

public class ListRecipesRequest : IPaginatedRequest
{
    public string? NameSearch { get; set; }
    public string? CategorySearch { get; set; }
    public string? SortBy { get; set; }
    public bool SortDesc { get; set; }
    public bool IsPagingEnabled { get; set; }
    public int Page { get; set; }
    public int Take { get; set; }
}

