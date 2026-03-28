using Microsoft.AspNetCore.Mvc;
using SV22T1020362.BusinessLayers;
using SV22T1020362.Models.Sales;

namespace SV22T1020362.Shop.Controllers
{
    /// <summary>
    /// Quản lý giỏ hàng: thêm, sửa, xóa, đặt hàng
    /// </summary>
    public class CartController : Controller
    {
        /// <summary>
        /// Hiển thị giỏ hàng
        /// </summary>
        public IActionResult Index()
        {
            var cart = ShoppingCartHelper.GetCart();
            return View(cart);
        }

        /// <summary>
        /// Thêm sản phẩm vào giỏ hàng (POST từ trang chi tiết hoặc danh sách)
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> AddToCart(int productID, int quantity = 1)
        {
            var product = await CatalogDataService.GetProductAsync(productID);
            if (product == null)
                return Json(new { success = false, message = "Sản phẩm không tồn tại" });

            if (quantity < 1) quantity = 1;

            var item = new OrderDetailViewInfo
            {
                ProductID = productID,
                ProductName = product.ProductName,
                Unit = product.Unit,
                Photo = product.Photo ?? "nophoto.png",
                Quantity = quantity,
                SalePrice = product.Price
            };

            ShoppingCartHelper.AddItem(item);

            return Json(new
            {
                success = true,
                message = $"Đã thêm \"{product.ProductName}\" vào giỏ hàng",
                cartCount = ShoppingCartHelper.Count
            });
        }

        /// <summary>
        /// Cập nhật số lượng mặt hàng trong giỏ
        /// </summary>
        [HttpPost]
        public IActionResult UpdateItem(int productID, int quantity)
        {
            if (quantity < 1)
            {
                ShoppingCartHelper.RemoveItem(productID);
            }
            else
            {
                var item = ShoppingCartHelper.GetItem(productID);
                if (item != null)
                    ShoppingCartHelper.UpdateItem(productID, quantity, item.SalePrice);
            }

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Xóa 1 mặt hàng khỏi giỏ
        /// </summary>
        [HttpPost]
        public IActionResult RemoveItem(int productID)
        {
            ShoppingCartHelper.RemoveItem(productID);
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Xóa toàn bộ giỏ hàng
        /// </summary>
        [HttpPost]
        public IActionResult ClearCart()
        {
            ShoppingCartHelper.ClearCart();
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Lấy số lượng mặt hàng trong giỏ (dùng cho AJAX cập nhật badge)
        /// </summary>
        [HttpGet]
        public IActionResult GetCartCount()
            => Json(new { count = ShoppingCartHelper.Count });

        /// <summary>
        /// Trang xác nhận đặt hàng (hiển thị form địa chỉ giao)
        /// </summary>
        [Microsoft.AspNetCore.Authorization.Authorize]
        [HttpGet]
        public async Task<IActionResult> Checkout()
        {
            var cart = ShoppingCartHelper.GetCart();
            if (!cart.Any())
                return RedirectToAction("Index");

            // Điền sẵn địa chỉ từ thông tin khách hàng
            var userData = User.GetUserData();
            int customerID = int.Parse(userData!.UserId!);
            var customer = await PartnerDataService.GetCustomerAsync(customerID);

            // Lấy danh sách tỉnh thành
            var provinces = await DictionaryDataService.ListProvincesAsync();

            ViewBag.Customer = customer;
            ViewBag.Cart = cart;
            ViewBag.Provinces = provinces;
            return View();
        }

        /// <summary>
        /// Đặt hàng: tạo đơn hàng từ giỏ hàng
        /// </summary>
        [Microsoft.AspNetCore.Authorization.Authorize]
        [HttpPost]
        public async Task<IActionResult> PlaceOrder(string deliveryProvince, string deliveryAddress)
        {
            var cart = ShoppingCartHelper.GetCart();
            if (!cart.Any())
                return RedirectToAction("Index");

            var userData = User.GetUserData();
            int customerID = int.Parse(userData!.UserId!);

            try
            {
                // Tạo đơn hàng mới với trạng thái New
                int orderID = await SalesDataService.AddOrderAsync(
                    customerID,
                    deliveryProvince ?? "",
                    deliveryAddress ?? "");

                // Thêm từng mặt hàng vào chi tiết đơn
                foreach (var item in cart)
                {
                    await SalesDataService.AddDetailAsync(new OrderDetail
                    {
                        OrderID = orderID,
                        ProductID = item.ProductID,
                        Quantity = item.Quantity,
                        SalePrice = item.SalePrice
                    });
                }

                // Xóa giỏ hàng sau khi đặt thành công
                ShoppingCartHelper.ClearCart();

                TempData["OrderSuccess"] = orderID;
                return RedirectToAction("OrderSuccess", new { id = orderID });
            }
            catch (Exception ex)
            {
                TempData["OrderError"] = ex.Message;
                return RedirectToAction("Checkout");
            }
        }

        /// <summary>
        /// Trang thông báo đặt hàng thành công
        /// </summary>
        [Microsoft.AspNetCore.Authorization.Authorize]
        public async Task<IActionResult> OrderSuccess(int id)
        {
            var order = await SalesDataService.GetOrderAsync(id);
            if (order == null)
                return RedirectToAction("Index", "Home");

            var details = await SalesDataService.ListDetailsAsync(id);
            ViewBag.Details = details;
            return View(order);
        }
    }
}