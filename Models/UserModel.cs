using System.ComponentModel.DataAnnotations;

namespace Shopping;


public class UserModel
{


  public UserModel() { }
  public UserModel(Guid _id, string _name, string _email)
  {
    Id = _id;
    Name = _name;
    Email = _email;
  }

  public UserModel(CreateUserModel _newUser)
  {
    Id = Guid.NewGuid();
    Name = _newUser.Name;
    Email = _newUser.Email;
  }
  public Guid Id { get; set; }
  public List<OrderModel>? OrderId { get; set; } = null;

  [Required]
  public string Name { get; set; } = "";
  public string? Email { get; set; }

}



public class CreateUserModel
{
  public string Name { get; set; } = "";
  public string? Email { get; set; }
}