using Microsoft.EntityFrameworkCore;
using VitasticCore.SharedKernal.Data;
using VitasticCore.SharedKernal.Responses.Collections;

namespace VitasticCore.EntityFramework.Specs;

/// <summary>
/// Build queries from specifications. Adapted from https://github.com/dotnet-architecture/eShopOnWeb
/// </summary>
internal static class EfSpecificationEvaluator
{
    /// <summary>
    /// Evaluate the specification and build the query against the input.
    /// </summary>
    /// <param name="inputQuery">The input query</param>
    /// <param name="specification">The specification to evaluate</param>
    /// <typeparam name="T">The type of entity to query</typeparam>
    /// <returns>The final query</returns>
    public static IQueryable<T> ApplyEfSpecification<T>(this IQueryable<T> inputQuery, IQuerySpecification<T> specification) where T : class
    {
        var query = inputQuery;

        query = specification.Includes.Aggregate(query, (current, include) => current.Include(include));

        query = specification.IncludeStrings.Aggregate(query, (current, include) => current.Include(include));

        query = specification.Criteria.Aggregate(query, (current, criteria) => current.Where(criteria));

        if (specification.Orderings.Count > 0)
        {
            (var orderBy, var orderByIsDescending) = specification.Orderings[0];

            var orderedQuery = orderByIsDescending ?
                query.OrderByDescending(orderBy) :
                query.OrderBy(orderBy);

            var secondaries = specification.Orderings.Skip(1);

            foreach ((var thenBy, var thenByIsDescending) in secondaries)
            {
                orderedQuery = thenByIsDescending ?
                    orderedQuery.ThenByDescending(thenBy) :
                    orderedQuery.ThenBy(thenBy);
            }

            query = orderedQuery;
        }

        var paginationOptions = specification.PaginationOptions;

        return query.GetPage(paginationOptions);
    }
}
