# Hướng dẫn tạo Views cho SV22T1020362.Admin

## 1. Cấu trúc thư mục Views

```
Views/
├── Shared/
│   ├── _Layout.cshtml (Đã có)
│   ├── _Header.cshtml (Đã có)
│   ├── _Sidebar.cshtml (Đã có)
│   └── _Footer.cshtml (Đã có)
├── _ViewStart.cshtml (Đã có)
├── _ViewImports.cshtml (MỚI TẠO)
├── Account/
│   ├── Login.cshtml ✅ (Đã tạo)
│   └── ChangePassword.cshtml ✅ (Đã tạo)
├── Home/
│   └── Index.cshtml ✅ (Đã tạo)
├── Category/
│   ├── Index.cshtml ✅ (Đã tạo)
│   ├── Create.cshtml ✅ (Đã tạo)
│   └── Edit.cshtml ✅ (Đã tạo)
├── Shipper/
│   ├── Index.cshtml ✅ (Đã tạo)
│   ├── Create.cshtml ✅ (Đã tạo)
│   └── Edit.cshtml (Tương tự Create)
├── Supplier/
│   ├── Index.cshtml (Tương tự Category, có thêm dropdown Province)
│   ├── Create.cshtml (Có thêm dropdown Province)
│   └── Edit.cshtml (Có thêm dropdown Province)
├── Customer/
│   ├── Index.cshtml (Có filter Province và IsLocked)
│   ├── Create.cshtml (Có dropdown Province)
│   ├── Edit.cshtml (Có dropdown Province)
│   └── ChangePassword.cshtml (Form đổi mật khẩu)
├── Employee/
│   ├── Index.cshtml (Có filter IsWorking và RoleName)
│   ├── Create.cshtml (Có checkbox chọn Roles)
│   ├── Edit.cshtml (Không cho sửa roles ở đây)
│   ├── ChangePassword.cshtml (Form đổi mật khẩu)
│   └── ChangeRoles.cshtml (Checkbox chọn roles, chỉ Admin)
├── Product/
│   ├── Index.cshtml (Filter Supplier, Category, MinPrice, MaxPrice)
│   ├── Detail.cshtml (Hiển thị chi tiết sản phẩm)
│   ├── Create.cshtml (Dropdown Supplier, Category)
│   ├── Edit.cshtml (Dropdown Supplier, Category)
│   ├── ListAttributes.cshtml (Bảng danh sách thuộc tính)
│   ├── AddAttribute.cshtml (Form thêm thuộc tính)
│   ├── EditAttribute.cshtml (Form sửa thuộc tính)
│   ├── ListPhotos.cshtml (Grid hiển thị ảnh)
│   ├── AddPhoto.cshtml (Form thêm ảnh)
│   └── EditPhoto.cshtml (Form sửa ảnh)
└── Order/
    ├── Index.cshtml (Filter phức tạp: Status, Customer, Employee, Shipper, Date)
    ├── Detail.cshtml (Hiển thị thông tin đơn + chi tiết sản phẩm + workflow buttons)
    └── Create.cshtml (Form phức tạp: chọn customer, thêm sản phẩm động)
```

## 2. Pattern chung cho các View

### 2.1. Index View (Danh sách)

**Cấu trúc:**
```html
@model [Type]SearchResult

<div class="card">
    <div class="card-header">
        <h3>Tiêu đề</h3>
        <div class="card-tools">
            <a asp-action="Create">Thêm mới</a>
        </div>
    </div>
    
    <div class="card-body">
        <!-- Form tìm kiếm -->
        <form method="get">
            <!-- Input fields + filters -->
        </form>
        
        <!-- Alert messages (TempData) -->
        
        <!-- Table -->
        <table class="table">
            <!-- Columns -->
        </table>
        
        <!-- Pagination -->
    </div>
</div>
```

**Các thành phần:**
- ✅ Form tìm kiếm với filters
- ✅ TempData ErrorMessage/SuccessMessage
- ✅ Table responsive với bootstrap
- ✅ Pagination với icons Bootstrap
- ✅ Action buttons (Edit, Delete)
- ✅ Confirm delete với JavaScript

### 2.2. Create/Edit View (Form)

**Cấu trúc:**
```html
@model [Type]

<div class="row justify-content-center">
    <div class="col-md-8">
        <div class="card">
            <div class="card-header">
                <h3>Tiêu đề</h3>
            </div>
            
            <form asp-action="[Action]" method="post">
                <div class="card-body">
                    <!-- ModelState errors -->
                    
                    <!-- Form fields -->
                    <div class="mb-3">
                        <label>Label</label>
                        <input asp-for="Property" class="form-control" />
                        <span asp-validation-for="Property"></span>
                    </div>
                </div>
                
                <div class="card-footer">
                    <button type="submit">Lưu</button>
                    <a asp-action="Index">Hủy</a>
                </div>
            </form>
        </div>
    </div>
</div>
```

**Các thành phần:**
- ✅ ModelState validation errors
- ✅ Tag helpers (asp-for, asp-validation-for)
- ✅ Bootstrap form controls
- ✅ Required field indicator (*)
- ✅ Buttons: Submit + Cancel

### 2.3. Dropdown (Select)

```html
<div class="mb-3">
    <label asp-for="PropertyID" class="form-label">Label</label>
    <select asp-for="PropertyID" class="form-select">
        <option value="">-- Chọn --</option>
        @foreach (var item in ViewBag.Items)
        {
            <option value="@item.ID">@item.Name</option>
        }
    </select>
</div>
```

### 2.4. Checkbox (Multiple Select)

```html
@foreach (var role in ViewBag.AllRoles)
{
    <div class="form-check">
        <input type="checkbox" name="selectedRoles" value="@role" 
               class="form-check-input" id="role_@role"
               @(Model.Roles.Contains(role) ? "checked" : "") />
        <label class="form-check-label" for="role_@role">@role</label>
    </div>
}
```

### 2.5. Pagination

```html
@if (Model.TotalPages > 1)
{
    <nav>
        <ul class="pagination justify-content-center">
            <!-- Previous -->
            <li class="page-item @(Model.Page <= 1 ? "disabled" : "")">
                <a class="page-link" asp-route-page="@(Model.Page - 1)">
                    <i class="bi bi-chevron-left"></i>
                </a>
            </li>
            
            <!-- Page numbers -->
            @for (int i = 1; i <= Model.TotalPages; i++)
            {
                <li class="page-item @(i == Model.Page ? "active" : "")">
                    <a class="page-link" asp-route-page="@i">@i</a>
                </li>
            }
            
            <!-- Next -->
            <li class="page-item @(Model.Page >= Model.TotalPages ? "disabled" : "")">
                <a class="page-link" asp-route-page="@(Model.Page + 1)">
                    <i class="bi bi-chevron-right"></i>
                </a>
            </li>
        </ul>
    </nav>
}
```

## 3. Bootstrap Icons sử dụng

- `bi bi-plus-circle` - Thêm mới
- `bi bi-pencil` - Sửa
- `bi bi-trash` - Xóa
- `bi bi-search` - Tìm kiếm
- `bi bi-x-circle` - Hủy/Xóa filter
- `bi bi-check-circle` - Lưu/Xác nhận
- `bi bi-eye` - Xem
- `bi bi-chevron-left/right` - Prev/Next
- `bi bi-envelope` - Email
- `bi bi-telephone` - Phone
- `bi bi-box-arrow-in-right` - Login
- `bi bi-box-arrow-right` - Logout
- `bi bi-key` - Password

## 4. CSS Classes Bootstrap 5

### Buttons:
- `btn btn-primary` - Nút chính
- `btn btn-secondary` - Nút phụ
- `btn btn-success` - Thành công
- `btn btn-warning` - Cảnh báo
- `btn btn-danger` - Nguy hiểm
- `btn btn-sm` - Nút nhỏ

### Alerts:
- `alert alert-success` - Thành công
- `alert alert-danger` - Lỗi
- `alert alert-warning` - Cảnh báo
- `alert alert-info` - Thông tin
- `alert-dismissible` - Có nút đóng

### Badges:
- `badge bg-primary`
- `badge bg-success`
- `badge bg-danger`
- `badge bg-warning`
- `badge bg-info`

### Table:
- `table` - Bảng cơ bản
- `table-striped` - Dòng xen kẽ màu
- `table-hover` - Hover effect
- `table-responsive` - Responsive wrapper

### Form:
- `form-control` - Input, textarea, select
- `form-select` - Select dropdown
- `form-check` - Checkbox/Radio
- `form-label` - Label
- `mb-3` - Margin bottom 3

## 5. Ví dụ View cụ thể

### 5.1. Supplier Index (có filter Province)

```html
@model SupplierSearchResult

<!-- Form tìm kiếm -->
<form method="get">
    <div class="row">
        <div class="col-md-6">
            <input type="text" name="searchValue" class="form-control" 
                   placeholder="Tìm theo tên, người liên hệ, SĐT..." />
        </div>
        <div class="col-md-3">
            <select name="province" class="form-select">
                <option value="">-- Tất cả tỉnh/TP --</option>
                @foreach (var p in ViewBag.Provinces)
                {
                    <option value="@p.ProvinceName" 
                            @(p.ProvinceName == ViewBag.Province ? "selected" : "")>
                        @p.ProvinceName
                    </option>
                }
            </select>
        </div>
        <div class="col-md-3">
            <button type="submit" class="btn btn-primary w-100">
                <i class="bi bi-search"></i> Tìm
            </button>
        </div>
    </div>
</form>
```

### 5.2. Employee Create (checkbox Roles)

```html
<div class="mb-3">
    <label class="form-label">Phân quyền</label>
    @foreach (var role in ViewBag.Roles)
    {
        <div class="form-check">
            <input type="checkbox" name="selectedRoles" value="@role" 
                   class="form-check-input" id="role_@role" />
            <label class="form-check-label" for="role_@role">@role</label>
        </div>
    }
</div>
```

### 5.3. Product Detail (Tab layout)

```html
<ul class="nav nav-tabs" role="tablist">
    <li class="nav-item">
        <a class="nav-link active" data-bs-toggle="tab" href="#info">
            Thông tin
        </a>
    </li>
    <li class="nav-item">
        <a class="nav-link" data-bs-toggle="tab" href="#attributes">
            Thuộc tính
        </a>
    </li>
    <li class="nav-item">
        <a class="nav-link" data-bs-toggle="tab" href="#photos">
            Hình ảnh
        </a>
    </li>
</ul>

<div class="tab-content">
    <div id="info" class="tab-pane fade show active">
        <!-- Product info -->
    </div>
    <div id="attributes" class="tab-pane fade">
        <!-- Attributes list -->
    </div>
    <div id="photos" class="tab-pane fade">
        <!-- Photos grid -->
    </div>
</div>
```

## 6. JavaScript cần thiết

### Confirm Delete:
```javascript
onclick="return confirm('Bạn có chắc muốn xóa?')"
```

### Dynamic Add/Remove (Order Create):
```javascript
// Thêm sản phẩm vào đơn hàng
function addProduct() {
    // Clone row template
    // Append to table
}

// Xóa sản phẩm khỏi đơn hàng
function removeProduct(row) {
    $(row).closest('tr').remove();
}
```

## 7. Lưu ý quan trọng

1. **_ViewImports.cshtml** phải có ở thư mục Views/
2. **Tag Helpers** sử dụng `asp-*` attributes
3. **ModelState validation** hiển thị lỗi từ server
4. **TempData** cho messages giữa redirects
5. **ViewBag** truyền dữ liệu từ Controller
6. **Bootstrap 5** - Không dùng jQuery
7. **Icons** - Sử dụng Bootstrap Icons (bi bi-*)

## 8. Checklist tạo View

Cho mỗi Controller/Action:

- [ ] Model type đúng (@model)
- [ ] Form method="post" (Create/Edit)
- [ ] Tag helpers (asp-action, asp-for, asp-validation-for)
- [ ] ModelState errors display
- [ ] TempData messages display
- [ ] Required fields có dấu (*)
- [ ] Buttons có icons
- [ ] Table responsive wrapper
- [ ] Pagination nếu cần
- [ ] Confirm delete cho action xóa
- [ ] Breadcrumb nếu cần

## 9. Testing

Sau khi tạo View:
1. Chạy app và test từng action
2. Kiểm tra validation errors
3. Test search và pagination
4. Test create/edit/delete
5. Kiểm tra responsive trên mobile
6. Test confirm dialogs
7. Kiểm tra TempData messages

## 10. Tài liệu tham khảo

- Bootstrap 5: https://getbootstrap.com/docs/5.0/
- Bootstrap Icons: https://icons.getbootstrap.com/
- ASP.NET Tag Helpers: https://docs.microsoft.com/aspnet/core/mvc/views/tag-helpers/
- AdminLTE 4: https://adminlte.io/docs/4.0/