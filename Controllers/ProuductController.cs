
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Shopping;

[ApiController]
[Route("api/[controller]")]

public class ProductsController : ControllerBase
{
  private readonly ILogger<UsersController> _logger;
  private readonly StoreContext _context;

  public ProductsController(ILogger<UsersController> logger, StoreContext context)
  {
    _logger = logger;
    _context = context;
  }

  [HttpGet]
  public async Task<ActionResult<List<ProductItemModel>>> GetProductList()
  {
    try
    {
      var productList = await _context.Product.ToListAsync();
      if (productList == null) return NotFound();

      return productList;
    }
    catch (Exception error)
    {
      _logger.LogError(error, "Get Order list Error");
      return StatusCode(500, new { error = "Internal Server Error" });
    }
  }

  [HttpGet("{id}")]
  public async Task<ActionResult<ProductItemModel>> GetProductById(string id)
  {
    try
    {
      // compare GUID
      if (!Guid.TryParse(id, out Guid productId)) return BadRequest();

      ProductItemModel? selectedProduct = await _context.Product.FindAsync(productId);
      if (selectedProduct == null) return NotFound();

      return Ok(selectedProduct);
    }
    catch (Exception error)
    {
      _logger.LogError(error, "Get Order list Error");
      return StatusCode(500, new { error = "Internal Server Error" });
    }
  }

  [HttpPost]
  public async Task<IActionResult> AddProduct(CreateProductModel newProduct)
  {
    try
    {
      if (newProduct == null) return BadRequest();

      var product = new ProductItemModel(newProduct);
      _context.Product.Add(product);

      await _context.SaveChangesAsync();

      return Ok(newProduct);

    }
    catch (Exception error)
    {
      _logger.LogError(error, "Get Order list Error");
      return StatusCode(500, new { error = "Internal Server Error" });

    }
  }
}
  }
}
