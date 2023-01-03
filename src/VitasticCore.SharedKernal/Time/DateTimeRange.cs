using VitasticCore.SharedKernal.Guards;

namespace VitasticCore.SharedKernal.Time;

/// <summary>
/// Represents a range of time.
/// </summary>
public record DateTimeRange
{
    /// <summary>
    /// Construct a new DateTimeRange
    /// </summary>
    /// <param name="startDate"></param>
    /// <param name="endDate"></param>
    public DateTimeRange(DateTime startDate, DateTime endDate)
    {
        _ = startDate.Ensure(s => s <= endDate, "startDate cannot be greater than endDate.");
        StartDate = startDate;
        EndDate = endDate;
    }

    /// <summary>
    /// The start date
    /// </summary>
    public DateTime StartDate { get; init; }

    /// <summary>
    /// The end date
    /// </summary>
    public DateTime EndDate { get; init; }
}
