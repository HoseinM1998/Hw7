
public static class Storage
{
    public static List<Product> Products { get; set; } = new List<Product>();
    public static List<User> Users { get; set; } = new List<User>();

    public static User OnlineUser { get; set; }

    static Storage()
    {
        Products.Add(new Product(1,"Samsung","S24",2024,"Black","Android 14/Camera 100 Pixel/2sim",400,3,TypeEnum.Mobile));
        Products.Add(new Product(2,"Samsung", "S23", 2023, "Red", "Android 13/Camera 50 Pixel/2sim", 300, 4, TypeEnum.Mobile));
        Products.Add(new Product(3,"Apple", "iphon14", 2024, "Black", "ios 15 14/Camera 12 Pixel/2sim", 600, 3, TypeEnum.Mobile));
        Products.Add(new Product(4,"Apple", "iphon13", 2023, "White", "ios 14 14/Camera 12 Pixel/1sim", 500, 3, TypeEnum.Mobile));

        Products.Add(new Product(5,"Lenovo", "LOQ", 2023, "Black", "i7 13/Ram 32/1T ssd/6GB RTX 4060", 1000, 1, TypeEnum.Laptop));
        Products.Add(new Product(6,"Lenovo", "Legion", 2024, "Gray", "i9 13/Ram 32/1T ssd/6GB RTX 4060", 1300, 2, TypeEnum.Laptop));
        Products.Add(new Product(7,"Macbook", "Pro MRX33", 2023, "Black", "M3/Ram 18/512 ssd", 1500, 4, TypeEnum.Laptop));
        Products.Add(new Product(8,"Msi", "Sword 16", 2024, "Gray", "i7 14/Ram 16+/1T ssd", 1200, 1, TypeEnum.Laptop));

        Products.Add(new Product(9,"X.vision", "50XC630", 2023, "Gray", "50Ench/LED/FuulHD/Android14", 400, 10, TypeEnum.Tv));
        Products.Add(new Product(10,"X.vision", "65XCU665", 2024, "Black", "650Ench/Ultra HD 4K/Android15", 700, 5, TypeEnum.Tv));
        Products.Add(new Product(11,"Samsung", "Q8030", 2024, "Gray", "98Ench/QLED/Ultra HD 4K/Android15", 1200, 2, TypeEnum.Tv));
        Products.Add(new Product(12,"Samsung", "65Q70C", 2023, "Black", "65Ench/QLED 4K/Android14", 900, 2, TypeEnum.Tv));



        Users.Add(new User(1,"Hosein Moslehi", "Hosein77", "hosein@gmail.com", "1234","Karaj",RoleEnum.manager));
        Users.Add(new User(2, "Hosein ", "H", "H", "H", "Karaj", RoleEnum.manager));

        Users.Add(new User(3,"Javad Moradi", "Javad76", "javad@gmail.com", "1234","Tehran",RoleEnum.user));
        Users.Add(new User(4, "h", "h", "h", "h", "Tehran", RoleEnum.user));
        Users.Add(new User(5, "j", "j", "j", "j", "Tehran", RoleEnum.user));
        Users.Add(new User(6, "k", "k", "k", "k", "Tehran", RoleEnum.user));




    }

}

