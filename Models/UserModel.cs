using System.ComponentModel.DataAnnotations;

namespace Shopping;


public class UserModel
{
  public Guid Id { get; set; }
  public List<OrderModel>? OrderId { get; set; } = null;

  [Required]
  public string Name { get; set; } = "";
  public string? Email { get; set; }

}