namespace Shopping;


public class ProductItemModel
{
  public Guid Id { get; set; }
  public string Name { get; set; } = "";
  public int Price { get; set; }
  public int quantity { get; set; }

  public string? OrderedId { get; set; } = null;
}