using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimeManagementSystem;

public class TimeEntry
{

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int TimeEntryId { get; set; }

    public required int UserId { get; set; }

    public required int ProjectId { get; set; }
    public Project? Project { get; set; }


    public required DateTime StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    public TimeSpan? Duration => EndTime.HasValue ? EndTime.Value - StartTime : null;


    public required DateTime EntryDate { get; set; }

}