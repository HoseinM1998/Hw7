using Colors.Net;
using Colors.Net.StringColorExtensions;

public class StoreService : IStore
{
    public List<Product> GetAllProducts()
    {

        return Storage.Products;
    }

    public void AddProductToCart(Product product, int count)
    {
        if (product.Quntity >= count)
        {
            var cartProduct = Storage.OnlineUser.ShoppingCart.Products.FirstOrDefault(p => p.Id == product.Id);

            if (cartProduct != null)
            {
                cartProduct.Count += count;
            }
            else
            {
                product.Count = count;
                Storage.OnlineUser.ShoppingCart.Products.Add(product);
            }
            ColoredConsole.WriteLine($"{product.Type} Name {product.Name} {product.Model}Added To Shopping Cart".Green());
        }
        else
        {
            ColoredConsole.WriteLine($"This Number Of Products {product.Type}{product.Name} Not Available. Only {product.Quntity} ".Red());
        }
    }
    public void ViewShoppingCart()
    {

        if (Storage.OnlineUser == null)
        {
            ColoredConsole.WriteLine("User Is Not Logged In.".Red());
            return;
        }

        else if (Storage.OnlineUser.ShoppingCart.Products.Count == 0)
        {
            ColoredConsole.WriteLine("Shopping Cart Null".Yellow());
            return;
        }

        ColoredConsole.WriteLine("Shopping Cart:".Green());

        foreach (var product in Storage.OnlineUser.ShoppingCart.Products)
        {
            int productTotalPrice = product.Price * product.Count;
            ColoredConsole.WriteLine($"{product.Type}: Name: {product.Name}, Model: {product.Model}, Description: {product.Description}, Price: {product.Price}$, Count: {product.Count}, Total: {productTotalPrice}$".Cyan());
        }

        int totalPrice = Storage.OnlineUser.ShoppingCart.Products.Sum(p => p.Price * p.Count);
        ColoredConsole.WriteLine($"Total Price: {totalPrice}$".Green());


    }

    public void Checkout()
    {
        if (Storage.OnlineUser.ShoppingCart.Products.Count == 0)
        {
            ColoredConsole.WriteLine("Shopping Cart Null".Yellow());
            return;
        }

        int totalPrice = Storage.OnlineUser.ShoppingCart.Products.Sum(p => p.Price * p.Count);

        if (totalPrice > Storage.OnlineUser.Balance)
        {
            ColoredConsole.WriteLine("Insufficient Balance".Red());
            return;
        }

        Storage.OnlineUser.Balance -= totalPrice;
        ColoredConsole.WriteLine($"Checkout Successful|Total Price: {totalPrice}$| Account Balance: {Storage.OnlineUser.Balance}$".Green());
        var order = new Factor(new List<Product>(Storage.OnlineUser.ShoppingCart.Products), totalPrice);
        Storage.OnlineUser.Factor.Add(order);

        foreach (var product in Storage.OnlineUser.ShoppingCart.Products)
        {
            var productInStock = Storage.Products.FirstOrDefault(p => p.Id == product.Id);
            if (productInStock != null)
            {
                if (productInStock.Quntity >= product.Count)
                {
                    productInStock.Quntity -= product.Count;
                }
                else
                {
                    ColoredConsole.WriteLine($"Not Quntity {product.Name} count: {productInStock.Quntity}, Only: {product.Count}".Red());
                }
            }
            else
            {
                ColoredConsole.WriteLine($"Product {product.Name} Not Found.".Red());
            }
        }
        Storage.OnlineUser.ShoppingCart.Products.Clear();
    }

    public void ShowFactor(int userId)
    {
        if (Storage.OnlineUser.Factor.Count == 0)
        {
            ColoredConsole.WriteLine("No Factor".Yellow());
            return;
        }
        int count = 1;
        foreach (var factor in Storage.OnlineUser.Factor)
        {
            ColoredConsole.WriteLine($"Factor ID: {count}, Total Price: {factor.TotalAmount}$, DateTime:{factor.FactorDate}, OrderStatus:{factor.Status}".Green());
            count++;
            foreach (var product in factor.Products)
            {
                ColoredConsole.WriteLine($"{product.Type}: Name {product.Name}, Model: {product.Model}, Color: {product.Color}, Description: {product.Description}, Price: {product.Price}$, Count: {product.Count}".Cyan());
            }
        }
    }
    public void Increase(int amount)
    {
        Storage.OnlineUser.Balance += amount;
        ColoredConsole.WriteLine($"Successful|New Balace:{Storage.OnlineUser.Balance}$".Green());
    }
    public void SearchProduct(string name)
    {
        string search = name.ToLower();
        bool foundProduct = false;
        foreach (var product in Storage.Products)
        {
            if (product is not null && product.Name.ToLower().Contains(search))
            {
                ColoredConsole.WriteLine($"{product.Type}:Name {product.Name},Model{product.Model},Color {product.Count},Description {product.Description}, Price: {product.Price}$, Count: {product.Count}".Cyan());
                foundProduct = true;
            }
        }
        if (!foundProduct)
        {
            ColoredConsole.WriteLine("No Product Found.".DarkRed());
        }
    }



    public void ConfirmOrders()
    {
        List<Factor> confirmation = new List<Factor>();
        foreach (var user in Storage.Users)
        {
            foreach (var factor in user.Factor)
            {
                if (factor.Status == OrderStatus.Registered)
                {
                    confirmation.Add(factor);
                }
            }
        }
        if (confirmation.Count == 0)
        {
            ColoredConsole.WriteLine("No Unconfirmed".Yellow());
            return;
        }
        ColoredConsole.WriteLine("Unconfirmed Orders:".Green());
        for (int i = 0; i < confirmation.Count; i++)
        {
            var factor = confirmation[i];
            ColoredConsole.WriteLine($"{i + 1} Total Price: {factor.TotalAmount}$, Status: {factor.Status}. DataTime:{factor.FactorDate}".Cyan());
        }
        ColoredConsole.Write("Enter Number Of The Confirm: ".Green());
        int index = Convert.ToInt32(Console.ReadLine())-1;
        if (index >= confirmation.Count)
        {
            ColoredConsole.WriteLine("Invalid order number.".Red());
            return;
        }
        var selectedFactor = confirmation[index];
        selectedFactor.Status = OrderStatus.Confirmed;
        ColoredConsole.WriteLine($"Factor {selectedFactor.Id+1} Confirmed".Green());
    }



    public void AddQuntity(int idProduct, int count)
    {
        var product = Storage.Products.FirstOrDefault(p => p.Id == idProduct);
        if (product == null)
        {
            ColoredConsole.WriteLine("Product not found.".Red());
            return;
        }
        product.Quntity += count;
        ColoredConsole.WriteLine($"Product {product.Type} {product.Name} {product.Model}  Quntity {count} New quantity: {product.Quntity}".Green());
    }


    public void RemoveProduct(int productid)
    {
        foreach (var product in Storage.Products)
        {
            if (product.Id == productid)
            {
                Storage.Products.Remove(product);
                ColoredConsole.WriteLine($"Removed The Product:{product.Type} {product.Name} {product.Model}".Green());
                return;
            }
        }
        ColoredConsole.WriteLine("Product Not Found.".Red());
    }

    public void ShowAllFactors()
    {
        foreach (var user in Storage.Users)
        {
            if (user.Factor.Count > 0)
            {
                ColoredConsole.WriteLine($"User: {user.FullName} (ID: {user.Id})".Blue());
                int count = 1;
                foreach (var factor in user.Factor)
                {
                    ColoredConsole.WriteLine($"  Factor ID: {count}, Total Amount: {factor.TotalAmount}$, Date: {factor.FactorDate}, Status: {factor.Status}".Green());
                    count++;
                    foreach (var product in factor.Products)
                    {
                        ColoredConsole.WriteLine($"{product.Type}: Name {product.Name}, Model: {product.Model}, Color: {product.Color}, Description: {product.Description}, Price: {product.Price}$, Count: {product.Count}".Cyan());
                    }
                }
            }
        }
    }
}