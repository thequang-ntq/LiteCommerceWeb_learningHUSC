using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace SV22T1020362.Shop
{
    /// <summary>
    /// Thông tin khách hàng lưu trong cookie đăng nhập
    /// </summary>
    public class WebUserData
    {
        public string? UserId { get; set; }
        public string? UserName { get; set; }
        public string? DisplayName { get; set; }
        public string? Email { get; set; }

        private List<Claim> Claims => new List<Claim>
        {
            new Claim(nameof(UserId),      UserId      ?? ""),
            new Claim(nameof(UserName),    UserName    ?? ""),
            new Claim(nameof(DisplayName), DisplayName ?? ""),
            new Claim(nameof(Email),       Email       ?? ""),
        };

        /// <summary>
        /// Tạo ClaimsPrincipal để lưu vào cookie
        /// </summary>
        public ClaimsPrincipal CreatePrincipal()
        {
            var identity = new ClaimsIdentity(Claims, CookieAuthenticationDefaults.AuthenticationScheme);
            return new ClaimsPrincipal(identity);
        }
    }

    /// <summary>
    /// Extension đọc thông tin user từ ClaimsPrincipal
    /// </summary>
    public static class WebUserExtensions
    {
        public static WebUserData? GetUserData(this ClaimsPrincipal principal)
        {
            try
            {
                if (principal == null || principal.Identity == null || !principal.Identity.IsAuthenticated)
                    return null;

                return new WebUserData
                {
                    UserId = principal.FindFirstValue(nameof(WebUserData.UserId)),
                    UserName = principal.FindFirstValue(nameof(WebUserData.UserName)),
                    DisplayName = principal.FindFirstValue(nameof(WebUserData.DisplayName)),
                    Email = principal.FindFirstValue(nameof(WebUserData.Email)),
                };
            }
            catch { return null; }
        }
    }
}