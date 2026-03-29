using Microsoft.AspNetCore.Mvc;
using SV22T1020362.BusinessLayers;
using SV22T1020362.Models.Sales;

namespace SV22T1020362.Shop.Controllers
{
    /// <summary>
    /// Quản lý giỏ hàng.
    /// - Khách chưa đăng nhập: lưu trong Session
    /// - Khách đã đăng nhập: lưu trong CSDL (Orders Status=0)
    /// </summary>
    public class CartController : Controller
    {
        #region Hiển thị giỏ hàng

        /// <summary>
        /// Hiển thị giỏ hàng.
        /// Nếu đã đăng nhập → lấy từ CSDL; nếu chưa → lấy từ Session.
        /// </summary>
        public async Task<IActionResult> Index()
        {
            var cart = await GetCurrentCart();
            return View(cart);
        }

        #endregion

        #region Thêm / sửa / xóa mặt hàng trong giỏ

        /// <summary>
        /// Thêm sản phẩm vào giỏ hàng (AJAX POST).
        /// Nếu đã đăng nhập → lưu CSDL; nếu chưa → lưu Session.
        /// Trả về JSON.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> AddToCart(int productID, int quantity = 1)
        {
            var product = await CatalogDataService.GetProductAsync(productID);
            if (product == null)
                return Json(new { success = false, message = "Sản phẩm không tồn tại" });

            if (quantity < 1) quantity = 1;

            if (User.Identity?.IsAuthenticated == true)
            {
                // Đã đăng nhập → lưu vào CSDL
                var userData = User.GetUserData();
                int customerID = int.Parse(userData!.UserId!);
                await SalesDataService.AddItemToCartDBAsync(customerID, productID, quantity, product.Price);

                // Cập nhật badge giỏ hàng từ CSDL
                var dbItems = await SalesDataService.GetCartItemsFromDBAsync(customerID);
                return Json(new
                {
                    success = true,
                    message = $"Đã thêm \"{product.ProductName}\" vào giỏ hàng",
                    cartCount = dbItems.Count
                });
            }
            else
            {
                // Chưa đăng nhập → lưu vào Session
                var item = new OrderDetailViewInfo
                {
                    ProductID = productID,
                    ProductName = product.ProductName,
                    Unit = product.Unit,
                    Photo = product.Photo ?? "nophoto.png",
                    Quantity = quantity,
                    SalePrice = product.Price
                };
                ShoppingCartHelper.AddSessionItem(item);

                return Json(new
                {
                    success = true,
                    message = $"Đã thêm \"{product.ProductName}\" vào giỏ hàng",
                    cartCount = ShoppingCartHelper.SessionCount
                });
            }
        }

        /// <summary>
        /// Cập nhật số lượng mặt hàng trong giỏ (POST form).
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> UpdateItem(int productID, int quantity)
        {
            if (User.Identity?.IsAuthenticated == true)
            {
                var userData = User.GetUserData();
                int customerID = int.Parse(userData!.UserId!);

                if (quantity < 1)
                    await SalesDataService.RemoveCartItemFromDBAsync(customerID, productID);
                else
                {
                    var cartItems = await SalesDataService.GetCartItemsFromDBAsync(customerID);
                    var existing = cartItems.Find(x => x.ProductID == productID);
                    decimal salePrice = existing?.SalePrice ?? 0;
                    await SalesDataService.UpdateCartItemInDBAsync(customerID, productID, quantity, salePrice);
                }
            }
            else
            {
                if (quantity < 1)
                    ShoppingCartHelper.RemoveSessionItem(productID);
                else
                {
                    var item = ShoppingCartHelper.GetSessionItem(productID);
                    if (item != null)
                        ShoppingCartHelper.UpdateSessionItem(productID, quantity, item.SalePrice);
                }
            }

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Xóa 1 mặt hàng khỏi giỏ (POST form).
        /// Nếu sau khi xóa giỏ DB trống thì xóa luôn đơn hàng Status=0.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> RemoveItem(int productID)
        {
            if (User.Identity?.IsAuthenticated == true)
            {
                var userData = User.GetUserData();
                int customerID = int.Parse(userData!.UserId!);

                // Xóa mặt hàng khỏi giỏ CSDL
                await SalesDataService.RemoveCartItemFromDBAsync(customerID, productID);

                // Nếu giỏ hàng rỗng sau khi xóa → xóa luôn đơn hàng Status=0
                var remaining = await SalesDataService.GetCartItemsFromDBAsync(customerID);
                if (!remaining.Any())
                    await SalesDataService.DeleteCartAsync(customerID);
            }
            else
            {
                ShoppingCartHelper.RemoveSessionItem(productID);
            }
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Xóa toàn bộ giỏ hàng (POST form).
        /// Nếu đã đăng nhập → xóa cả record trong CSDL.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> ClearCart()
        {
            if (User.Identity?.IsAuthenticated == true)
            {
                var userData = User.GetUserData();
                int customerID = int.Parse(userData!.UserId!);
                await SalesDataService.DeleteCartAsync(customerID);
            }
            else
            {
                ShoppingCartHelper.ClearSessionCart();
            }
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Lấy số lượng mặt hàng trong giỏ (AJAX GET, dùng cho badge header).
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetCartCount()
        {
            int count;
            if (User.Identity?.IsAuthenticated == true)
            {
                var userData = User.GetUserData();
                int customerID = int.Parse(userData!.UserId!);
                var items = await SalesDataService.GetCartItemsFromDBAsync(customerID);
                count = items.Count;
            }
            else
            {
                count = ShoppingCartHelper.SessionCount;
            }
            return Json(new { count });
        }

        #endregion

        #region Đặt hàng (Checkout)

        /// <summary>
        /// Trang xác nhận đặt hàng - yêu cầu đã đăng nhập.
        /// </summary>
        [Microsoft.AspNetCore.Authorization.Authorize]
        [HttpGet]
        public async Task<IActionResult> Checkout()
        {
            var userData = User.GetUserData();
            int customerID = int.Parse(userData!.UserId!);

            var cart = await SalesDataService.GetCartItemsFromDBAsync(customerID);
            if (!cart.Any())
                return RedirectToAction("Index");

            var customer = await PartnerDataService.GetCustomerAsync(customerID);
            var provinces = await DictionaryDataService.ListProvincesAsync();

            // Lấy địa chỉ giao hàng đã lưu (nếu có) từ giỏ hàng CSDL
            var cartOrder = await SalesDataService.GetCartOrderAsync(customerID);

            ViewBag.Customer = customer;
            ViewBag.Cart = cart;
            ViewBag.Provinces = provinces;
            ViewBag.CartOrder = cartOrder;
            return View();
        }

        /// <summary>
        /// Đặt hàng: chuyển giỏ hàng (Status=0) thành đơn hàng thực (Status=1).
        /// Yêu cầu đã đăng nhập.
        /// </summary>
        [Microsoft.AspNetCore.Authorization.Authorize]
        [HttpPost]
        public async Task<IActionResult> PlaceOrder(string deliveryProvince, string deliveryAddress)
        {
            var userData = User.GetUserData();
            int customerID = int.Parse(userData!.UserId!);

            var cart = await SalesDataService.GetCartItemsFromDBAsync(customerID);
            if (!cart.Any())
                return RedirectToAction("Index");

            try
            {
                // Lấy OrderID của giỏ hàng hiện tại
                var cartOrder = await SalesDataService.GetCartOrderAsync(customerID);
                if (cartOrder == null)
                    throw new InvalidOperationException("Không tìm thấy giỏ hàng.");

                // Chuyển Status từ 0 → 1, cập nhật địa chỉ giao hàng
                bool ok = await SalesDataService.ConfirmCartAsync(
                    cartOrder.OrderID,
                    deliveryProvince ?? "",
                    deliveryAddress ?? "");

                if (!ok)
                    throw new InvalidOperationException("Không thể đặt hàng. Vui lòng thử lại.");

                int orderID = cartOrder.OrderID;
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
        /// Trang thông báo đặt hàng thành công.
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

        #endregion

        #region Helper

        /// <summary>
        /// Lấy danh sách mặt hàng trong giỏ hiện tại.
        /// Đã đăng nhập → CSDL; chưa → Session.
        /// </summary>
        private async Task<List<OrderDetailViewInfo>> GetCurrentCart()
        {
            if (User.Identity?.IsAuthenticated == true)
            {
                var userData = User.GetUserData();
                int customerID = int.Parse(userData!.UserId!);
                return await SalesDataService.GetCartItemsFromDBAsync(customerID);
            }
            return ShoppingCartHelper.GetSessionCart();
        }

        #endregion
    }
}