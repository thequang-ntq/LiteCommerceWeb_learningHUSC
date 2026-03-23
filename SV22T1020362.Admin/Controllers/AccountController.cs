using Microsoft.AspNetCore.Mvc;

namespace SV22T1020362.Admin.Controllers
{
    /// <summary>
    /// Các chức năng liên quan đến tài khoản
    /// </summary>

    // Controller cho xác thực và quản lý tài khoản
    public class AccountController : Controller
    {

        /// <summary>
        /// Đăng nhập
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Login() { 

            return View();
        }

        /// <summary>
        /// Đổi mật khẩu
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        /// <summary>
        /// Đăng xuất
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Logout()
        {

            return RedirectToAction("Login");
        }
    }
}