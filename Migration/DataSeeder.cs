using Shopping;

public interface IDataSeeder
{
  void SeedData();
}

public class DataSeeder : IDataSeeder
{
  private readonly StoreContext _dbContext;

  public DataSeeder(StoreContext dbContext)
  {
    _dbContext = dbContext;
  }

  public void SeedData()
  {
    // 여기에서 데이터베이스에 초기 데이터를 추가하는 코드 작성
    if (!_dbContext.User.Any())
    {
      var seedData = new List<UserModel>{
        new UserModel{Id = Guid.NewGuid(), Name="Kim Chul Su",Email = "test1@email.com", OrderId= null},
        new UserModel{Id = Guid.NewGuid(), Name="Hong Kill Dong",Email = "test2@email.com", OrderId= null},
        new UserModel{Id = Guid.NewGuid(), Name="Park Dong Eun",Email = "test1@email.com", OrderId= null},
    };

      _dbContext.User.AddRange(seedData);
      _dbContext.SaveChanges();

    }

    if (!_dbContext.Product.Any())
    {
      var productSeedData = new List<ProductItemModel>{
        new ProductItemModel {Id = Guid.NewGuid(), Name = "TV", Price= 120, quantity = 10},
        new ProductItemModel {Id = Guid.NewGuid(), Name = "Microwave", Price= 90, quantity = 5},
        new ProductItemModel {Id = Guid.NewGuid(), Name = "Monitor", Price= 100, quantity = 12},
        new ProductItemModel {Id = Guid.NewGuid(), Name = "Wallet", Price= 30, quantity = 4},
      };

      _dbContext.Product.AddRange(productSeedData);
      _dbContext.SaveChanges();
    }
  }
}
