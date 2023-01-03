using VitasticCore.SharedKernal.Responses.Collections;

namespace Vitastic.Domain.Events.Recipes;

public record ListRecipesRequest(
    string? NameSearch,
    string? CategorySearch,
    string? SortBy,
    bool SortDesc,
    bool IsPagingEnabled,
    int Page,
    int Take) : IPaginatedRequest;
