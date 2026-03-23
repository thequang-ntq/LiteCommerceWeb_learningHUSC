# Hướng dẫn sử dụng Session trong SV22T1020362.Admin (30/01/2026)

## 1. Cấu hình Session trong Program.cs

```csharp
// Thêm vào phần builder.Services
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Session timeout 30 phút
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.Cookie.Name = ".LiteCommerce.Session";
});

// Thêm middleware (QUAN TRỌNG: Phải đặt trước UseAuthorization)
app.UseSession();
```

## 2. SessionExtensions Helper

File: `Helpers/SessionExtensions.cs`

Cung cấp 2 extension methods:
- `SetObject<T>(string key, T value)` - Lưu object vào Session
- `GetObject<T>(string key)` - Lấy object từ Session

Sử dụng:
```csharp
// Lưu object
HttpContext.Session.SetObject("UserAccount", userAccount);

// Lấy object
var user = HttpContext.Session.GetObject<UserAccount>("UserAccount");
```

## 3. AccountController với Session

### Login
```csharp
var userAccount = await EmployeeService.AuthenticateAsync(model.Email, model.Password);
HttpContext.Session.SetObject(SESSION_USER_ACCOUNT, userAccount);
```

### Logout
```csharp
HttpContext.Session.Remove(SESSION_USER_ACCOUNT);
// Hoặc: HttpContext.Session.Clear();
```

### Get Current User
```csharp
private UserAccount? GetCurrentUser()
{
    return HttpContext.Session.GetObject<UserAccount>(SESSION_USER_ACCOUNT);
}
```

## 4. BaseController cho Authentication

File: `Controllers/BaseController.cs`

Tất cả các Controller cần kiểm tra đăng nhập nên kế thừa từ `BaseController`:

```csharp
public class HomeController : BaseController
{
    // Tự động kiểm tra đăng nhập
    // Có thể sử dụng: CurrentUser, IsAuthenticated, IsAdmin, HasRole()
}
```

### Properties có sẵn:
- `CurrentUser` - Thông tin user hiện tại
- `IsAuthenticated` - Đã đăng nhập chưa
- `IsAdmin` - Có phải Admin không
- `HasRole(string roleName)` - Kiểm tra quyền

### Ví dụ sử dụng trong Controller:
```csharp
public class EmployeeController : BaseController
{
    public async Task<IActionResult> ChangeRoles(int id)
    {
        // Chỉ Admin mới được phân quyền
        if (!IsAdmin)
        {
            TempData["ErrorMessage"] = "Bạn không có quyền thực hiện chức năng này.";
            return RedirectToAction("Index");
        }
        
        // Logic phân quyền...
    }
}
```

## 5. Cập nhật các Controller hiện có

### AccountController
Đã cập nhật đầy đủ, KHÔNG cần kế thừa BaseController

### Các Controller khác
Có 2 cách:

**Cách 1: Kế thừa BaseController (Khuyến nghị)**
```csharp
public class HomeController : BaseController
{
    // Tự động kiểm tra authentication
}
```

**Cách 2: Giữ nguyên, chỉ thêm helper method**
```csharp
public class HomeController : Controller
{
    private UserAccount? GetCurrentUser()
    {
        return HttpContext.Session.GetObject<UserAccount>("UserAccount");
    }
    
    public IActionResult Index()
    {
        var user = GetCurrentUser();
        if (user == null)
            return RedirectToAction("Login", "Account");
        
        ViewBag.CurrentUser = user;
        // Logic...
    }
}
```

## 6. Sử dụng trong View (_Layout.cshtml)

```html
@{
    var currentUser = ViewBag.CurrentUser as SV22T1020362.Models.UserAccount;
}

@if (currentUser != null)
{
    <span class="d-none d-md-inline">@currentUser.FullName</span>
    
    @if (currentUser.IsAdmin)
    {
        <!-- Hiển thị menu Admin -->
    }
}
```

## 7. Route mặc định

Cập nhật route trong Program.cs để redirect về Login khi vào trang chủ:

```csharp
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");
```

Hoặc giữ nguyên và kiểm tra trong HomeController:
```csharp
public class HomeController : BaseController
{
    public IActionResult Index()
    {
        // BaseController đã tự động kiểm tra authentication
        return View();
    }
}
```

## 8. Test Session

### Kiểm tra đăng nhập:
1. Chạy ứng dụng: `/Account/Login`
2. Đăng nhập với tài khoản employee trong CSDL
3. Session sẽ lưu thông tin user trong 30 phút
4. Truy cập các trang khác sẽ tự động kiểm tra session

### Kiểm tra logout:
1. Truy cập: `/Account/Logout`
2. Session bị xóa
3. Redirect về trang Login

## 9. Lưu ý

- Session timeout mặc định: 30 phút
- Session lưu trong memory, sẽ mất khi restart ứng dụng
- Với production, nên dùng Redis hoặc SQL Server để lưu session
- BaseController tự động kiểm tra authentication cho tất cả action
- AccountController KHÔNG kế thừa BaseController (để cho phép truy cập Login)

## 10. Cấu trúc thư mục

```
SV22T1020362.Admin/
├── Controllers/
│   ├── AccountController.cs (KHÔNG kế thừa BaseController)
│   ├── BaseController.cs (Kiểm tra authentication)
│   ├── HomeController.cs (Kế thừa BaseController)
│   ├── CategoryController.cs (Kế thừa BaseController)
│   └── ... (Tất cả đều kế thừa BaseController)
├── Helpers/
│   └── SessionExtensions.cs
└── Program.cs (Cấu hình Session)
```