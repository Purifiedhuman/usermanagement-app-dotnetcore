using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using UserService.Data;
using UserService.Dtos.UserDto;
using UserService.Entities;
using UserService.Helpers.Pagination;

namespace UserService.Controllers;

[ApiController]
[Route("api/[controller]"), Authorize]
public class UserController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public UserController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<PagedList<ApplicationUser>>> GetUsers([FromQuery] GetUsersDto args)
    {
        var (page, pageSize) = (args.Page, args.PageSize);

        IQueryable<ApplicationUser> userQuery = _context.Users;

        if (!string.IsNullOrWhiteSpace(args.SearchUser))
        {
            userQuery = userQuery.Where(u => u.UserName.Contains(args.SearchUser.ToLower()));
        }

        if (args.isDeleted.HasValue)
        {
            userQuery = userQuery.Where(u => u.isDeleted == args.isDeleted);
        }

        if (!string.IsNullOrWhiteSpace(args.SortBy))
        {
            userQuery = args.SortOrder switch
            {
                "asc" => userQuery.OrderBy(GetSortProperty(args)),
                "desc" => userQuery.OrderByDescending(GetSortProperty(args)),
                _ => userQuery,
            };
        }

        var users = await PagedList<ApplicationUser>.CreateAsync(userQuery, page, pageSize);

        return users;
    }

    private static Expression<Func<ApplicationUser, object>> GetSortProperty(GetUsersDto args)
    {
        return args.SortBy?.ToLower() switch
        {
            "username" => u => u.UserName,
            "email" => u => u.Email,
            _ => u => u.Id,
        };
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ApplicationUser>> GetUser(int id)
    {
        var user = await _context.Users.FindAsync(id);

        if (user == null)
        {
            return NotFound();
        }

        return user;
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutUser(string id, ApplicationUser user)
    {
        if (id != user.Id)
        {
            return BadRequest();
        }

        _context.Entry(user).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!UserExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    [HttpPost]
    public async Task<ActionResult<ApplicationUser>> PostUser(ApplicationUser user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetUser", new { id = user.Id }, user);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool UserExists(string id)
    {
        return _context.Users.Any(e => e.Id == id);
    }

}
