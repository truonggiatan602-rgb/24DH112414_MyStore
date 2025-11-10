using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _24DH112414_MyStore.Models;
using _24DH112414_MyStore.Models.ViewModel;


namespace _24DH112414_MyStore.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        private MyStoreEntities1 db = new MyStoreEntities1();

        //Hàm lấy dịch vị giỏ hàng
        private CartService GetCartService()
        {
            return new CartService(Session);
        }

        //Hiển thị giỏ hàng không gom nhóm theo danh mục
        public ActionResult Index()
        {
            var cart = GetCartService().GetCart();
            return View(cart);
        }

        //Thêm sản phẩm vào giỏ
        public ActionResult AddToCart(int id, int quantity = 1)
        {
            var product = db.Products.Find(id);
            if (product != null)
            {
                var cartService = GetCartService();
                cartService.GetCart().AddItem(product.ProductID, product.ProductImage,
                    product.ProductName, product.ProductPrice, quantity, product.Category.CategoryName);
            }
            return RedirectToAction("Index");
        }

        //Xóa sản phẩm khỏi giỏ
        public ActionResult RemoveFromCart(int id)
        {
            var cartService = GetCartService();
            cartService.GetCart().RemoveItem(id);
            return RedirectToAction("Index");
        }

        //Làm trống giỏ hàng
        public ActionResult ClearCart()
        {
            var cartService = ClearCart();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult UpdateQuantity(int id, int quantity)
        {
            var cartService = GetCartService();
            cartService.GetCart().UpdateQuantity(id, quantity);
            return RedirectToAction("Index");
        }
    }
}