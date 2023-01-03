namespace VitasticCore.SharedKernal.Responses.Collections;

/// <summary>
/// Extensions for IPaginatedRequest.
/// </summary>
public static class PaginatedRequestExtensions
{
    /// <summary>
    /// Gets a PaginationOptions from an IPaginatedRequest.
    /// </summary>
    /// <param name="request">The request</param>
    public static PaginationOptions GetPaginationOptions(this IPaginatedRequest request)
    {
        return new(request.Page, request.Take, request.IsPagingEnabled);
    }
}
