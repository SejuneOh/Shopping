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


  [HttpPost()]
  public async Task<ActionResult<OrderModel>> PostOrder([FromBody] OrderModel newOrder)
  {
    try
    {

      // 사용자
      var orderUser = await _context.User.FindAsync(newOrder.userId);
      if (orderUser == null) return BadRequest();

      ProductItemModel? selectedProduct = await _context.Product.FindAsync(newOrder.orderProductId);

      if (selectedProduct == null) return NotFound();
      if (selectedProduct.Quantity <= 0) return StatusCode(200, "No quantity left.");

      selectedProduct.Quantity -= 1;

      _context.Order.Add(newOrder);

      await _context.SaveChangesAsync();

      return Ok(newOrder);
    }
    catch (Exception error)
    {
      _logger.LogError(error, "Get Order list Error");
      return StatusCode(500, new { error = "Internal Server Error" });
    }
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


  [HttpGet("{id}")]
  public async Task<ActionResult<OrderModel>> GetOrderById(Guid id)
  {
    var selectedOrder = await _context.Order.FindAsync(id);

    if (selectedOrder == null)
    {
      return NotFound();
    }
    return selectedOrder;
  }




  [HttpPut("{id}")]
  public async Task<IActionResult> UpdateOrder(string id, UpdateOrderModel updateUserData)
  {
    try
    {
      if (!Guid.TryParse(id, out Guid originGuid) || updateUserData == null)
      {
        return BadRequest();
      }

      var selectedOrder = await _context.Order.FindAsync(originGuid);
      if (selectedOrder == null) return NotFound();



      _context.Entry(selectedOrder).State = EntityState.Modified;

      await _context.SaveChangesAsync();
      return Ok(selectedOrder);

    }
    catch (Exception error)
    {
      _logger.LogError(error, "Get Order list Error");
      return StatusCode(500, new { error = "Internal Server Error" });

    }
  }

  [HttpDelete("{id}")]
  public async Task<IActionResult> DeleteOrder(string id)
  {

    try
    {
      if (!Guid.TryParse(id, out Guid searchId))
      {
        return BadRequest();
      }

      var selectedOrder = await _context.Order.FindAsync(searchId);

      if (selectedOrder == null) return NotFound();

      _context.Order.Remove(selectedOrder);
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



