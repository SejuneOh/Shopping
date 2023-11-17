using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Shopping;


[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
  // logger

  private readonly ILogger<UsersController> _logger;
  private readonly StoreContext _context;

  public UsersController(ILogger<UsersController> logger, StoreContext context)
  {
    _logger = logger;
    _context = context;
  }

  // Get Users
  [HttpGet]
  public async Task<ActionResult<IEnumerable<UserModel>>> GetUsers()
  {
    return await _context.User.ToListAsync();
  }


  [HttpGet("{id}")]
  public async Task<ActionResult<UserModel>> GetUserById(Guid id)
  {
    var selectedUser = await _context.User.FindAsync(id);

    if (selectedUser == null)
    {
      return NotFound();
    }
    return selectedUser;
  }


  [HttpPost]
  public async Task<ActionResult<UserModel>> CreateUser(CreateUserModel newUser)
  {
    try
    {
      if (newUser == null) return BadRequest();

      var generateUser = new UserModel(newUser);

      _context.User.Add(generateUser);
      await _context.SaveChangesAsync();

      return Ok(generateUser);

    }
    catch (Exception error)
    {
      _logger.LogError(error, "Get Order list Error");
      return StatusCode(500, new { error = "Internal Server Error" });

    }
  }

  [HttpPut("{id}")]
  public async Task<IActionResult> UpdateUser(string id, UserModel updateUserData)
  {
    try
    {
      if (!Guid.TryParse(id, out Guid originGuid) || id != updateUserData.Id.ToString())
      {
        return BadRequest();
      }

      _context.Entry(updateUserData).State = EntityState.Modified;

      await _context.SaveChangesAsync();
      return Ok(updateUserData);

    }
    catch (Exception error)
    {
      _logger.LogError(error, "Get Order list Error");
      return StatusCode(500, new { error = "Internal Server Error" });

    }
  }

  [HttpDelete("{id}")]
  public async Task<IActionResult> DeleteTodoItem(string id)
  {

    try
    {
      if (!Guid.TryParse(id, out Guid searchId))
      {
        return BadRequest();
      }

      var selectedUser = await _context.User.FindAsync(searchId);

      if (selectedUser == null) return NotFound();

      _context.User.Remove(selectedUser);
      await _context.SaveChangesAsync();

      return NoContent();
    }
    catch (Exception error)
    {
      _logger.LogError(error, "Get Order list Error");
      return StatusCode(500, new { error = "Internal Server Error" });
    }
  }
}


