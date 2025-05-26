using ETR_IPT3D_TEAM3.Models;
using System.Text.Json;
namespace ETR_IPT3D_TEAM3.Services
{
    public class CartService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CartService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public List<Product> GetCart()
        {
            var cart = _httpContextAccessor.HttpContext.Session.GetString("Cart");
            return cart != null ? JsonSerializer.Deserialize<List<Product>>(cart) : new List<Product>();
        }

        public void AddToCart(Product product)
        {
            var cart = GetCart();
            cart.Add(product);
            _httpContextAccessor.HttpContext.Session.SetString("Cart", JsonSerializer.Serialize(cart));
        }
    }

}
