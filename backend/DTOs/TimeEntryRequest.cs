namespace TimeManagementSystem;

public class TimeEntryRequest
{

    public int TimeEntryId { get; set; }

    public int UserId { get; set; }

    public int ProjectId { get; set; }

    public DateTime StartTime { get; set; }

    public required DateTime EntryDate { get; set; }

}