using Newtonsoft.Json;

namespace SV22T1020362.Shop
{
    /// <summary>
    /// Lớp cung cấp các tiện ích liên quan đến ngữ cảnh ứng dụng web cho Shop
    /// </summary>
    public static class ApplicationContext
    {
        private static IHttpContextAccessor? _httpContextAccessor;
        private static IWebHostEnvironment? _webHostEnvironment;
        private static IConfiguration? _configuration;

        /// <summary>
        /// Khởi tạo ApplicationContext - gọi trong Program.cs
        /// </summary>
        public static void Configure(IHttpContextAccessor httpContextAccessor,
            IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException();
            _webHostEnvironment = webHostEnvironment ?? throw new ArgumentNullException();
            _configuration = configuration ?? throw new ArgumentNullException();
        }

        public static HttpContext? HttpContext => _httpContextAccessor?.HttpContext;
        public static IWebHostEnvironment? WebHostEnvironment => _webHostEnvironment;
        public static IConfiguration? Configuration => _configuration;

        /// <summary>
        /// Đường dẫn vật lý đến thư mục wwwroot
        /// </summary>
        public static string WWWRootPath => _webHostEnvironment?.WebRootPath ?? string.Empty;

        /// <summary>
        /// Ghi dữ liệu vào session
        /// </summary>
        public static void SetSessionData(string key, object value)
        {
            try
            {
                string sValue = JsonConvert.SerializeObject(value);
                if (!string.IsNullOrEmpty(sValue))
                    _httpContextAccessor?.HttpContext?.Session.SetString(key, sValue);
            }
            catch { }
        }

        /// <summary>
        /// Đọc dữ liệu từ session
        /// </summary>
        public static T? GetSessionData<T>(string key) where T : class
        {
            try
            {
                string sValue = _httpContextAccessor?.HttpContext?.Session.GetString(key) ?? "";
                if (!string.IsNullOrEmpty(sValue))
                    return JsonConvert.DeserializeObject<T>(sValue);
            }
            catch { }
            return null;
        }

        /// <summary>
        /// Lấy giá trị cấu hình từ appsettings.json
        /// </summary>
        public static string GetConfigValue(string name)
        {
            return _configuration?[name] ?? "";
        }

        /// <summary>
        /// Số sản phẩm hiển thị trên mỗi trang
        /// </summary>
        public static int PageSize => Convert.ToInt32(GetConfigValue("PageSize"));
    }
}