using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

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
  public async Task<IActionResult> DeleteUser(string id)
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

  [HttpPost("{id}/Order")]
  public async Task<IActionResult> UserOrderProduct(string id, OrderProductModel orderProduct)
  {
    try
    {
      if (!Guid.TryParse(id, out Guid userId) || !Guid.TryParse(orderProduct.ProductId, out Guid productId) || orderProduct.Quantity == 0)
      {
        return BadRequest();
      }
      // var selectedUser = RedirectToAction(nameof(GetUserById), userId);
      var selectedUser = await _context.User.FindAsync(userId);
      var selectedProudcted = await _context.Product.FindAsync(productId);
      if (selectedUser == null || selectedProudcted == null)
      {
        return NotFound();
      }

      if (selectedProudcted.Quantity < orderProduct.Quantity)
      {
        return StatusCode(400, "The current quantity of the item is less than the quantity you queried.");
      }

      var newORder = new OrderModel
      {
        Id = Guid.NewGuid(),
        userId = selectedUser.Id.ToString(),
        orderProductId = selectedProudcted.Id.ToString(),
        quantity = orderProduct.Quantity
      };

      _context.Order.Add(newORder);

      // if (selectedUser.OrderId == null)
      // {
      //   var orderList = new List<OrderModel> { newORder }; // 중괄호를 사용하여 리스트를 초기화
      //   selectedUser.OrderId = orderList;
      // }
      // else
      // {
      //   selectedUser.OrderId.Add(newORder);
      // }

      selectedProudcted.Quantity -= orderProduct.Quantity;


      await _context.SaveChangesAsync();

      return Ok(newORder);

    }
    catch (Exception error)
    {
      _logger.LogError(error, "Get Order list Error");
      return StatusCode(500, new { error = "Internal Server Error" });

    }
  }

  [HttpGet("{id}/Order")]
  public async Task<IActionResult> GetUserOrderList(string id)
  {
    try
    {
      if (!Guid.TryParse(id, out Guid userId))
      {
        return BadRequest();
      }

      var selectedUser = await _context.User.FindAsync(userId);
      if (selectedUser == null) return NotFound("Not Found User");

      var orderList = await _context.Order.Where(x => x.userId == id).ToListAsync();

      var userOrderModel = new UserOrderModel
      {
        Id = selectedUser.Id,
        Name = selectedUser.Name,
        Email = selectedUser.Email,
        OrderList = orderList
      };

      return Ok(userOrderModel);

    }
    catch (Exception error)
    {
      _logger.LogError(error, "Get Order list Error");
      return StatusCode(500, new { error = "Internal Server Error" });

    }
  }
}




