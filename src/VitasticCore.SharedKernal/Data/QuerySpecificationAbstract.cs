using System.Linq.Expressions;
using VitasticCore.SharedKernal.Guards;
using VitasticCore.SharedKernal.Responses.Collections;

namespace VitasticCore.SharedKernal.Data;

/// <inheritdoc/>
public abstract class QuerySpecificationAbstract<T> : IQuerySpecification<T>
{
    private readonly List<Expression<Func<T, bool>>> _criteria = new();
    private readonly List<Expression<Func<T, object>>> _includes = new();
    private readonly List<string> _includeStrings = new();
    private readonly List<(Expression<Func<T, object>> OrderBy, bool IsDescending)> _orderings = new();

    /// <summary>
    /// Create a new query
    /// </summary>
    /// <param name="criteria">The filtering criteria</param>
    protected QuerySpecificationAbstract(params Expression<Func<T, bool>>[] criteria)
    {
        AddCriteria(criteria);
    }

    /// <inheritdoc/>
    public IReadOnlyList<Expression<Func<T, bool>>> Criteria => _criteria;

    /// <inheritdoc/>
    public IReadOnlyList<Expression<Func<T, object>>> Includes => _includes;

    /// <inheritdoc/>
    public IEnumerable<string> IncludeStrings => _includeStrings;

    /// <inheritdoc/>
    public IReadOnlyList<(Expression<Func<T, object>> OrderBy, bool IsDescending)> Orderings => _orderings;

    /// <inheritdoc/>
    public PaginationOptions PaginationOptions { get; private set; } = PaginationOptions.None;

    /// <summary>
    /// Add criteria that will be used everytime this specification is used.
    /// </summary>
    /// <param name="criteria">The filtering criteria</param>
    protected void AddCriteria(params Expression<Func<T, bool>>[] criteria)
    {
        _criteria.AddRange(criteria);
    }

    /// <summary>
    /// Add an include expression.
    /// </summary>
    /// <param name="includeSelector">A selector of the extended entities to include</param>
    protected void AddInclude(Expression<Func<T, object>> includeSelector)
    {
        _ = includeSelector.EnsureNotNull();
        _includes.Add(includeSelector);
    }

    /// <summary>
    /// Add an include string.
    /// </summary>
    /// <param name="includeString">A string that can be used with reflection to find extended entities</param>
    protected void AddInclude(string includeString)
    {
        _ = includeString.EnsureNotNullOrEmpty();
        _includeStrings.Add(includeString);
    }

    /// <summary>
    /// Add a level of sorting to the query.
    /// </summary>
    /// <param name="sortPropertySelector">A selector for the property to sort by</param>
    /// <param name="isDescending">Toggle descending sort</param>
    protected void AddOrderBy(Expression<Func<T, object>> sortPropertySelector, bool isDescending = false)
    {
        _ = sortPropertySelector.EnsureNotNull();
        _orderings.Add((sortPropertySelector, isDescending));
    }

    /// <summary>
    /// Enable pagination on the query.
    /// </summary>
    /// <param name="paginationOptions">Options to control pagination</param>
    protected void ApplyPaging(PaginationOptions paginationOptions)
    {
        _ = paginationOptions.EnsureNotNull();
        PaginationOptions = paginationOptions;
    }
}
