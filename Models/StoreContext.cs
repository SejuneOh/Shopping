using Microsoft.EntityFrameworkCore;

namespace Shopping;

public class StoreContext : DbContext
{
  // generator function
  public StoreContext(DbContextOptions<StoreContext> options) : base(options) { }

  public DbSet<UserModel> User { get; set; }
  // public DbSet<ProductsModel> Products { get; set; }
  public DbSet<ProductItemModel> Product { get; set; }
  public DbSet<OrderModel> Order { get; set; }
}
