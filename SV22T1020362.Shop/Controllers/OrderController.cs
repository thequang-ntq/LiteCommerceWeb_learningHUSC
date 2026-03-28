using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SV22T1020362.BusinessLayers;
using SV22T1020362.Models.Sales;

namespace SV22T1020362.Shop.Controllers
{
    /// <summary>
    /// Theo dõi trạng thái và lịch sử đơn hàng của khách hàng
    /// </summary>
    [Authorize]
    public class OrderController : Controller
    {
        /// <summary>
        /// Lịch sử đơn hàng của khách hàng đang đăng nhập
        /// </summary>
        public async Task<IActionResult> Index()
        {
            var userData = User.GetUserData();
            int customerID = int.Parse(userData!.UserId!);

            var orders = await SalesDataService.ListOrdersByCustomerAsync(customerID);
            return View(orders);
        }

        /// <summary>
        /// Chi tiết một đơn hàng của khách hàng
        /// </summary>
        public async Task<IActionResult> Detail(int id)
        {
            var userData = User.GetUserData();
            int customerID = int.Parse(userData!.UserId!);

            var order = await SalesDataService.GetOrderAsync(id);

            // Chỉ cho xem đơn hàng của chính mình
            if (order == null || order.CustomerID != customerID)
                return RedirectToAction("Index");

            var details = await SalesDataService.ListDetailsAsync(id);
            ViewBag.Details = details;
            return View(order);
        }
    }
}