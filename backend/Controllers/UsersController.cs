using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace TimeManagementSystem;

[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly DataContext _context;

    public UsersController(DataContext context)
    {
        _context = context;
    }

    [HttpGet("list")]
    public async Task<ActionResult<List<User>>> List()
    {
        var users = await _context.Users.ToListAsync();
        return users;
    }

    [HttpGet("{id}")]
     public async Task<ActionResult<User>> Get(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null)
        {
            return NotFound();
        }
        return Ok(user);
    }

    [HttpPut]
    public async Task<ActionResult<User>> Put(User userRequest)
    {
        if (userRequest == null || userRequest.UserId <= 0)
        {
            return BadRequest();
        }

         var existingUser = await _context.Users.FindAsync(userRequest.UserId);
        if (existingUser == null)
        {
            return NotFound();
        }

        existingUser.Username = userRequest.Username;
        existingUser.PasswordHash = userRequest.PasswordHash;

        await _context.SaveChangesAsync();

        return Ok(existingUser);
    }

    [HttpPost]
    public async Task<ActionResult<User>> Post(User userRequest)
    {
        if (userRequest == null)
        {
            return BadRequest();
        }

        _context.Users.Add(userRequest);
        await _context.SaveChangesAsync();
        return Ok(userRequest);
    }

    [HttpPost("authenticate")]
    public async Task<ActionResult<User>> Authenticate(User loginModel)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == loginModel.Username && u.PasswordHash == loginModel.PasswordHash);
        if (user == null)
        {
            return BadRequest();
        }

        return Ok(user);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<bool>> Delete(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null)
        {
            return NotFound();
        }
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        return Ok(true);
    }
}
