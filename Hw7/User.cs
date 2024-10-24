using System.Data;

public class User
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; private set; }
    public string Addres { get; set; }
    public int Balance { get; set; } 
    public RoleEnum Role { get; set; }
    public ShopingCart ShoppingCart { get; set; }

    public List<Factor> Factor = new List<Factor>(); 

    public void SetPassword(string password)
    {
        Password = password;
    }
    public User(string fullName, string userName, string email, string password, string addres) 
    {
        FullName = fullName;
        UserName = userName;
        Email = email;
        Password = password;
        Addres = addres;
        Balance = 10000;
        Role = RoleEnum.user;
        ShoppingCart = new ShopingCart();
    }

    public User(int id,string fullName,string userName,string email,string password,string addres,RoleEnum role)
    {
        Id = id;
        FullName = fullName;
        UserName=userName;
        Email=email;
        Password=password;
        Addres = addres;
        Balance = 10000;
        Role = role;
        ShoppingCart = new ShopingCart();

    }

}
