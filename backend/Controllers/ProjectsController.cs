using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace TimeManagementSystem;

[ApiController]
[Route("api/projects")]
public class ProjectsController : ControllerBase
{
    private readonly DataContext _context;

    public ProjectsController(DataContext context)
    {
        _context = context;
    }

    [HttpGet("list")]
    public async Task<ActionResult<List<ProjectResponse>>> List()
    {
        var projects = await _context.Projects.ToListAsync();
        List<ProjectResponse> projectResponseList = new List<ProjectResponse>();
        projects.ForEach(c => projectResponseList.Add(
            new ProjectResponse()
            {
                ProjectId = c.ProjectId,
                UserId = c.UserId,
                ProjectName = c.ProjectName,
            })
        );
        return projectResponseList;
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<ProjectResponse>> Get(int id)
    {
        var project = await _context.Projects
                .Include(p => p.User)
                .FirstOrDefaultAsync(p => p.ProjectId == id);
        if (project == null)
        {
            return NotFound();
        }

        ProjectResponse projectResponse = new()
        {
            ProjectId = project.ProjectId,
            UserId = project.UserId,
            ProjectName = project.ProjectName
        };
        projectResponse.User = project.User;
        return Ok(projectResponse);
    }

    [HttpPut]
    public async Task<ActionResult<ProjectResponse>> Put(Project projectRequest)
    {
        if (projectRequest == null || projectRequest.ProjectId <= 0)
        {
            return BadRequest();
        }

        var existing = await _context.Projects.FindAsync(projectRequest.ProjectId);
        if (existing == null)
        {
            return NotFound();
        }

        existing.UserId = projectRequest.UserId;
        existing.ProjectName = projectRequest.ProjectName;

        await _context.SaveChangesAsync();

        ProjectResponse projectResponse = new()
        {
            UserId = existing.UserId,
            User = existing.User,
            ProjectName = existing.ProjectName,

        };

        return Ok(existing);
    }

    [HttpPost]
    public async Task<ActionResult<ProjectResponse>> Post(Project projectRequest)
    {
        if (projectRequest == null)
        {
            return BadRequest();
        }

        _context.Projects.Add(projectRequest);
        await _context.SaveChangesAsync();
        return Ok(projectRequest);
    }

   [HttpDelete("{id}")]
    public async Task<ActionResult<bool>> Delete(int id)
    {
        var project = await _context.Projects.FindAsync(id);
        if (project == null)
        {
            return NotFound();
        }
        _context.Projects.Remove(project);
        await _context.SaveChangesAsync();
        return Ok(true);
    }

}