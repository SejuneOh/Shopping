using System.ComponentModel.DataAnnotations;

namespace Shopping;





public class OrderModel
{
  public Guid Id { get; set; }
  [Required]
  public string userId { get; set; } = "";

  [Required]
  public string orderProductId { get; set; } = "";

  [Required]
  public int quantity { get; set; }

  public string OrderType { get; set; } = "";
}


public class UpdateOrderModel
{
  public string userId { get; set; } = "";

  [Required]
  public string orderProductId { get; set; } = "";

  [Required]
  public int quantity { get; set; }

  public string OrderType { get; set; } = "";

}