namespace Shopping;


public class OrderModel
{
  public Guid Id { get; set; }
  public List<ProductItemModel>? ProductId { get; set; } = null;

  public string OrderType { get; set; }
}