using ETR_IPT3D_TEAM3.Models;
using ETR_IPT3D_TEAM3.Services;
using Microsoft.AspNetCore.Mvc;

namespace ETR_IPT3D_TEAM3.Controllers
{
    public class CartController : Controller
    {
        private readonly CartService _cartService;

        public CartController(CartService cartService)
        {
            _cartService = cartService;
        }

        public IActionResult Index()
        {
            var cartItems = _cartService.GetCart();
            return View(cartItems);
        }

        public IActionResult AddToCart(int productId)
        {
            var product = new Product();
            _cartService.AddToCart(product);
            return RedirectToAction("Index");
        }

        public IActionResult Checkout()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View();
            }

            TempData["Message"] = "Please log in or register to complete your purchase.";
            return RedirectToAction("Login", "Account");
        }

    }

}
