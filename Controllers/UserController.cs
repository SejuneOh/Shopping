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


}