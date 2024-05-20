using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace TimeManagementSystem;

[ApiController]
[Route("api/timeEntries")]
public class TimeEntriesController : ControllerBase
{
    private readonly DataContext _context;

    public TimeEntriesController(DataContext context)
    {
        _context = context;
    }

    [HttpGet("list")]
    public async Task<ActionResult<List<TimeEntryResponse>>> List()
    {
        var timeEntries = await _context.TimeEntries.ToListAsync();
        List<TimeEntryResponse> timeEntryResponseList = new List<TimeEntryResponse>();
        timeEntries.ForEach(t => timeEntryResponseList.Add(new TimeEntryResponse()
        {
            TimeEntryId = t.TimeEntryId,
            UserId = t.UserId,
            ProjectId = t.ProjectId,
            Project = t.Project,
            StartTime = t.StartTime,
            EndTime = t.EndTime,
            EntryDate = t.EntryDate,
        }));
        return timeEntryResponseList;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TimeEntryResponse>> Get(int id)
    {
        var timeEntry = await _context.TimeEntries
                .Include(t => t.Project)
                .Include(t => t.Project.User)
                .FirstOrDefaultAsync(t => t.TimeEntryId == id);
        if (timeEntry == null)
        {
            return NotFound();
        }

        TimeEntryResponse timeEntryResponse = new()
        {
            TimeEntryId = timeEntry.TimeEntryId,
            UserId = timeEntry.UserId,
            ProjectId = timeEntry.ProjectId,
            Project = timeEntry.Project,
            StartTime = timeEntry.StartTime,
            EndTime = timeEntry.EndTime,
            EntryDate = timeEntry.EntryDate,
        };
        return Ok(timeEntryResponse);
    }

    [HttpPut]
    public async Task<ActionResult<TimeEntryResponse>> Put(TimeEntry timeEntryRequest)
    {
        if (timeEntryRequest == null || timeEntryRequest.ProjectId <= 0)
        {
            return BadRequest();
        }

        var existing = await _context.TimeEntries.FindAsync(timeEntryRequest.ProjectId);
        if (existing == null)
        {
            return NotFound();
        }

        existing.TimeEntryId = timeEntryRequest.TimeEntryId;
        existing.UserId = timeEntryRequest.UserId;
        existing.ProjectId = timeEntryRequest.ProjectId;
        existing.UserId = timeEntryRequest.UserId;
        existing.Project = timeEntryRequest.Project;
        existing.StartTime = timeEntryRequest.StartTime;
        existing.EndTime = timeEntryRequest.EndTime;
        existing.EntryDate = timeEntryRequest.EntryDate;

        await _context.SaveChangesAsync();

        return Ok(existing);
    }

    [HttpPost]
    public async Task<ActionResult<TimeEntryResponse>> Post(TimeEntry timeEntryRequest)
    {
        if (timeEntryRequest == null)
        {
            return BadRequest();
        }

        _context.TimeEntries.Add(timeEntryRequest);
        await _context.SaveChangesAsync();
        return Ok(timeEntryRequest);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<bool>> Delete(int id)
    {
        var timeEntry = await _context.TimeEntries.FindAsync(id);
        if (timeEntry == null)
        {
            return NotFound();
        }
        _context.TimeEntries.Remove(timeEntry);
        await _context.SaveChangesAsync();
        return Ok(true);
    }

}


