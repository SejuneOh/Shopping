namespace Shopping;


public class ProductItemModel
{

  public ProductItemModel(CreateProductModel newProduct)
  {
    if (newProduct != null)
    {
      Id = Guid.NewGuid();
      Name = newProduct.Name;
      Price = newProduct.Price;
      quantity = newProduct.quantity;
    }
  }
  public Guid Id { get; set; }
  public string Name { get; set; } = "";
  public int Price { get; set; }
  public int quantity { get; set; }
  // public string? OrderedId { get; set; } = null;
}


public class CreateProductModel : ProductItemModel
{
  public CreateProductModel(CreateProductModel newProduct) : base(newProduct)
  {
  }
}



