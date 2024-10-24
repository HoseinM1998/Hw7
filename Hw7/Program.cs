using Colors.Net.StringColorExtensions;
using Colors.Net;
using System.Drawing;

Authentication authentication = new Authentication();
StoreService storeService = new StoreService();

int option;
do
{
    ColoredConsole.WriteLine("1. Login: ".Yellow());
    ColoredConsole.WriteLine("2. Register: ".Yellow());
    ColoredConsole.WriteLine("3. List Product: ".Yellow());
    ColoredConsole.WriteLine("4. Exit.".Red());

    option = Convert.ToInt32(Console.ReadLine());

    if (option == 1)
    {
        ColoredConsole.WriteLine("Enter UserName: ".Blue());
        string username = Console.ReadLine();
        ColoredConsole.WriteLine("Enter Password: ".Blue());
        string password = Console.ReadLine();

        if (authentication.UserLogin(username, password))
        {
            ColoredConsole.WriteLine("Welcome!".Green());
            int userId = Storage.OnlineUser.Id;
            var userRole = Storage.OnlineUser.Role;

            if (userRole == RoleEnum.user)
            {
                bool userExit = false;
                while (!userExit)
                {
                    ColoredConsole.WriteLine("*****************************".DarkGray());
                    ColoredConsole.WriteLine("1. List Product: ".DarkCyan());
                    ColoredConsole.WriteLine("2. Shopping Cart: ".DarkCyan());
                    ColoredConsole.WriteLine("3. Show Factor: ".DarkCyan());
                    ColoredConsole.WriteLine("4. Show Balance: ".DarkCyan());
                    ColoredConsole.WriteLine("5. Add Balance: ".DarkCyan());
                    ColoredConsole.WriteLine("6. Search Product: ".DarkCyan());
                    ColoredConsole.WriteLine("7. Logout".DarkRed());

                    int userOption = Convert.ToInt32(Console.ReadLine());

                    switch (userOption)
                    {
                        case 1:
                            var products = storeService.GetAllProducts();
                            foreach (var product in products)
                            {
                                ColoredConsole.WriteLine($"ID: {product.Id}, Name: {product.Name},Model:{product.Model}, Price: {product.Price}$, Quantity: {product.Quntity}".Cyan());
                            }

                            ColoredConsole.WriteLine("Enter Product ID to add to cart: ".Blue());
                            int productId = Convert.ToInt32(Console.ReadLine());

                            ColoredConsole.WriteLine("Enter Count Product: ".Blue());
                            int count = Convert.ToInt32(Console.ReadLine());

                            var selectedProduct = products.FirstOrDefault(p => p.Id == productId);
                            if (selectedProduct != null)
                            {
                                storeService.AddProductToCart(selectedProduct, count);
                            }
                            else
                            {
                                ColoredConsole.WriteLine("Invalid Product ID".Red());
                            }
                            break;

                        case 2:
                            storeService.ViewShoppingCart();
                            ColoredConsole.WriteLine("Do You Want To Pay? (yes/no)".Blue());

                            string userResponse = Console.ReadLine().ToLower();
                            if (userResponse == "yes" || userResponse == "y")
                            {
                                storeService.Checkout();
                            }
                            else
                            {
                                ColoredConsole.WriteLine("Returning To Menu.".Yellow());
                            }

                            break;


                        case 3:
                            storeService.ShowFactor(userId);

                            break;
                        case 4:
                            ColoredConsole.WriteLine($"Balance {Storage.OnlineUser.Balance}$".DarkBlue());
                            break;

                        case 5:
                            ColoredConsole.WriteLine($"Balance {Storage.OnlineUser.Balance}$".DarkBlue());
                            ColoredConsole.WriteLine("Enter The Balance Increase Amount".Blue());
                            int amount = Convert.ToInt32(Console.ReadLine());
                            storeService.Increase(amount);

                            break;

                        case 6:
                            Console.WriteLine("Enter Name Product: ");
                            string name = Console.ReadLine();
                            storeService.SearchProduct(name);
                            break;

                        case 7:
                            userExit = true;
                            break;
                        default:
                            ColoredConsole.WriteLine("Invalid Option".Red());
                            break;
                    }
                }
            }
            else if (userRole == RoleEnum.manager)
            {
                bool managerExit = false;
                while (managerExit == false)
                {
                    ColoredConsole.WriteLine("*****************************".DarkGray());
                    ColoredConsole.WriteLine("Welcome, Manager".Green());
                    ColoredConsole.WriteLine("1.Order Confirmation: \n2.Add Product: \n3.Changr Quntity Product: \n4.Reamove Product: \n5.View Factor All User:".DarkGray());

                    ColoredConsole.WriteLine("6.Logout".DarkRed());

                    int managerOption = Convert.ToInt32(Console.ReadLine());
                    switch (managerOption)
                    {
                        case 1:
                            storeService.ConfirmOrders();

                            break;

                        case 2:

                            ColoredConsole.WriteLine("Enter Product Name:".Blue());
                            string name = Console.ReadLine();

                            ColoredConsole.WriteLine("Enter Product Model:".Blue());
                            string model = Console.ReadLine();

                            ColoredConsole.WriteLine("Enter Year of Production:".Blue());
                            int year = int.Parse(Console.ReadLine());

                            ColoredConsole.WriteLine("Enter Product Color:".Blue());
                            string color = Console.ReadLine();

                            ColoredConsole.WriteLine("Enter Product Description:".Blue());
                            string description = Console.ReadLine();

                            ColoredConsole.WriteLine("Enter Product Price:".Blue());
                            int price = int.Parse(Console.ReadLine());

                            ColoredConsole.WriteLine("Enter Product Quantity:".Blue());
                            int quantity = int.Parse(Console.ReadLine());

                            ColoredConsole.WriteLine("Enter Type (Mobile: 0,Laptop: 1,Tv: 3): ".DarkYellow());
                            int type = Convert.ToInt32(Console.ReadLine());
                            var TypeEnum = (TypeEnum)type;
                            Product newProduct = new Product(Storage.Products.Count + 1, name, model, year, color, description, price, quantity, TypeEnum);
                            Storage.Products.Add(newProduct);
                            ColoredConsole.WriteLine($"Product {name} added successfully!".Green());
                            break;

                        case 3:
                            var products1 = storeService.GetAllProducts();
                            foreach (var product in products1)
                            {
                                ColoredConsole.WriteLine($"ID: {product.Id}, Name: {product.Name},Model:{product.Model}, Price: {product.Price}$, Quantity: {product.Quntity}".Cyan());
                            }
                            Console.WriteLine("Enter Product ID To Add Quntity");
                            int productIdAdd = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Entre Count To Add Quntity:");
                            int count = Convert.ToInt32(Console.ReadLine());
                            storeService.AddQuntity(productIdAdd, count);
                            break;

                            case 4:
                            var products = storeService.GetAllProducts();
                            foreach (var product in products)
                            {
                                ColoredConsole.WriteLine($"ID: {product.Id}, Name: {product.Name},Model:{product.Model}, Price: {product.Price}$, Quantity: {product.Quntity}".Cyan());
                            }
                            ColoredConsole.WriteLine("Enter Product ID to Remove:".Yellow());
                            int productIdRem = Convert.ToInt32(Console.ReadLine());
                            storeService.RemoveProduct(productIdRem);
                            break;
                            case 5:
                            storeService.ShowAllFactors();
                            break;
                           
                        case 6:
                            managerExit = true;
                            break;

                        default:
                            ColoredConsole.WriteLine("Invalid Option".Red());
                            break;
                    }
                }
            }
        }
        else
        {
            ColoredConsole.WriteLine("Invalid username or password.".Red());
        }
    }
    else if (option == 2)
    {
        ColoredConsole.WriteLine("Enter FullName: ".DarkYellow());
        string fullName = Console.ReadLine();
        ColoredConsole.WriteLine("Enter UserName: ".DarkYellow());
        string userName = Console.ReadLine();
        ColoredConsole.WriteLine("Enter Email: ".DarkYellow());
        string email = Console.ReadLine();
        ColoredConsole.WriteLine("Enter Password: ".DarkYellow());
        string password = Console.ReadLine();
        ColoredConsole.WriteLine("Enter Address: ".DarkYellow());
        string address = Console.ReadLine();

        if (authentication.UserRegister(fullName, userName, email, password, address))
        {
            ColoredConsole.WriteLine("Register Successful!".Green());
        }
        else
        {
            ColoredConsole.WriteLine("UserName Exists. Enter Another UserName".Red());
        }
    }
    else if (option == 3)
    {
        var products = storeService.GetAllProducts();
        foreach (var product in products)
        {
            ColoredConsole.WriteLine($"ID: {product.Id}, Name: {product.Name},Model:{product.Model}, Price: {product.Price}$, Quantity: {product.Quntity}".Cyan());
        }
    }
} while (option != 4);



