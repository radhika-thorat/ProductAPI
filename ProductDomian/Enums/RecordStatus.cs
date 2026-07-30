namespace ProductSolution.Domain.Enums;

/// <summary>
/// Represents the status of a record in the system.
/// </summary>
public enum RecordStatus
{
    /// <summary>
    /// Indicates that the record is active.
    /// </summary>
    Active = 1,

    /// <summary>
    /// Indicates that the record is inactive.
    /// </summary>
    Inactive = 2,

    /// <summary>
    /// Indicates that the record has been deleted.
    /// </summary>
    Deleted = 3
}