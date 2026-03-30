using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SV22T1020362.BusinessLayers;
using SV22T1020362.Models.Common;
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
        /// Trang lịch sử đơn hàng — kết quả load qua AJAX
        /// </summary>
        [Authorize]
        public IActionResult Index()
        {
            return View(); // Views/Order/Index.cshtml
        }

        /// <summary>
        /// Trả về partial HTML kết quả tìm kiếm đơn hàng (dùng cho AJAX từ Index)
        /// </summary>
        public async Task<IActionResult> Search(string searchValue = "", int page = 1, int pageSize = 10)
        {
            var userData = User.GetUserData();
            int customerID = int.Parse(userData!.UserId!);

            // Lấy tất cả đơn hàng của khách hàng rồi lọc phía server
            var allOrders = await SalesDataService.ListOrdersByCustomerAsync(customerID);

            // Lọc theo từ khóa (mã đơn hoặc trạng thái)
            if (!string.IsNullOrWhiteSpace(searchValue))
            {
                searchValue = searchValue.Trim().ToLower();
                allOrders = allOrders.Where(o =>
                    o.OrderID.ToString().Contains(searchValue) ||
                    o.Status.GetDescription().ToLower().Contains(searchValue) ||
                    (o.DeliveryAddress ?? "").ToLower().Contains(searchValue)
                ).ToList();
            }

            // Phân trang thủ công
            int rowCount = allOrders.Count;
            int pageCount = pageSize > 0 ? (int)Math.Ceiling((double)rowCount / pageSize) : 1;
            if (page < 1) page = 1;
            if (page > pageCount && pageCount > 0) page = pageCount;

            var paged = pageSize > 0
                ? allOrders.Skip((page - 1) * pageSize).Take(pageSize).ToList()
                : allOrders;

            // Đóng gói vào PagedResult để dùng GetDisplayPages()
            var result = new PagedResult<OrderViewInfo>
            {
                Page = page,
                PageSize = pageSize,
                RowCount = rowCount,
                DataItems = paged
            };

            return View(result); // Views/Order/Search.cshtml — Layout = null
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