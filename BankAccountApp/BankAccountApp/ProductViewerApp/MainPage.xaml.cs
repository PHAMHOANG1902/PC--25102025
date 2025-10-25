using ProductViewerApp.Models;

namespace ProductViewerApp;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();

        var products = new List<Product>
        {
            new Product { Name = "iPhone 15", Category = "Điện thoại", Price = 25990000 },
            new Product { Name = "MacBook Air M3", Category = "Laptop", Price = 32990000 },
            new Product { Name = "AirPods Pro 2", Category = "Phụ kiện", Price = 5990000 }
        };

        ProductList.ItemsSource = products;
    }
}
