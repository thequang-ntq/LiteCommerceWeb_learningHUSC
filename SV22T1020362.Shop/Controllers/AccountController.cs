using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SV22T1020362.BusinessLayers;
using SV22T1020362.Models.Partner;

namespace SV22T1020362.Shop.Controllers
{
    /// <summary>
    /// Chức năng tài khoản khách hàng: đăng ký, đăng nhập, đăng xuất, đổi mật khẩu
    /// </summary>
    public class AccountController : Controller
    {
        #region Đăng nhập / Đăng xuất

        /// <summary>
        /// Hiển thị form đăng nhập
        /// </summary>
        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            if (User.Identity?.IsAuthenticated == true)
                return RedirectToAction("Index", "Home");

            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        /// <summary>
        /// Xử lý đăng nhập - xác thực qua bảng Customers
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Login(string username, string password, string? returnUrl = null)
        {
            ViewBag.Username = username;
            ViewBag.ReturnUrl = returnUrl;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                ModelState.AddModelError("", "Vui lòng nhập đầy đủ thông tin");
                return View();
            }

            try
            {
                string hashedPwd = CryptHelper.HashMD5(password);
                var account = await SecurityDataService.AuthorizeCustomerAsync(username, hashedPwd);

                if (account == null)
                {
                    ModelState.AddModelError("", "Email hoặc mật khẩu không đúng, hoặc tài khoản đang bị khóa");
                    return View();
                }

                var userData = new WebUserData
                {
                    UserId = account.UserId,
                    UserName = account.UserName,
                    DisplayName = account.DisplayName,
                    Email = account.Email,
                };

                // Ghi nhận phiên đăng nhập bằng Cookie Authentication
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    userData.CreatePrincipal()
                );

                // Merge giỏ hàng Session (nếu có) vào CSDL sau khi đăng nhập
                var sessionCart = ShoppingCartHelper.GetSessionCart();
                if (sessionCart.Any())
                {
                    int customerID = int.Parse(account.UserId);
                    await SalesDataService.MergeSessionCartToDBAsync(customerID, sessionCart);
                    ShoppingCartHelper.ClearSessionCart();
                }

                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    return Redirect(returnUrl);

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Có lỗi xảy ra: {ex.Message}");
                return View();
            }
        }

        /// <summary>
        /// Đăng xuất
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Clear();
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }

        #endregion

        #region Đăng ký tài khoản

        /// <summary>
        /// Hiển thị form đăng ký
        /// </summary>
        [HttpGet]
        public IActionResult Register()
        {
            if (User.Identity?.IsAuthenticated == true)
                return RedirectToAction("Index", "Home");
            return View();
        }

        /// <summary>
        /// Xử lý đăng ký tài khoản mới
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Register(string customerName, string contactName,
            string email, string phone, string province, string address,
            string password, string confirmPassword)
        {
            ViewBag.CustomerName = customerName;
            ViewBag.ContactName = contactName;
            ViewBag.Email = email;
            ViewBag.Phone = phone;
            ViewBag.Province = province;
            ViewBag.Address = address;

            // Validate
            if (string.IsNullOrWhiteSpace(customerName))
                ModelState.AddModelError("customerName", "Vui lòng nhập họ và tên");

            if (string.IsNullOrWhiteSpace(email))
                ModelState.AddModelError("email", "Vui lòng nhập email");
            else if (!await PartnerDataService.ValidateCustomerEmailAsync(email))
                ModelState.AddModelError("email", "Email này đã được sử dụng");

            if (string.IsNullOrWhiteSpace(password))
                ModelState.AddModelError("password", "Vui lòng nhập mật khẩu");
            else if (password.Length < 6)
                ModelState.AddModelError("password", "Mật khẩu phải có ít nhất 6 ký tự");

            if (password != confirmPassword)
                ModelState.AddModelError("confirmPassword", "Mật khẩu xác nhận không khớp");

            if (!ModelState.IsValid)
                return View();

            try
            {
                // Tạo khách hàng mới (mật khẩu được mã hóa MD5)
                var newCustomer = new Customer
                {
                    CustomerName = customerName,
                    ContactName = contactName ?? customerName,
                    Email = email,
                    Phone = phone ?? "",
                    Province = string.IsNullOrWhiteSpace(province) ? null : province,
                    Address = address ?? "",
                    IsLocked = false
                };

                int newId = await PartnerDataService.AddCustomerAsync(newCustomer);

                // Đặt mật khẩu cho tài khoản vừa tạo
                await SecurityDataService.ChangeCustomerPasswordAsync(email, CryptHelper.HashMD5(password));

                TempData["RegisterSuccess"] = "Đăng ký thành công! Vui lòng đăng nhập.";
                return RedirectToAction("Login");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
        }

        #endregion

        #region Quản lý thông tin cá nhân

        /// <summary>
        /// Hiển thị trang thông tin cá nhân
        /// </summary>
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var userData = User.GetUserData();
            if (userData == null) return RedirectToAction("Login");

            int customerID = int.Parse(userData.UserId!);
            var customer = await PartnerDataService.GetCustomerAsync(customerID);
            if (customer == null) return RedirectToAction("Login");

            return View(customer);
        }

        /// <summary>
        /// Cập nhật thông tin cá nhân
        /// </summary>
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Profile(string customerName, string contactName,
            string phone, string province, string address)
        {
            var userData = User.GetUserData();
            if (userData == null) return RedirectToAction("Login");

            int customerID = int.Parse(userData.UserId!);
            var customer = await PartnerDataService.GetCustomerAsync(customerID);
            if (customer == null) return RedirectToAction("Login");

            if (string.IsNullOrWhiteSpace(customerName))
            {
                ModelState.AddModelError("customerName", "Vui lòng nhập họ và tên");
                return View(customer);
            }

            try
            {
                customer.CustomerName = customerName;
                customer.ContactName = contactName ?? customerName;
                customer.Phone = phone ?? "";
                customer.Province = string.IsNullOrWhiteSpace(province) ? null : province;
                customer.Address = address ?? "";

                await PartnerDataService.UpdateCustomerAsync(customer);
                TempData["Success"] = "Cập nhật thông tin thành công!";
                return RedirectToAction("Profile");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(customer);
            }
        }

        /// <summary>
        /// Hiển thị form đổi mật khẩu
        /// </summary>
        [Authorize]
        [HttpGet]
        public IActionResult ChangePassword() => View();

        /// <summary>
        /// Xử lý đổi mật khẩu của khách hàng đang đăng nhập
        /// </summary>
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ChangePassword(string oldPassword, string newPassword, string confirmPassword)
        {
            var userData = User.GetUserData();
            if (userData == null) return RedirectToAction("Login");

            if (string.IsNullOrWhiteSpace(oldPassword))
                ModelState.AddModelError("oldPassword", "Vui lòng nhập mật khẩu cũ");

            if (string.IsNullOrWhiteSpace(newPassword))
                ModelState.AddModelError("newPassword", "Vui lòng nhập mật khẩu mới");
            else if (newPassword.Length < 6)
                ModelState.AddModelError("newPassword", "Mật khẩu mới phải có ít nhất 6 ký tự");

            if (newPassword != confirmPassword)
                ModelState.AddModelError("confirmPassword", "Mật khẩu xác nhận không khớp");

            if (!ModelState.IsValid) return View();

            try
            {
                // Kiểm tra mật khẩu cũ
                var check = await SecurityDataService.AuthorizeCustomerAsync(
                    userData.UserName!, CryptHelper.HashMD5(oldPassword));

                if (check == null)
                {
                    ModelState.AddModelError("oldPassword", "Mật khẩu cũ không đúng");
                    return View();
                }

                await SecurityDataService.ChangeCustomerPasswordAsync(
                    userData.UserName!, CryptHelper.HashMD5(newPassword));

                TempData["Success"] = "Đổi mật khẩu thành công!";
                return RedirectToAction("Profile");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
        }

        #endregion
    }
}