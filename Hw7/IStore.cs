
public interface IStore
{
    public List<Product> GetAllProducts();
    public void AddProductToCart(Product product, int count);
    public void ViewShoppingCart();
    public void Checkout();
    public void ShowFactor(int userId);
    public void Increase(int amount);
    public void ConfirmOrders();
    public void AddQuntity(int idProduct, int count);
    public void RemoveProduct(int productid);
    public void ShowAllFactors();



}

