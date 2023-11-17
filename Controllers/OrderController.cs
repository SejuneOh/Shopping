using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Shopping;


[ApiController]
[Route("api/[controller]")]
public class OdersController : ControllerBase
{
  private readonly ILogger<UsersController> _logger;
  private readonly StoreContext _context;

  public OdersController(ILogger<UsersController> logger, StoreContext context)
  {
    _logger = logger;
    _context = context;
  }


  [HttpGet]
  public async Task<ActionResult<IEnumerable<OrderModel>>> GetOrderList()
  {
    try
    {
      var list = await _context.Order.ToListAsync();
      if (list == null) return NotFound();

      return list;
    }
    catch (Exception error)
    {
      _logger.LogError(error, "Get Order list Error");
      return StatusCode(500, new { error = "Internal Server Error" });
    }
  }

  [HttpPost("{userId}")]
  public async Task<ActionResult<OrderModel>> PostOrder([FromQuery] string _userId, [FromBody] OrderModel _order)
  {
    try
    {
      // 사용자
      var orderUser = await _context.User.FindAsync(_userId);
      if (orderUser == null || _order == null) return BadRequest();

      ProductItemModel? selectedProduct = await _context.Product.FindAsync(_order.orderProductId);

      if (selectedProduct == null) return NotFound();
      if (selectedProduct.quantity <= 0) return StatusCode(200, "No quantity left.");

      selectedProduct.quantity -= 1;

      _context.Order.Add(_order);

      await _context.SaveChangesAsync();

      return Ok(_order);
    }
    catch (Exception error)
    {
      _logger.LogError(error, "Get Order list Error");
      return StatusCode(500, new { error = "Internal Server Error" });
    }
  }

}


