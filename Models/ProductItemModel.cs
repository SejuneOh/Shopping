using System.ComponentModel.DataAnnotations;

namespace Shopping;


public class ProductItemModel
{
  public ProductItemModel() { }

  public ProductItemModel(Guid _Id, string _Name, int _Price, int _Quantity)
  {
    Id = _Id;
    Name = _Name;
    Price = _Price;
    Quantity = _Quantity;

  }
  public ProductItemModel(string name, int price, int quantity)
  {
    Name = name;
    Price = price;
    Quantity = quantity;
  }

  public ProductItemModel(CreateProductModel newProduct)
  {
    if (newProduct != null)
    {
      Id = Guid.NewGuid();
      Name = newProduct.Name;
      Price = newProduct.Price;
      Quantity = newProduct.Quantity;
    }
  }


  public Guid Id { get; set; }
  public string Name { get; set; } = "";
  public int Price { get; set; }
  public int Quantity { get; set; }
  // public string? OrderedId { get; set; } = null;
}


public class CreateProductModel
{
  public string Name { get; set; } = "";
  public int Price { get; set; }
  public int Quantity { get; set; }
}

public class UpdateProductModel : CreateProductModel { }
public class OrderProductModel
{
  [Required]
  public string ProductId { get; set; } = "";
  public int Quantity { get; set; }
}



