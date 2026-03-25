using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SV22T1020362.BusinessLayers;
using SV22T1020362.Models.Sales;

namespace SV22T1020362.Admin.Controllers
{
    /// <summary>
    /// Các chức năng liên quan đến quản lý đơn hàng.
    /// Workflow xử lý đơn hàng sử dụng modal dialog (open-modal pattern).
    /// </summary>
    [Authorize(Roles = $"{WebUserRoles.Sales},{WebUserRoles.Administrator}")]
    public class OrderController : Controller
    {
        public const int PAGESIZE = 20;
        public const string SEARCH_ORDER = "SearchOrder";

        #region Order List & Search

        /// <summary>
        /// Giao diện nhập đầu vào tìm kiếm đơn hàng
        /// </summary>
        public IActionResult Index()
        {
            var input = ApplicationContext.GetSessionData<OrderSearchInput>(SEARCH_ORDER);
            if (input == null)
                input = new OrderSearchInput()
                {
                    Page = 1,
                    PageSize = PAGESIZE,
                    SearchValue = "",
                    Status = 0,
                    DateFrom = null,
                    DateTo = null
                };
            return View(input);
        }

        /// <summary>
        /// Tìm kiếm và hiển thị danh sách đơn hàng dưới dạng phân trang
        /// </summary>
        /// <param name="input">Đầu vào tìm kiếm, phân trang đơn hàng</param>
        public async Task<IActionResult> Search(OrderSearchInput input)
        {
            // Normalize DateFrom/DateTo về DateTimeKind.Unspecified
            // để tránh bị lệch múi giờ khi serialize/deserialize session
            if (input.DateFrom.HasValue)
                input.DateFrom = DateTime.SpecifyKind(input.DateFrom.Value, DateTimeKind.Unspecified);
            if (input.DateTo.HasValue)
                input.DateTo = DateTime.SpecifyKind(input.DateTo.Value, DateTimeKind.Unspecified);

            var result = await SalesDataService.ListOrdersAsync(input);
            ApplicationContext.SetSessionData(SEARCH_ORDER, input);
            return View(result);
        }

        /// <summary>
        /// Xem chi tiết đơn hàng — hiển thị thông tin đơn hàng và danh sách mặt hàng
        /// </summary>
        /// <param name="id">Mã đơn hàng</param>
        public async Task<IActionResult> Detail(int id)
        {
            var model = await SalesDataService.GetOrderAsync(id);
            if (model == null)
                return RedirectToAction("Index");

            // Truyền danh sách chi tiết đơn hàng qua ViewBag cho view hiển thị
            ViewBag.Details = await SalesDataService.ListDetailsAsync(id);
            ViewBag.Title = $"Chi tiết đơn hàng #{id}";
            return View(model);
        }

        #endregion

        #region Order Create

        /// <summary>
        /// Lập đơn hàng mới — giao diện mẫu, chưa triển khai logic đầy đủ
        /// </summary>
        [HttpGet]
        public IActionResult Create()
        {
            // TODO: Triển khai tính năng lập đơn hàng mới (giỏ hàng, chọn khách hàng, lưu đơn)
            return View();
        }

        #endregion

        #region Order Details Management (modal)

        /// <summary>
        /// Partial view form thêm mặt hàng vào đơn hàng — hiển thị trong dialogModal
        /// </summary>
        /// <param name="id">Mã đơn hàng (0 nếu là giỏ hàng chưa lưu)</param>
        [HttpGet]
        public IActionResult CreateCartItem(int id = 0)
        {
            ViewBag.OrderID = id;
            return PartialView();
        }

        /// <summary>
        /// Partial view form cập nhật chi tiết đơn hàng — hiển thị trong dialogModal
        /// </summary>
        /// <param name="id">Mã đơn hàng</param>
        /// <param name="productId">Mã mặt hàng</param>
        [HttpGet]
        public IActionResult EditCartItem(int id = 0, int productId = 0)
        {
            // TODO: Load dữ liệu chi tiết thực tế và truyền vào partial view
            return PartialView();
        }

        /// <summary>
        /// Partial view xác nhận xóa mặt hàng khỏi đơn hàng — hiển thị trong dialogModal
        /// </summary>
        /// <param name="id">Mã đơn hàng</param>
        /// <param name="productId">Mã mặt hàng</param>
        [HttpGet]
        public IActionResult DeleteCartItem(int id = 0, int productId = 0)
        {
            return PartialView();
        }

        /// <summary>
        /// Partial view xác nhận xóa toàn bộ giỏ hàng — hiển thị trong dialogModal
        /// </summary>
        [HttpGet]
        public IActionResult ClearCart()
        {
            return PartialView();
        }

        #endregion

        #region Order Status Workflow (modal — GET trả về PartialView, POST xử lý)

        /// <summary>
        /// GET: Hiển thị modal xác nhận duyệt đơn hàng
        /// </summary>
        /// <param name="id">Mã đơn hàng</param>
        [HttpGet]
        public async Task<IActionResult> Accept(int id)
        {
            var model = await SalesDataService.GetOrderAsync(id);
            return PartialView(model);
        }

        /// <summary>
        /// POST: Thực hiện duyệt đơn hàng, sau đó reload trang Detail
        /// </summary>
        /// <param name="id">Mã đơn hàng</param>
        [HttpPost]
        [ActionName("Accept")]
        public async Task<IActionResult> AcceptPost(int id)
        {
            // TODO: Lấy EmployeeID thực từ Claims của người dùng đang đăng nhập
            int employeeID = 1;
            await SalesDataService.AcceptOrderAsync(id, employeeID);
            return RedirectToAction("Detail", new { id });
        }

        /// <summary>
        /// GET: Hiển thị modal xác nhận chuyển giao hàng (chọn người giao hàng)
        /// </summary>
        /// <param name="id">Mã đơn hàng</param>
        [HttpGet]
        public async Task<IActionResult> Shipping(int id)
        {
            var model = await SalesDataService.GetOrderAsync(id);
            return PartialView(model);
        }

        /// <summary>
        /// POST: Thực hiện chuyển đơn hàng sang trạng thái đang giao
        /// </summary>
        /// <param name="id">Mã đơn hàng</param>
        /// <param name="shipperID">Mã người giao hàng được chọn</param>
        [HttpPost]
        [ActionName("Shipping")]
        public async Task<IActionResult> ShippingPost(int id, int shipperID)
        {
            await SalesDataService.ShipOrderAsync(id, shipperID);
            return RedirectToAction("Detail", new { id });
        }

        /// <summary>
        /// GET: Hiển thị modal xác nhận hoàn tất đơn hàng
        /// </summary>
        /// <param name="id">Mã đơn hàng</param>
        [HttpGet]
        public async Task<IActionResult> Finish(int id)
        {
            var model = await SalesDataService.GetOrderAsync(id);
            return PartialView(model);
        }

        /// <summary>
        /// POST: Thực hiện hoàn tất đơn hàng
        /// </summary>
        /// <param name="id">Mã đơn hàng</param>
        [HttpPost]
        [ActionName("Finish")]
        public async Task<IActionResult> FinishPost(int id)
        {
            await SalesDataService.CompleteOrderAsync(id);
            return RedirectToAction("Detail", new { id });
        }

        /// <summary>
        /// GET: Hiển thị modal xác nhận từ chối đơn hàng
        /// </summary>
        /// <param name="id">Mã đơn hàng</param>
        [HttpGet]
        public async Task<IActionResult> Reject(int id)
        {
            var model = await SalesDataService.GetOrderAsync(id);
            return PartialView(model);
        }

        /// <summary>
        /// POST: Thực hiện từ chối đơn hàng
        /// </summary>
        /// <param name="id">Mã đơn hàng</param>
        [HttpPost]
        [ActionName("Reject")]
        public async Task<IActionResult> RejectPost(int id)
        {
            // TODO: Lấy EmployeeID thực từ Claims của người dùng đang đăng nhập
            int employeeID = 1;
            await SalesDataService.RejectOrderAsync(id, employeeID);
            return RedirectToAction("Detail", new { id });
        }

        /// <summary>
        /// GET: Hiển thị modal xác nhận hủy đơn hàng
        /// </summary>
        /// <param name="id">Mã đơn hàng</param>
        [HttpGet]
        public async Task<IActionResult> Cancel(int id)
        {
            var model = await SalesDataService.GetOrderAsync(id);
            return PartialView(model);
        }

        /// <summary>
        /// POST: Thực hiện hủy đơn hàng
        /// </summary>
        /// <param name="id">Mã đơn hàng</param>
        [HttpPost]
        [ActionName("Cancel")]
        public async Task<IActionResult> CancelPost(int id)
        {
            await SalesDataService.CancelOrderAsync(id);
            return RedirectToAction("Detail", new { id });
        }

        #endregion

        #region Order Delete

        /// <summary>
        /// GET: Hiển thị modal xác nhận xóa đơn hàng.
        /// POST: Thực hiện xóa và chuyển về danh sách.
        /// </summary>
        /// <param name="id">Mã đơn hàng</param>
        public async Task<IActionResult> Delete(int id)
        {
            if (Request.Method == "POST")
            {
                try { await SalesDataService.DeleteOrderAsync(id); } catch { }
                return RedirectToAction("Index");
            }

            // GET: Trả về partial view trong modal
            var model = await SalesDataService.GetOrderAsync(id);
            if (model == null)
                return RedirectToAction("Index");

            return PartialView(model);
        }

        #endregion
    }
}