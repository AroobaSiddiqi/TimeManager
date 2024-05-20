namespace TimeManagementSystem;

public class TimeEntryResponse: TimeEntryRequest
{

    public Project? Project { get; set; }

    public DateTime? EndTime { get; set; }

    public TimeSpan? Duration => EndTime.HasValue ? EndTime.Value - StartTime : null;


}