
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Model { get; set; }
    public int Year {  get; set; }
    public string Color { get; set; }
    public string Description { get; set; }
    public int Quntity { get; set; }
    public int Count {  get; set; }
    public int Price { get;private set; }
    
    public TypeEnum Type { get; set; }
    public int UserId { get; set; }

    public void setPrice(int price)
    {
        Price = price;
    }

    public Product() 
    {
        Count = 1;
    }
    public Product(int id,string name ,string model,int year, string color, string description,int price,int quntity,TypeEnum type)
    {
        Id = id;
        Name = name;
        Model = model;
        Year = year;
        Color = color;
        Description = description;
        setPrice(price);
        Quntity = quntity;
        Type = type;
        Count = 1;
    }
 
}

