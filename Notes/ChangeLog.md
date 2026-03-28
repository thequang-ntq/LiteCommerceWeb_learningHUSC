# Công việc ngày 29/01/2026
## LiteCommerce Project
### Khởi tạo Solution
1) Tạo Solution có tên là **SV22T1020362**
2) Bổ sung cho Solution các Project sau:
- **SV22T1020362.Admin**: project dạng ASP.NET Core MVC
- **SV22T1020362.Shop**: project dạng ASP.NET Core MVC
- **SV22T1020362.Models**: project dạng Class Library
- **SV22T1020362.DataLayers**: project dạng Class Library
- **SV22T1020362.BusinessLayers**: project dạng Class Library
3) Tạo 2 thư mục Solution tên là Applications và Libraries, cho 2 project **SV22T1020362.Admin** và **SV22T1020362.Shop** vào Applications, còn 3 project còn lại vào Libraries.
4) Add project references: DataLayers được references Models; BusinessLayers được references DataLayers và Models; 2 project presentation là Admin và Shop được references 3 cái còn lại.
5) Trong thư mục wwwroot\lib\, xóa hết các thư viện trong đó và dán 6 thư viện trong AdminLTE_4\_MyLayout\lib\ vào trong wwwroot\lib\, rồi dán thay thế 2 file favicon.ico và Layout.html trong AdminLTE_4\_MyLayout\ vào wwwroot\.
6) Build Solution, mở trang web và mở URL: \Layout.html. Ví dụ: http://localhost:5204/Layout.html. Nếu hiện trang giao diện thì là làm đúng. Phần giao diện đầu bị bể thì không sao.

### Bắt đầu lập trình
- Trong 1 file Layout bắt buộc phải có 1 @RenderBody() --> Đặc tả các vị trí mà nội dung của các Views sẽ được đưa vào đó.
- Có thể có nhiều Layout.
- Code Partial Views, bản thân nó không phải là 1 trang HTML hoàn chỉnh, mà chỉ là 1 phần code trong trang html. VD: _Footer.cshtml, _Header.cshtml, _SideBar.cshtml dùng cho _Layout.cshtml.
- Các Partial Views phải đặt trong thư mục Views/Shared/, 
- Mở \Home.
1. 1) Cóp nội dung trong file Layout.html trong wwwroot vào _Layout.cshtml trong project Admin. Sau đó sửa các đường link url, thêm ~/ vào trước các link trong href và src. Sau đó thêm: < link rel="stylesheet" href ="~/css/site.css" /> vào thẻ < head>, rồi xóa hết nội dung trong wwwroot/css/site.css.
2) Trong project Admin, thư mục Views/Shared/_Layout.cshtml, sửa dòng 183 thành: < h3 class="mb-0">@ViewBag.Title< /h3> trong col-sm-12 trong AppContentHeader. Rồi xóa hết thẻ Card ở dòng 192 trong AppContent trong container-fluid, sửa thành: @RenderBody(). Xóa hết phần model trước khai báo script.
3) Tập trung vào App Main. Trong 1 file Layout phải có 1 @Renderbody() và chỉ đúng 1 lệnh @RenderBody(). Khi sử dụng Layout trong các Views, nội dung của các Views đấy sẽ được viết tại lệnh @RenderBody().
4) Tạo trong project Admin trong Views/Shared file _Header.cshtml, dán hết nội dung _Header trong _Layout.cshtml vào file, rồi gọi lại trong _Layout.cshtml: @{ await Html.RenderPartialAsync("_Header"); }. Tương tự với SideBar.
5) Phần main trong _Layout.cshtml trong Admin thì giữ nguyên. Footer cũng tách ra tương tự như 10).
6) Trong project Admin trong thư mục Views/Shared sửa các đường link trong _SideBar.cshtml và _Heder.cshtml. Sửa: < a href="~/Supplier" class="nav-link"> (Dòng 35); < a href="~/Customer" class="nav-link"> (Dòng 41) ; < a href="~/Shipper" class="nav-link"> (Dòng 47); < a href="~/Employee" class="nav-link"> (Dòng 53); < a href="~/Category" class="nav-link"> (Dòng 71); < a href="~/Product" class="nav-link"> (Dòng 77); < a href="~/Order/Create" class="nav-link"> (Dòng 95); < a href="~/Order" class="nav-link"> (Dòng 101); < a href="~/Account/ChangePassword" class="btn btn-default btn-flat">Đổi mật khẩu< /a> và < a href="~/Account/Logout" class="btn btn-default btn-flat float-end">Thoát< /a> (Dòng 34 và 35 trong _Header.cshtml);
7) Tạo các Controller tương ứng: SupplierController; CustomerController; ShipperController; EmployeeController; CategoryController; ProductController; OrderController. Mỗi Controller có Action Index và 1 file Index.cshtml tương ứng trong Views/, riêng Order có 2 Action là Index và Create, và có 2 file View tương ứng.
8) Sửa tiếp các đường link còn lại trong _Sidebar.cshtml và _Header.cshtml sao cho hợp lý. Cứ mỗi đường link được sửa, tạo Controller và Views giống như cách làm ở trên.
9) Lưu ý sửa các đường link có dấu ~/ ở đầu đối với cả link ảnh để cập nhật cho đúng. ~/ đại bieeurcho link từ đầu wwwrooot/ trỏ ra tiếp.

### Phác thảo chức năng cho SV22T1020362.Admin (Không làm thợ đụng mà phải có kế hoạch)
- Trang chủ: Home/Index (hoặc Home)
- Tài khoản:
	- Account/Login
	- Account/Logout
	- Account/ChangePassword
- Supplier:
	- Supplier/Index
	- Supplier/Create
	- Supplier/Edit/{id}
	- Supplier/Delete/{id}
- Customer:
	- Customer/Index
	- Customer/Create
	- Customer/Edit/{id}
	- Customer/Delete/{id}
	- Customer/ChangePassword/{id}
- Shipper:
	- Shipper/Index
	- Shipper/Create
	- Shipper/Edit/{id}
	- Shipper/Delete/{id}
- Employee:
	- Employee/Index
	- Employee/Create
	- Employee/Edit/{id}
	- Employee/Delete/{id}
	- Employee/ChangePassword/{id}
	- Employee/ChangeRoles/{id} (Chỉ có Admin mới được làm)
- Category:
	- Category/Index (Nhiều level, nhiều mức, nếu giao diện ds đã hiển thị đủ thông tin thì không cần detail)
	- Category/Create
	- Category/Edit/{id}
	- Category/Delete/{id}
- Product:
	- Product/Index (Hiển thị ds sản phẩm, lọc, tìm kiếm)
		- Tìm kiếm, lọc mặt hàng theo nhà cung cấp, phân loại, khoảng giá, tên
		- Hiển thị dưới dạng phân trang
	- Product/Detail/{id}
	- Product/Create
	- Product/Edit/{id}
	- Product/Delete/{id}
	- Product/ListAttributes/{id}
		- Hiển thị danh sách thuộc tính của mặt hàng
	- Product/AddAttribute/{id}
		- Bổ sung 1 thuộc tính cho mặt hàng. Thuộc tính có attributeId, mặt hàng có id.
	- Product/EditAttribute/{id}?attributeId={attributeId}
		- Edit 1 thuộc tính của mặt hàng
	- Product/DeleteAttribute/{id}?attributeId={attributeId}
		- Xóa 1 thuộc tính của mặt hàng
	- Product/ListPhotos/{id}
	- Product/AddPhoto/{id}
	- Product/EditPhoto/{id}?photoId={photoId}
	- Product/DeletePhoto/{id}?photoId={photoId}
- Order
	- Order/Index
		- Hiển thị, tìm kiếm đơn hàng
	- Order/Detail/{id}
		- Xem chi tiết đơn hàng và thực hiện các phép xử lý trên đơn hàng đó.
	- Order/Create
		- Thêm 1 đơn hàng.
	- ... (Phức tạp, còn nhiều quy trình nghiệp vụ BusinessProcess, làm cuối cùng)
	- Order/Edit/{id}
    	- Cập nhật thông tin đơn hàng (Không cập nhật chi tiết)
    	- Cập nhật trạng thái đơn hàng
    	- Nhân viên duyệt đơn hàng
    	- Giao đơn hàng cho Shipper
    	- Hoàn tất đơn hàng
    	- Hủy đơn hàng
    	- Từ chối đơn hàng
	- Order/Delete/{id}
    	- Xóa chi tiết đơn hàng tương ứng
    	- Xóa đơn hàng 
	- Order/ListDetails/{id}
    	- Xem danh sách chi tiết đơn hàng 
    - Order/AddDetail/{id}
        - Thêm chi tiết đơn hàng
    - Order/UpdateDetail/{id}/productID={productID}
        - Cập nhật chi tiết đơn hàng
    - Order/DeleteDetail/{id}/productID={productID}
        - Xóa chi tiết đơn hàng
    - Order/ListStatus
        - Xem danh sách trạng thái đơn hàng

### Việc đã làm thêm khi về nhà
#### 29/01/2026
- Thực hiện trên 3 project class library SV22T1020362.Models, SV22T1020362.DataLayers, SV22T1020362.BusinessLayers.
- Thực hiện trên project ASP.NET Core MVC là SV22T1020362.Admin.
- Bổ sung các file liên quan cho SV22T1020362.Models, SV22T1020362.DataLayers, SV22T1020362.BusinessLayers để biểu diễn kiểu dữ liệu, DAL và xử lý nghiệp vụ Service cho các đối tượng trong CSDL.
- Các file trên mới chỉ là phần khởi tạo ban đầu, còn cần chỉnh sửa nhiều về sau.
- Bổ sung các Action trong các Controller đã tạo ở công việc trên lớp ngày 29/01/2026 và các giao diện tương ứng. Các Action và các giao diện (Views) được tạo cũng chỉ là phần khởi tạo ban đầu, còn cần bổ sung nhiều.

#### 30/01/2026
- Thực hiện trên project ASP.NET Core MVC là SV22T1020362.Admin.
- Bổ sung, sửa lại code các file Controller trong SV22T1020362.Admin.
- Tạo chuỗi kết nối ConnectionStrings trong appsettings.json trong project SV22T1020362.Admin.
- Khởi tạo Service (Trong SV22T1020362.BusinessLayers) trong Program.cs trong project SV22T1020362.Admin.
- Cấu hình session trong Program.cs trong project SV22T1020362.Admin.
- Tạo thư mục Helpers trong SV22T1020362.Admin và tạo file SessionExtensions.cs là helper class để lưu và lấy object vào và từ session dưới dạng json.
- Cập nhật các Controller đã bổ sung code ở trên:
    - Bổ sung file BaseController.cs: là controller cơ sở để tự động kiểm tra authentication cho mọi action, tự động redirect về Login nếu chưa đăng nhập. 
    - AccountController.cs: Cập nhật và không kế thừa BaseController, có sử dụng session.
    - Cập nhật các Controller còn lại: Thay đổi kế thừa từ Controller -> BaseController.
- Tạo file **[Notes/Home/Readme_Session-2026_01_30.md]** là tài liệu đầy đủ về:
    - Cách cấu hình Session
    - Cách sử dụng SessionExtensions
    - Cách sử dụng BaseController
    - Ví dụ code cụ thể
    - Cấu trúc thư mục
- Cập nhật code trong _Header.cshtml trong project SV22T1020362.Admin, trong thư mục Views/Shared/ để sử dụng session.

#### 31/01/2026
- Bắt đầu tạo Views cho các Controller.
- Tạo file **[Notes/Home/Readme_Views_01-2026_01_31.md]** để ghi các file được tạo hiện tại và hướng dẫn tạo các file Views còn lại.
- Hiện đang tạo được Views cho: Home, Category, Shipper (Index, Create).

#### 01/02/2026
- Tạo các Views còn thiếu cho các Controller: Shipper (Edit), Customer, Employee, Product (Index, Details, Create, Edit).
- Còn thiếu các Views: Customer(Index), Supplier (Index, Create, Edit), Product (ListAttributes, AddAttribute, EditAttribute, ListPhotos, AddPhoto, EditPhoto), Order (Index, Details, Create).
- Bổ sung các Views còn thiếu: Product, Order. Còn thiếu Views: Customer (Index), Supplier (Index, Create, Edit).
- Tạo 2 file: **[Notes/Home/VIEWS_SUMMARY-2026_02_01.md]** để tổng kết các Views đã tạo và **[Notes/Home/RENAME_GUIDE-2026_02_01.txt]** để hướng dẫn đổi tên file vào project SV22T1020362.Admin.
- Lưu ý các lỗi còn dính trong các file Index của các Views.
- Còn lỗi:
    - Khi bấm Lập đơn hàng --> Error Not Found (Order/Create).
    - Mật khẩu đã được mã hóa, hàm Authentication khi đăng nhập phải mã hóa mật khẩu trước khi kiểm tra trong CSDL.
    - Phần dropdown hiển thị khi bấm vào tên chưa được đồng bộ, còn để lại tên Trần Nguyên Phong ban đầu.
    - Danh sách Navigate bị tràn màn hình, hiển thị quá nhiều số ở giữa mà không phải dấu 3 chấm.
    - ...

# Công việc ngày 02/02/2026
## LiteCommerce Project
### Làm giao diện
- Giao diện Home/Index chưa làm
- Giao diện Account/Login dùng mẫu AdminLTE4:
    - Copy tất cả trong Views/Shared/_Layout.cshtml bỏ vào Views/Account/Login.cshtml
    - Xóa đi phần body ban đầu, nhưng giữ lại script trong body đó.
    - Copy mẫu trong file: D:\Nam4\HK2\LapTrinhUngDungWeb\TaiLieu\AdminLTE_4\node_modules\admin-lte\dist\examples\login.html
    - Copy phần body của mẫu trên vào Views/Account/Login.cshtml, không copy phần script.
    - Sửa tên thành NTQ Shop, sửa chữ 2 chỗ tiêu đề và nút thành Đăng nhập, Bỏ đi phần Remember Me, và phần Sign in using Facebook trở đi đến Register a new membership.
- Giao diện Account/ChangePassword: Sử dụng ChatGPT, copy mẫu source: ChangePassword.cshtml trên Classroom.
- Xóa đi các phần trong các Action ở các file ở trong Controllers trong project SV22T1020362.Admin để lại ban đầu cho phù hợp, hiển thị được các Views.
- Giao diện Supplier dùng ChatGPT, copy mẫu source trên Classroom:
    - Index.cshtml (Của Supplier)
- Làm giao diện trang đầu tiên phải chuẩn, để cóp những cái còn lại.
- Copy Index.cshtml của Supplier cho Index của: Customer, Shipper, Employee, Category.
- Giao diện Product/Views của Product: Copy mẫu source Index.cshtml trên Classroom.
- Hoàn thiện các Views ở trên.
- Tiếp tục: Giao diện của:
    - Supploer/Edit (Dùng cho cả Create và Edit)
    - Supplier/Delete
- Về nhà: Thiết kế nốt giao diện cho những cái còn lại, trừ Product và Order, cho: Customer, Shipper, Employee, Category.
    - Customer: ChangePassword như nào
    - Employee: ChangePassword, ChangeRoles như nào
    - Employee: Index nên thiết kế theo Card.

### Việc đã làm thêm khi về nhà
#### 02/02/2026
- Sửa lại các Controller về trạng thái ban đầu, chưa Post, chưa xử lý dữ liệu.
- Sửa lại các Views cho phù hợp với mẫu trên lớp. Bổ sung CRUD (Chỉ giao diện, chưa xử lý) cho các Views còn lại.
- Các Views đã sửa theo btvn:
    - Category: Delete, Edit.
    - Customer: ChangePassword, Delete, Edit.
    - Employee: ChangePassword, ChangeRole, Delete, Edit, Index (Dạng Card).
    - Shipper: Delete, Edit.
- Các Views đã làm và đã sửa trong 02/02/2026 (Trên lớp + nhà):
    - Account: Login, ChangePassword.
    - Category: Index, Edit, Delete.
    - Customer: Index, Edit, Delete, ChangePassword.
    - Employee: Index, Edit, Delete, ChangePassword, ChangeRole.
    - Product: Index.
    - Shipper: Index, Edit, Delete.
    - Supplier: Index, Edit, Delete.
- Các Views còn lại:
    - Home: Index.
    - Order.
    - Product: ngoại trừ Index.

# Công việc ngày 05/02/2026
## LiteCommerce Project
### Làm giao diện (Tiếp)
- Sửa lại giao diện phân quyền nhân viên theo các Role: Employee, Admin cho phù hợp (Employee -> ChangeRole),
- Làm tiêp giao diện đơn hàng:
    - Order/Create: 
        - Nhập thông tin và lưu vào CSDL, kể cả mặt hàng. Thông tin lưu vào Orders, OrderDetails. Cần: Ngày đặt hàng, thông tin khách hàng, nơi giao hàng, trạng thái. Còn những thông tin: Shipper, thời gian giao hàng, thời gian finish chưa có.
        - Còn OrderDetails là thêm các mặt hàng vào đơn hàng.
        - Xác định Business Workflow để làm nghiệp vụ.
        - Quá trình mua hàng:
            -  Bước 1: Tìm kiếm hàng cần bán.
            - Bước 2: Thêm vào giỏ hàng.
            - Bước 3: Xem thử mua đủ chưa ?
                - Chưa đủ -> Tìm kiếm sản phẩm tiếp (Quay lại bước 1). (Giao diện thiết kế cho phép khách hàng làm việc này).
                - Đủ -> Qua bước 4.
            - Bước 4: Kiểm tra giỏ hàng.
            - Bước 5: Sau khi kiểm tra giỏ hàng xong, cung cấp thông tin.
            - Bước 6: Đặt hàng.'
        - Thiết kế giao diện prototype.
        - Lập trình giao diện. Ảnh thầy có vấn đề: Chọn khách hàng nếu khách hàng quá nhiều (>1000 người) thì chọn không được.
        - Khách hàng muốn đăng ký tích điểm thì phải làm sao.
        - Lúc đặt hàng mà khách hàng chưa có thì phải tạo mới khách hàng cho họ.
    - Về nhà suy nghĩ và làm tiếp các giao diện còn lại:
        - Product
        - Order
        - Home có dashboard.
 
### Việc đã làm thêm khi về nhà
#### 21/02/2026
- Order/Create:
    - Đề xuất ý tưởng: Chọn khách hàng chỉ hiển thị 50 người được thêm đơn hàng gần đây nhất: Nối bảng Customers với bảng Orders, lọc ra Order gần nhất của Customer, null nếu không có - theo OrderTime; chọn các Customer có OrderTime gần nhất. Khi bấm tìm kiếm sẽ tìm theo tên hoặc ID khách hàng và lọc ra top 20 kết quả (Kèm ảnh).

#### 23/02/2026
- Đăng ký tích điểm thì làm sao.
- Thêm mới 3 trường và chọn khách hàng đã có ở phase 2.
- Thêm xong thành đơn hàng thì có trừ số lượng hiện tại trong Products không.
- Chỉ hiển thị top 50 khách hàng được thêm gần nhất, nếu tìm kiếm theo tên khách hàng thì hiện Top 30 kết quả tìm kiếm.
- Order/Create:
    - Ý tưởng: Chia ra 3 phase:
        - Phase 1: Gồm 2 phần: 
            - Phần trên: Là tìm kiếm và lựa chọn mặt hàng (trong Products), ban đầu có 30 mặt hàng mặc định, có thể nhập tên mặt hàng cần tìm, hiển thị danh sách mặt hàng thỏa điều kiện gồm: Tên ở đầu, hình ảnh ở dưới bên trái, giá bán và số lượng (số lượng có thể tùy chỉnh) ở dưới bên phái, dưới cùng là nút thêm vào giỏ hàng. Tìm kiếm 20 kết quả đầu tiên thỏa mãn.
            - Phần dưới: Là danh sách mặt hàng trong giỏ hàng. Khi thêm một mặt hàng vào giỏ hàng thông qua nút ở phần trên, thì thông tin mặt hàng sẽ được thêm vào danh sách này. Thông tin mặt hàng gồm: Số thứ tự, hình ảnh, tên, Đơn vị tính, Số lượng, Giá, Thành tiền (Số lượng x Giá), Thao tác: gồm nút để sửa số lượng, nút để xóa mặt hàng ở dòng tương ứng và nút checkbox để chọn. Ở phía dưới danh sách là thông tin tiền tổng cộng của tất cả thành tiền ở trên, sau đó ở dưới nữa là 2 nút: Xóa các hàng được chọn trong checkbox và Xóa giỏ hàng (Xóa tất cả hàng).
            - Sau khi đã thêm xác nhận giỏ hàng, người dùng bấm nút Tiếp (Giữ lại thông tin Phase 1) để chuyển qua Phase 2.
        - Phase 2: Là thông tin khách hàng và nơi giao hàng. Gồm:
            - Đầu tiên là trường Tên khách hàng, yêu cầu nhập ký tự. Khi chưa nhập gì thì hiển thị sẵn top 50 khách hàng được thêm gần nhất (Nếu số khách hàng hiện có quá 50) dưới dạng danh sách gợi ý dưới trường nhập tên. Nếu đã nhập ký tự thì hiển thị top 30 khách hàng theo ký tự tìm kiếm dưới dạng danh sách gợi ý dưới trường nhập tên. Nếu nhập tên mới (Không chọn trong danh sách gợi ý) thì tự nhập 2 trường còn lại: Tỉnh/thành và Địa chỉ, sau khi nhập xong thì thêm (Add) vào CSDL Customers. Nếu chọn trong danh sách gợi ý thì có 2 trường hợp: 1 là khách hàng được chọn đã có đủ thông tin Tỉnh/thành và địa chỉ - chỉ cần bấm Tiếp. 2 là khách hàng được chọn chưa có 2 trường trên, thì phải tự nhập và khi bấm Tiếp sẽ Update (Edit, cập nhật) thông tin vào CSDL Customers.
            - Ở dưới là 2 trường Tỉnh/thành và Địa chỉ, yêu cầu nhập ký tự, đã có mô tả ở trên.
            - Cuối cùng là nút Quay lại để quay lại Phase 1 (Giữ lại thông tin Phase 2), và nút Tiếp để chuyển qua Phase 3 (Giữ lại thông tin Phase 2).
        - Phase 3: Xác nhận cuối cùng (Hiện Pop-up) để tạo đơn hàng. Nếu bấm có thì lấy thông tin tài khoản hiện tại (Của Employees) và tạo đơn hàng, thêm đơn hàng vào Quản lý đơn hàng, trạng thái ban đầu là 1 - Đơn hàng vừa gửi/khởi tạo, chi tiết đơn hàng là danh sách mặt hàng. Nếu bấm Không thì quay lại Phase 2 (Giữ lại thông tin Phase 2). Tạo xong thì trừ số lượng tổng của các mặt hàng tương ứng có trong danh sách. Hiện mới là giao diện nên chưa cần xử lý. 
- Order/Index:
    - Ý tưởng: Là danh sách đơn hàng. Gồm:
        - Đầu tiên là thanh tìm kiếm theo ID order / tên khách hàng / trạng thái.
        - Tiếp đến là danh sách Order gồm: OrderID, tên khách, thời gian Order, trạng thái, thao tác: gồm nút Xóa Order để chuyển qua trang Order/Delete là giao diện xác nhận xóa giống mẫu Delete các trang Employee, Customer ở trên. OrderID sẽ là link liên kết đến trang Xem chi tiết đơn hàng.
- Order/Detail: Chi tiết đơn hàng, gồm:
    - Các thông tin tổng quan về đơn hàng, có trong CSDL bảng Orders (CustomerID, EmployeeID, ShipperID chuyển qua tên tương ứng), và danh sách mặt hàng có trong CSDL bảng OrderDetails, liên kết OrderID. Tiếp đến là thông tin khách hàng và thông tin nhân viên (Account tạo đơn hàng). 
    - Dưới cùng là các nút: Cập nhật để chỉnh sửa số lượng các mặt hàng và chỉnh sửa trạng thái đơn hàng và có thể thêm / chỉnh sửa thông tin shipper giống như phần thông tin khách hàng ở trên Order/Create; Khi chỉnh sửa xong thì ở dưới hiện 2 nút là Hủy và Lưu dữ liệu để hủy thay đổi hoặc lưu thay đổi và quay lại trang Order/Detail; nếu lưu thay đổi thì phải cập nhật số lượng tổng của Products, hiện mới làm giao diện nên chưa cần chú ý. Còn có nút Quay lại để quay lại Order/Index và nút Xóa để chuyển qua trang Order/Delete là giao diện xác nhận xóa đơn hàng.
    - Lưu ý nếu cập nhật trạng thái đơn hàng thành -2 (Từ chối) hoặc -1 (Hủy) thì lập tức cộng lại số lượng tổng Products của các hàng trong danh sách mặt hàng. Hiện chỉ giao diện nên chưa cần.
    - Có nút Quay lại ở dưới cùng để về Order/Index.
- Order/Delete: Giao diện xác nhận xóa đơn hàng giống mẫu các file trên. Có nút Quay lại để quay lại Order/Index và nút Xác nhận xóa để xóa thẳng.
- Product/Index: Đã có.
- Product/Detail: Thông tin chi tiết mặt hàng, gồm các trường trong CSDL bảng Products (SupplierID, CategoryID chuyển qua tên tương ứng, có ảnh mẫu riêng). Tiếp dưới 3 nút: Nút Quay lại để về Product/Index; Nút Danh sách thuộc tính để qua trang Product/ListAttributes/{ProductID} để quản lý thuộc tính của mặt hàng; Nút Thư viện ảnh để qua trang Product/ListPhotos/{ProductID} để quản lý trong thư viện ảnh của mặt hàng. Hoàn thành, đã hủy, từ chối thì không được đổi trạng thái nữa.
- Product/Edit: gồm Thêm và Cập nhật mặt hàng:
    - Thêm gồm 3 Phase:
        - Phase 1: Đầu tiên yêu cầu nhập các thuộc tính Products: Tên, Mô tả, chọn nhà cung cấp, chọn loại hàng (Giống bên thông tin khách hàng trong Order/Create), Đơn vị, Giá, Chọn ảnh ngoài (1 ảnh mẫu). Tiếp đến là nút Tiếp để qua Phase 2 và nút Quay lại để về Product/Index. Cần giữ lại thông tin Phase 1 này kể cả khi bấm Tiếp. Mỗi lần bấm Tiếp là lưu vào CSDL (Thêm hoặc Cập nhật) và lấy thông tin ProductID.
        - Phase 2: Chuyển trang Product/ListAttributes/{ProductID}. Bấm nút Thêm thuộc tính trong trang để thêm. Dưới cùng là nút Quay lại để về Phase 1 và nút Tiếp để qua Phase 3. Cần giữ lại thông tin Phase 2 này kể cả khi bấm Quay lại hoặc Tiếp. Nếu không có thuộc tính thì nút Tiếp sẽ là nút Bỏ qua để qua Phase 3 và không có dữ liệu thuộc tính.
        - Phase 3: Chuyển trang Product/ListPhotos/{ProductID}. Bấm nút Thêm ảnh trong trang để thêm. Có nút Quay lại để về Phase 2 và nút Xác nhận thêm mặt hàng để thêm mặt hàng và quay lại Product/Index. Giữ lại dữ liệu Phase 3 kể cả khi Quay lại. Nếu không có thư viện ảnh thì nút Xác nhận Thêm vẫn hoạt động và không có dữ liệu thư viện ảnh.
    - Cập nhật cũng giống Thêm, nhưng lưu ý ở Phase 1 có thể thêm nút Dừng bán để tích chọn, và cuối Phase 3 là Lưu dữ liệu thay vì Xác nhận Thêm.
- Product/Delete: Giao diện xác nhận Xóa Mặt hàng, có nút Quay lại để về Product/Index và nút Xác nhận xóa để xóa, giống mẫu các file trên.
- Product/ListAttributes: Giao diện giống bên Customer/Index. Có các nút: Thêm mới ở đầu bên phải thanh tìm kiếm theo tên thuộc tính; các nút trong Thao tác: Cập nhật, Xóa. Các nút hoạt động tương tự như bên Customer.
- Product/ListPhotos: Giao diện giống bên Customer/Index. Có các nút tương tự như Product/ListAttributes. Các nút hoạt động tương tự như bên Customer.
- Home/Index: Tham khảo Dashboard v1 của AdminLTE4 (file code index.html). Cần thống kê:
    - Doanh thu theo ngày / tháng / năm. Nếu là tháng và năm thì hiển thị dưới dạng biểu đồ đường lên / xuống như Dashboard v1 và canvas.
    - Tổng khách hàng / nhân viên/ nhà cung cấp / Shipper.
    - Tổng số đơn hàng / loại hàng / mặt hàng.
    - Vinh danh khách hàng mua nhiều đơn nhất / chi nhiều tiền nhất (Top 10).
- Bắt tay vào code (Claude AI với prompt yêu cầu như trên). Đã có các file code v1.0.
# Công việc ngày 02/03/2026
## LiteCommerce Project
### Bài tập
#### Các Controller và Action dự kiến (các chức năng dự kiến)
##### Home
- Home/Index
- 
##### Account
- Account/Login
- Account/Logout
- Account/ChangePassword

##### Supplier
- Supplier/Index    
- Supplier/Create
- Supplier/Edit/{id}
- Supplier/Delete/{id}

##### Customer
- Customer/Index
- Customer/Create
- Customer/Edit/{id}
- Customer/Delete/{id}
- Customer/ChangePassword/{id}

##### Shipper
- Shipper/Index
- Shipper/Create
- Shipper/Edit/{id}
- Shipper/Delete/{id}

##### Employee
- Employee/Index
- Employee/Create
- Employee/Edit/{id}
- Employee/Delete/{id}
- Employee/ChangePassword/{id}
- Employee/ChangeRole/{id}

##### Category
- Category/Index
- Category/Create
- Category/Edit/{id}
- Category/Delete/{id}

##### Product
- Product/Index
- Product/Create
- Product/Edit/{id}
- Product/Delete/{id}
- Product/ListAttributes/{id}
- Product/CreateAttribute/{id}
- Product/EditAttribute/{id}?attributeId={attributeId}
- Product/DeleteAttribute/{id}?attributeId={attributeId}
- Product/ListPhotos/{id}
- Product/CreatePhoto/{id}
- Product/EditPhoto/{id}?photoId={photoId}
- Product/DeletePhoto/{id}?photoId={photoId}

##### Order
- Order/Index
- Order/Search
- Order/Create
- Order/Detail/{id}
- Order/CreateCartItem/{id}
- Order/EditCartItem/{id}?productId={productId}
- Order/DeleteCartItem/{id}?productId={productId}
- Order/ClearCart
- Order/Accept/{id}
- Order/Shipping/{id}
- Order/Finish/{id}
- Order/Reject/{id}
- Order/Cancel/{id}
- Order/Delete/{id}
=> Thiếu CreateCartItem (Thêm hàng vào giỏ)
##### Ghi chú: Còn thiếu

### Sử dụng Views (wwwroot và Views) của thầy.
### Nhắc lại các kiến thức cũ: RenderBody, RenderPartialAsync.
### Kiến thức mới: RenderSectionAsync
### Với lệnh RenderPartialAsync có thể đặt trong các View thông thường. Nhưng RenderBody và RenderSectionAsync chỉ có thể trong _Layout

# Công việc ngày 05/03/2026
## LiteCommerce Project
### Phân tích Models
- Nguyên khối: Monotholic.
- Microsolic?
- Domain Knowledge
- CLean Architect (Phổ biến hiện nay)
- Models: Chia theo Domain (Miền nghiệp vụ). Các Domain: 
    - Data Dictionary (Theo Technical là Entity, Từ điển dữ liệu: Là các định nghĩa / thuật ngữ / khái niệm dùng chung)
        - Province.
    - Partner (Đối tác). (Theo Technical là Entity, Nếu quy mô lớn thì Customer phải nằm trong CRM - Customer Relationship Management).
        - Supplier
        - Customer
        - Shipper
    - HR (Theo Technical là Entity, Có thể thêm Department - Bộ phận, Salary - Lương)
        - Employee
    - Catalog (Sản phẩm, Theo Technical là Entity)
        - Category
        - Product
        - ProductAttribute
        - ProductPhoto
    - Sales (Bán hàng, Theo Technical là DTO) 
        - OrderStatus (Trong Data Dictionary hay Sales ? Vì cũng là thuật ngữ dùng cho trạng thái đơn hàng, nhưng chỉ dùng trong Sales)
        - Order
        - OrderDetail
    - Security (Bảo mật, Theo Technical là Entity)
        - UserAccount
    - Common (Những cái chung chung, gồm phân trang, số kết quả trả về... Theo Technical thì là View Models, hiển thị phân trang...)
- Nhớ đổi tên namespace ở code Models của thầy.
- Hệ thống cần chú trọng cách kiến trúc / tổ chức để có thể tồn tại lâu dài.

### Bổ sung Code cho Models
- Cần thêm 1 class ProductSearchInput trong Catalog kế thừa PaginationSearchInput (Chỉ tìm kiếm với dữ liệu đơn giản) với chức năng tìm kiếm phân trang cho Mặt hàng. (Bổ sung code)
- Tương tự với OrderSearchInput trong Sales.
- Cần thêm các class chứa được CustomerName, EmployeeName đáp ứng yêu cầu hiển thị trang.
- Hiển thị các trang trên phân trang dạng: <<< ... 5 6 7 8 9 10 11 12 13 14 15 ... >> >>> (Trang hiện tại là 10)

### Data Layers - Phân tích, code
- Khai báo và cài đặt các phép xử lý dữ liệu
- Chỉ khai báo, cài đặt thì hỏi AI
    - Interfaces: Dùng để định nghĩa các "giao diện" xử lý dữ liệu (Chưa dùng generic - Tái cấu trúc - Cái này nên dùng)
    - Ví dụ Supplier: ISupplierRepository, SupplierRepository
- Không được tạo DataLayers ở tầng trình diễn.
- TestController trong SV22T1020362.Admin/Controllers.
- Models thêm 1 số file code cho đủ hiển thị.
- Bài tập về nhà: Viết cho 6 đối tượng: Supplier, Customer, Shipper, Employee, Category, Product. Mỗi đối tượng cần có 2 file:
+ 1 file Interface I<TênĐT>Repository - cần hiểu và suy nghĩ để làm. Quan trọng là Interface (ý tưởng).
+ 1 file <TênĐT>Repository implements interface tương ứng ở trên theo SQL Server 2014 Developer Edition - AI Code theo interface.
- Tự làm để hiểu. Sau thầy gửi code.

### Việc làm khi về nhà
#### 05/03/2026 - Làm bài tập về nhà (Bổ sung Interface cho 6 đối tượng)
- Bổ sung code nhờ Claude AI, với prompt-2026_03_05.txt.
- Nghiệp vụ đổi mật khẩu,... nên đặt trong UserAccountRepository.

#### 06/03/2026 - Sửa lại file code, tách đổi mật khẩu,... qua UserAccountRepository trong DataLayers
- Sửa code nhờ Claude AI.

# Công việc ngày 09/03/2026
## LiteCommerce Project
- Xóa hết các phần trong DataLayers, để lại 2 thư mục: Interfaces và SQLServer.
- Interface: Khai báo những chức năng dự kiến cài đặt cho DataLaeyrs
- SQL Server: Imple những chức năng cần cài đặt cho SQL Server, kế thừa Interface.
- Cách cài đặt trên mỗi hệ quản trị cơ sở dữ liệu sẽ khác nhau.

### Phân tích các chức năng cho DataLayers
- Nhà cung cấp, khách hàng, người giao hàng, nhân viên, loại hàng: Tương đồng
    - Tìm kiếm phân trang: Đầu vào tìm kiếm, phân trang: có giá trị tìm kiếm, hiển thị thông tin ở trang nào, bao nhiêu dòng ở một trang, nhấn tìm kiếm hiển thị kết quả tìm kiếm.
        - Page, PageSize, SearchValue.
    - Lấy thông tin của 1 đối tượng dựa vào id.
    - Bổ sung 1 đối tượng vào CSDL.
    - Cập nhật 1 đối tượng trong CSDL.
    - Xóa 1 đối tượng ra khỏi CSDL dựa vào id.
    - Kiểm tra xem 1 đối tượng có dữ liệu liên quan hay không.
- Nên tạo hàm bất đồng bộ. Khi tạo hàm bất đồng bộ, phải bọc kiểu dữ liệu vào trong thẻ Task.
- Quy định một Interface IGenericRepository là mẫu dùng chung cho các đối tượng trên. Ở các file Interface của các đối tượng cụ thể, chỉ cần imple Interface rồi tạo các hàm đã định nghĩa trong IGeneric.
- Trong Customer và Employee, cần thêm hàm kiểm tra Email bị trùng không. Thì cần thêm một hàm IsValidEmail (Chưa định nghĩa trong IGeneric) trong Interface của đối tượng Customer và Employee.
- Nhưng không nên gộp lại Customer và Employee thành một Interface chung vì 2 thằng này thuộc 2 Domain khác nhau, sau này mở rộng sẽ có các hàm khác nhau.
- Xem tiếp: Mặt hàng:
    - Tìm kiếm phân trang (đầu vào có thay đổi)
        - Đầu vào có thay đổi: Thêm SupplierID, CategoryID, MinPrice, MaxPrice
    - Lấy thông tin 1 mặt hàng
    - Bổ sung mặt hàng
    - Cập nhật mặt hàng
    - Xóa mặt hàng
    - Kiểm tra dữ liệu liên quan?
    - Attribute: List/Get/Add/Update/Delete (Không cần IsUsed - kiểm tra dữ liệu liên quan vì chỉ phụ thuộc Product).
    - Photo: List/Get/Add/Update/Delete
    - Prototype rất quan trọng.
    - Cần có DTO để chuyển dữ liệu, nối các bảng để hiển thị cho đúng.
- Đơn hàng:
    - Tìm kiếm phân trang:
        - Đầu vào có thay đổi: OrderStatus, FromDate, ToDate (OrderSearchInput)
        - Đầu ra khác Entity, cần DTO: Danh sách cần CustomerName, Phone, EmployeeName, Total, Status (OrderDTO) (OrderSearchResult/OrderDetailResult).
        - Lấy Chi tiết đơn hàng làm chuẩn để tạo DTO cho cả chi tiết và thông tin ngoài danh sách, vì chi tiết nhiều thông tin hơn.
        - Tách ra 2 DTO
        - AcceptOrder về bản chất là UpdateOrder nhưng tên gọi khác.
        - Có nên tách ra IOrderDetailRepository hay gộp vào trong 1 file OrderRepository.
        - Gộp
        - OrderViewInfo (Thông tin chi tiết đơn hàng)
        - Tạo file OrderDetailViewInfo (Thông tin chi tiết mặt hàng trong đơn hàng)
        - Đổi tên: Ctrl - R - R (Đổi tên hết).
        - Lấy code của thầy cóp vào (09/03/2026).

### Việc làm khi về nhà
- Code các file repo.

# Công việc ngày 12/03/2026
## LiteCommerce Project
### Làm code
- Cài package NewtonSoft.Json- Trong Admin, tạo thư mục AppCodes, copy file ApplicationContext.cs vào thư mục này
- Sửa lại code của Program.cs theo mẫu
- Đối với các lớp trong AppCodes trong project Admin, bỏ phần đuôi .AppCodes ở sau.
- Tạo lớp Configuration trong BusinessLayers
- Construct trong Service trong BusinessLayers phải có Constructor là hàm static, không có public ở trước, ta không gọi nó mà nó tự chạy khi nó lần đầu tiên được sử dụng.
- Làm thay đổi dữ liệu: Thêm, Sửa, Xóa phải kiểm tra dữ liệu trước khi làm
- Các hàm trong static class đều phải là static.
- 1 nhân viên có thể đảm nhiệm nhiều vai trò khác nhau

### SV22T0102362.BusinessLayers
- Gửi AI tất cả các file trong Models, DataLayers (Intefaces, SQLServer).
- Cần kiểm tra dữ liệu ở các hàm Add, Update trong các Services.
- Cần thêm các Service khác tương tự 2 file Service DictionaryDataService, PartnerDataService ở trên.

### SV22T1020362.Admin: Admin/Controllers + Admin/Views + Admin/Models/ + ...
- Lấy dữ liệu đã gửi từ BusinessLayers và những file code mới trên BusinessLayers.
- Dùng nhiều nên import sẵn SV22T1020362.Models.Common vào _ViewImports.cshtml
- input phải có thuộc tính name mới gửi được lên server (Trong CustomerIndex.cshtml)
- bool không cần dấu ? ở sau vì không null, sửa trong Models/Controller.
- Check thử xem đã có danh sách khách hàng chưa trong CustomerIndex.cshtml.
- Models/PagedResult chưa đáp ứng được tìm kiếm khách hàng (Qua trang khác mất searchValue).
- Bổ sung trong thư mục Models của SV22T1020362.Admins gọi là ViewModel: PaginationSearchViewModel.cs
    - Thay vì chỉ dùng mỗi PagedResult, cần dùng lại SearchValue thì ta có thể bỏ cả 2 thằng vào và dùng cả 2.
    - Bằng cách này, ta khắc phục được lỗi chuyển trang mất SearhValue
- Trong CustomerIndex dùng PaginationSearchViewModel chứa cả PaginationSearchInput và PagedItems.
- Sửa lại trong CustomerIndex, quan trọng:
    -  <a class="page-link" href="~/Customer?page=@p.Page&searchValue=@Model.Input.SearchValue">@p.Page</a>
    - Trong form, có value để giữ lại giá trị tìm kiếm

- Dựa vào mẫu Customer/CustomerIndex và CustomerController đấy, làm tương tự cho: Suppler, Shipper, Employee, Category Product
- Order chưa, dùng AJAX.

### Tóm tắt BTVN
- 1. DataLayers phải hoàn thành
- 2. BusinessLayers làm thử khi về nhà, buổi sau đưa code ráp vào.

# Công việc ngày 16/03/2026
## LiteCommerce Project
### Code
- Cập nhật thêm chũ "Async" sau tên các hàm
- Sửa lại các chỗ trong DataLayers và BusinessLayers: Thay thế OrderSearchInfo thành OrderViewInfo
- Ghép Code BusinessLayers, sửa tên các hàm cho đúng.
- Thống nhất:
    - Models 
    - Interface (SQlServer, MySQL... tự cài đặt)
    - Business Logic (QUAN TRỌNG)
    - 
- Tạo mẫu Prompt prompt_2026_03_16.txt để làm tham chiêu đủ các phần trong Models, DataLayers, BusinessLayers (Chưa sửa, mới có code v1.0).
- Bắt đầu làm Admin:
    - Đầu tiên là Customer: Controller, Views
        - Vấn đề (1): Quay lại ở trang khác (Ví dụ Order) không nhớ đang làm ngang đâu -> Bất tiện, nên lưu lại số trang đang ở và dữ liệu tìm kiếm đang tìm.
        - Vấn đề (2): Gõ chữ tìm kiếm -> Tìm bị load lại toàn trang ->  Thừa, chỉ cần load lại danh sách thôi.
        - Trong wwwroot/js/site.js, đã có hàm paginationSearch -> Tìm kiếm phân trang bằng AJAX
        - CustomerController:
            - PAGESIZE=10
            - Bổ sung IActionResult tên Search(PaginationSearchInput input): Tìm kiếm và trả về kết quả phân trang
            - Xóa code Index: nhập đầu vào tìm kiếm và hiển thị kết quả tìm
            -> Tách chức năng -> xử lý vấn đề (1)
            - Lưu điều kiện tìm kiếm vào Session trong hàm Search() -> xử lý vấn đề (2)
            - SESSION là vùng nhớ được cấp phát cho từng phiên truy cập của người dùng, lưu dữ liệu trong Server,
            - SESSION lưu quá đà thì sẽ gây tràn bộ nhớ, nặng Server, nên cần cẩn thận.
            - Có thể lưu trên mạng, ketch đỡ nặng.
            - Giá trị dùng >= 2 lần -> Constant. Muốn chặt chẽ -> Enum
            - Dùng Constant SEARCH_CUSTOMER
            - Trong ApplicationContext.cs, có hàm SetSessionData(key: tên biến, value: giá trị của biến): Ghi dữ liệu vào session
            - Trong ApplicationContext.cs, có hàm GetSessionData<T>(key: tên biến | T: kiểu dl): Lấy dữ liệu SESSION
            - Đừng sử dụng HẰNG tùy tiện. Hằng = Ngữ nghĩa + Nhất quán
            - 3 nguyên tắc Action - View
            - Code tầng dưới giống nhau, chỉ khác phần Presentation.
        - CustomerIndex:
            - Sửa lại model là PaginationSearchInput
            - Ở thẻ form:
                - có data-target là nơi để lưu kết quả tìm kiếm
                - method: get là lấy dữ liệu, post...
                - action: hàm được gọi
                - onSubmit: hàm JS được gọi khi gửi form
            - Vấn đề là JS có thể bị vô hiệu hóa.
            - Input phải có name mới đẩy lên Server.
            - asp-for="SearchValue" tương đương: 
                - name="SearchValue" value = "@Model.SearchValue"
            - Bổ sung PageSize để đưa đủ vào PaginationSearchInput 
            - Chèn @section Scripts vào cuối file (Copy trong OrderIndex)
        - Tách code dưới CustomerIndex bỏ qua file CustomerSearch.
        - CustomerSearch:
            - model: PagedResult<Customer>
            - Layout = null;
            - Bỏ chữ .Result sau Model
            - Đổi link href và onclick của PageItem trong Search
    - BTVN: Tiếp theo làm cho các trang còn lại
    - Category thiếu trong BusinessLayers/CatalogDataService.
    - Test: SQL Server 2014 Profiler -> New Trace -> kết nối và khi chạy trên localhost -> Mở trang có truy vấn SQL thì thấy được trên trace -> Mở SQL Server Management Studio -> Chạy thử câu lệnh đó -> Xem bao lâu -> Tối ưu.
    - Vào Task List trong VS 2019, xem TODO thì phải làm
### BTVN làm index/search cho các trang còn lại, bổ sung phần phân trang cho tất cả đối tượng còn lại
- Đã sửa: CategoryRepository, SupplierRepository, ProductRepository, CatalogDataService, OrderRepository, _Layout.cshtml.
- Đã cập nhật:
    - Các file trong SV22T120362.BusinessLayers (Làm TODO)
    - Các file Controllers trong SV22T1020362.Admin/Controllers
    - Các file Index trong SV22T1020362.Admin/Views/Category, Customer, Employee, Shipper, Supplier, Order, Product.
    - Các file ListAttributes, ListPhotos trong SV22T1020362.Admin/Views/Product.
- Đã bổ sung:
    - Các file Search trong SV22T1020362/Views/Category, Customer, Employee, Shipper, Supplier, Order, Product.
- ...
- 

# Công việc ngày 16/03/2026
## LiteCommerce Project
### Code
- PAGESIZE HARDCODE ở các Controller --> Bỏ --> Thêm vào appsettings.json, dùng lại trong Controllers
- GetConfigValue() trong AppCodes/ApplicationContxt.cs, ApplicationContxt.cs thêm hàm PageSize trả về dữ liệu PageSize trong appsettings.json
- Các file Controllers gọi hàm trên
- Bổ sung AppCodes/SelectListHelper.cs (Code thầy)
- Cập nhật Create, Edit, Delete cho các Controller
- 3 nguyên tắc truyền dữ liệu Action -> View
- Kiểu dữ liệu trả về: trong return View(tên kiểu dữ liệu);
- Thẻ action trong form: Gửi dữ liệu về đâu để xử lý
- method=post: Gửi dữ liệu từ trình duyệt lên máy chủ một cách bảo mật.
- Đối tượng nhập dữ liệu muốn gửi dữ liệu lên server phải có thuộc tính name và giá trị thuộc tính name chính là tham số mình cần truyền
- asp-for: Chỉ dùng khi trên View có Model và đối tượng nhập dữ liệu gắn với 1 thuộc tính nào đó của Model
- asp-items: là 1 danh sách list các item
- checked trong thẻ input bỏ đi ở CustomerEdit, sửa lại IsActive -> IsLocked...
- Bổ sung action SaveData trong CustomerController là HttpPost
- Viết Modal hóa: ngắn gọn # viết tường minh.

### BT
- Làm cho Supplier tương tự Customer

### Tiếp
- Kiểm tra dữ liệu đầu vào: IsNullOrWhiteSpace
- ModelState -> AddModelState(key: Tên lỗi (Trùng tên với trường để báo lỗi trường), "Nội dung lỗi");
- nameof(...) -> Dùng trả về chuỗi của trong ngoặc
- Cập nhật + xóa khách hàng: Delete trong CustomerController
- Khi cần gửi những dữ liệu đơn giản dùng ViewBag, phức tạp dùng Model

### Tương tự làm cho các phần còn lại (Thêm, sửa, xóa)
- Nếu muốn thêm sửa chỉ hiển thị 1 dòng duy nhất thì sửa lại PaginationSearchInput, trong điều kiện ListAsync của Repository thì phải có trường cần tìm trong WHERE.

### Đã chỉnh sửa và bổ sung các file code
#### 21/03/2026, Prompt 2026_03_19
##### Prompt 1 
- CategoryController.cs
- Category/Edit
- Category/Delete
- ShipperController.cs
- Shipper/Edit
- Shipper/Delete
- SupplierController.cs
- Supplier/Edit
- Supplier/Delete
- EmployeeController.cs (lỗi RoleNames)
- Employee/Delete
- Employee/ChangePassword
- Employee/ChangeRole (lỗi RoleNames)
- CustomerController.cs
- ProductController.cs
- OrderController.cs
- ...

---
- TODO còn lại cần xử lý sau:
    - ChangeRole POST trong EmployeeController: cần thêm cột RoleNames vào Employee model hoặc tạo method riêng trong HRDataService để chỉ cập nhật RoleNames.
    - Order/Create: tính năng lập đơn hàng mới chưa triển khai.
    - Lấy EmployeeID thực từ Claims sau khi implement Authentication đầy đủ.
    - Xóa file ảnh vật lý khi cập nhật/xóa ảnh sản phẩm hoặc ảnh nhân viên.

##### Prompt 2
- Product/Edit
- Product/Delete
- Product/EditAttribute
- Product/EditPhoto
- Product/ListAttributes
- Product/ListPhotos
- Order/Detail
- Order/Delete
- Order/Accept
- Order/Reject
- Order/Cancel
- Order/Shipping
- Order/Finish
- OrderController.cs

##### Prompt 3
- IUserAccountRepository.cs
- EmployeeAccountRepository.cs
- CustomerAccountRepository.cs
- SecurityDataService.cs
- EmployeeController.cs
- Employee/ChangeRole.cshtml
- EmployeeRepository.cs
- Employee.cs

##### Prompt 4
- Employee.cs
- EmployeeRepository.cs
- Employee/ChangeRole

##### Prompt 5
Các file ChangePassword và ChangeRole

##### TODO
- Mật khẩu khách hàng chưa mã hóa khi ChangePassword Customer

##### Prompt 6
- Edit, Delete của: Supplier, Shipper, Employee, Category
- Search của: Employee
- Sửa lỗi Edit và Create của Employee bị lỗi không mở được do IsWorking bị NULL.
- OrderController.cs (Sửa lỗi TimeZone)
- OrderIndex (Thủ công từ ngày, đến ngày

#### 22/03/2026, Prompt 2026_03_22
- Đang làm được: 
    - Phân trang, tìm, thêm, sửa, xóa, đổi pass, phân quyền cho: 
        - Supplier, CUstomer, Shipper, Employee, Category, Product
    - Phân trang, tìm cho: Order
- Cần làm tiếp:
    - Mã hóa mật khẩu cho Customer, Employee khi đổi pass !
    - Xác thực đăng nhập
    - Order/Create và các trạng thái đơn hàng
    - Sửa, xóa item đơn hàng

- Đã làm được:
    - Tìm kiếm, phân trang, thêm, sửa, xóa cho các đối tượng, ngoại trừ Order/Create
    - Mã hóa mật khẩu
    - Trạng thái đơn hàng hiển thị các nút phù hợp

# Công việc ngày 23/03/2026
## LiteCommerce Project
### Code
- Bổ sung 2 file CryptHelper.cs và WebSecurityModels.cs (Trong AppCodes).
    - CryptHelper: Hàm mã hóa mật khẩu MD5
    - WebSecurityModels: Lưu thông tin người dùng, khi người dùng đăng nhập sẽ tạo ra cookies, kiểm tra cookies của người dùng: ID, tên, tên hiển thị, email, photo, quyền.
- Khai báo để dùng Authentication, Authorization cho Cookie trong Program.cs (Comment thêm).
- Tối thiểu phải có ID trong WebUserData để Authentication.
- Sửa Login.cshtml, AccountController
- Nguyên lý là phải try catch khi bắt lỗi
- Viết database cho Login, AccountController,...
- Sửa CustomerController.cs
- Authorize cho các đối tượng
- [Authorize] cho OrderController
- Có thể liệt dê danh sách các quyền và phân cách nhau bởi dấu phẩy
- Tạo Action AccessDeined.
- Tạo View AccessDenied
- Lấy thông tin của User [User]
- Mở rộng thêm phương thức có this trước đó + Kiểu dữ liệu trong tên biến của hàm được truyền vào.
- Đổi mật khẩu cũng dùng cái User.GetUserData() để biết đổi mật khẩu của ai.
- _Header.cshtml có đổi code.
- Trong AccountController cũng có Authorize, nhưng Login cũng trong -> AllowAnonymous
- Hoàn thiện Authorize cho các đối tượng còn lại
- 

### 1. Làm code
- Trong AppCodes/WebSecurityModels.cs có 3 Roles có thể cho nhân viên, sửa lại trong Views Employee ChangeRoles theo 3 Role cho hợp lệ, và kể cả các phần liên quan nếu cần.
- Kết nối phần AccountController với server CSDL, hoàn thiện AccountController.
- Bổ sung Authorize cho các Controller của các đối tượng còn lại.
- Làm những gì liên quan đến bảo mật, Authentication, Authorize.
- File code đã sửa:
    - Views/Employee/ChangeRole.cshtml
    - Controllers/EmployeeController.cs
    - BusinessLayers/HRDataService.cs
    - DataLayers/Interfaces/IEmployeeRepository.cs
    - DataLayers/SQLServer/EmployeeRepository.cs
    - Controllers/AccountController.cs
    - Views/Account/ChangePassword.cshtml 
    - Views/Account/AccessDenied.cshtml
    - Controllers/CategoryController.cs
    - Controllers/SupplierController.cs
    - Controllers/CustomerController.cs 
    - Controllers/ShipperController.cs
    - Controllers/EmployeeController.cs
    - Controllers/ProductController.cs
    - Controllers/HomeController.cs
- ControllerRoles được phép:
    - AccountController: [Authorize] (Login/AccessDenied dùng [AllowAnonymous])
    - HomeController: [Authorize] — mọi người dùng đã đăng nhập
    - CategoryController: Administrator, DataManager
    - ProductController: Administrator, DataManager
    - SupplierController: Administrator, DataManager
    - CustomerController: Administrator, DataManager
    - ShipperController: Administrator, DataManager
    - EmployeeController: Administrator
    - OrderController: Administrator, Sales (đã có)

- TODO: Chưa xóa vật lý ảnh không dùng nữa trong: wwwroot/images/employees và /products.

### 25/03/2026 - 26/03/2026
#### 1. Demo code Shop
- Các file trong SV22T1020362.Shop:
    - Program.cs
    - appsettings.json, appsettings.Development.jon
    - AppCodes
    - Controllers
    - Models
    - Views: 
        - _ViewImports
        - _ViewStart
        - Account
        - Cart
        - Home
        - Order
        - Product
        - Shared
    - wwwroot: shop.css, shop.js
- BusinessLayers/SalesDataService.cs
- IOrderRepository
- OrderRepository

# Công việc ngày 26/03/2026
## LiteCommerce
### Admin
#### HƯớng dẫn làm chức năng Order/Create
- Lưu giỏ hàng đang được tạo trong session, thoát trình duyệt là mất
- ShoppingCartHelper trong AppCodes -> giống DataService trong BusinessLayers, nhưng làm với session, bên kia làm trong DB.
- Bên Customer (Shop) không được cập nhật giá bán của hàng tnong giỏ, nhưng bên Admin thì được.
- site.js, site.css cần bỏ vào prompt
- Làm cái thêm, sửa, xóa, xem, tìm kiếm, xóa hết giỏ hàng, cập nhật các giao diện tương ứng
- Admin:
    - OrderController
    - Views/Order/Create
        - Form search (Cóp bên CustomerIndex)
        - section scripts
    - Views/Order/SearchProduct
        - Căt ra từ Create, sau đó ghép vào bằng AJAX
        - File mới tìm kiếm mặt hàng giống Product, đều chỉ cần tên
        - Copy bên CustomerSearch
        - Mỗi phần tử trong vòng lặp không dùng asp-for được
    - Views/Order/ShowCart
        - List đổi thành IEnumerable (Tổng quát)
        - Cắt ra từ Create sau đó ghép vào bằng AJAX
    - AppCodes
        - ApiResult
        - ShoppingCartHelper.cs
    - DeleteCartItem.cshtml: xóa hàng trong giỏ
    - ClearCart.cshtml: xóa tất cả hàng trong giỏ
    - EditCartItem.cshtml: cập nhật hàng trong giỏ, cập nhật số lượng.
    - SalesDataService.cs
- TODO: 
    - Những cái còn thiếu
    - Thông tin khách hàng và nơi giao hàng:
        - Không bắt buộc tên, không bắt buộc chọn tỉnh/thành, có thể POST trực tiếp
        - Các phần xem, tìm kiếm, thêm, sửa, xóa, xử lý đơn hàng trong Detail tương tự như giỏ hàng, tạo mới file.
    - Khi thêm đơn hàng chỉ cần mã khách hàng, địa chỉ đơn hàng
    - Làm trong SalesDataService, thay hàm AddOrderAsync cho tốt hơn.
    - Vậy sửa lại bên OrderRepository luôn hàm AddOrderAsync
    - Sau khi thêm xong, phải xóa giỏ hàng.
    - Còn: 2 cái xóa hàng và xóa hết hàng trong giỏ, lập đơn hàng, và những cái tương tự bên xử lý chi tiết đơn hàng (Tham khảo giỏ hàng).
    - Xử lý chi tiết đơn hầng: Dùng Ajax để load thông tin đơn hàng, thông tin khách hàng, thông tin giao hàng, chia nhỏ ra để load, ajax load danh sách hàng trong đơn hàng ở dưới cùng trong trang chi tiết đơn hàng.

#### Đã sửa 14:00 
- Controllers/OrderController.cs
    - action DeleteCartItem và ClearCart check Request.Method == "POST" nhưng JS gọi bằng fetch POST — tuy nhiên vấn đề thực sự là DeleteCartItem có route [HttpGet] mà JS gọi POST, nên nó không match. Tương tự ClearCart.
    - thay action CreateOrder
    - thêm các action mới cho detail, AJAX load từng phần + thêm/sửa/xóa hàng trong đơn
- BusinessLayers/SalesDataService.cs: 
    - thay phần AddOrderAsync overload thứ 2
- View/Order/Detail.cshtml: sửa lại để load AJAX
- Views/Order/_OrderDetails.cshtml: partial view danh sách mặt hàng trong đơn
- Views/Order/AddOrderItem.cshtml: partial view modal tìm & thêm hàng
- Views/Order/_SearchProductInDetail.cshtml: partial kết quả tìm kiếm trong modal
- Views/Order/EditOrderItem.cshtml: partial view modal sửa mặt hàng trong đơn
- Views/Order/DeleteOrderItem.cshtml: partial view modal xác nhận xóa hàng trong đơn

#### Đã sửa 17:00
* Trong project SV22T1020362.Admin (Dành cho quản trị viên) đã sửa:
1. Trong trang Lập đơn hàng (Order/Create), ở phần Thông tin khách hàng & nơi giao hàng, trường Khách hàng:
1.1. Đầu tiên trong phần Dropdown để Select sẽ hiển thị 50 khách hàng đầu tiên trong danh sách khách hàng, lấy danh sách này giống như bên Customer/Index. Ở cuối Dropdown sẽ có lựa chọn là Tìm kiếm thêm.
1.2. Khi bấm vào Tìm kiếm thêm, sẽ hiển thị bootstrap 5 modal với dạng tương tự như phần nhập thông tin bên Customer/Edit, nhưng trường Tên khách hàng khi bấm vào sẽ hiển thị dropdown tất cả khách hàng trong danh sách và có thể nhập. Nếu chọn trong dropdown thì các trường còn lại được bổ sung tương ứng dữ liệu khách hàng đó, còn nếu nhập thì cũng kiểm tra giống bên Customer/Edit, bắt nhập Tên khách hàng, Email đúng, chọn Tỉnh/Thành và thực hiện tạo khách hàng mới và lấy thông tin khách hàng luôn.
1.3. Khi Cuối modal có 2 nút Chọn và Hủy. Bấm chọn thì đóng modal, ghi nhớ thông tin khách hàng được chọn vào đơn hàng được lập (Lấy CustomerID) và hiển thị tên ở trường Tên khách hàng ở ngoài. Còn nếu bấm Hủy thì modal và không thay đổi.
1.4. Nếu không bấm Tìm kiếm thêm thì chọn trong dropdown (Không được nhập đối với Tên khách hàng ở ngoài modal) và ghi nhs thông tin khách hàng được chọn vào đơn hàng được lập. Nếu bấm Lập đơn hàng thì khi đó mới bắt đầu lưu thông tin khách hàng vào đơn hàng. Không chọn dropdown cũng được, có thể bổ sung sau khi tạo đơn.
1.5. Còn lại giữ nguyên. Tỉnh thành và Địa chỉ ở đây là DeliveryProvince và DeliveryAddress trong Order. Status ban đầu khi mới tạo là 1 - Đơn hàng vừa gửi/khởi tạo. 
1.6. Phải có thông tin nhân viên phụ trách khi tạo đơn thành công. Nhân viên phụ trách phải lấy từ Claims tài khoản nhân viên đang đăng nhập (WebSecurityModesl, các phần trong Account).

2. Trong trang chi tiết đơn hàng (Order/Detail): Khi vừa tạo đơn hàng mới (Trong Lập đơn hàng Order/Create) thành công, sẽ chuyển qua trang Order/Detail này và có: Trạng thái 1 - là Đơn hàng vừa gửi/khởi tạo,và thời gian tạo (OrderTime) lấy thời gian thực ngay lúc tạo, lúc này:
2.1. Ở phần Thông tin đơn hàng:
- Mã đơn hàng là mã của đơn vừa tạo, nhân viên phụ trách chưa có, Ngày lập (OrderTime) lấy thời điểm ngay lúc tạo đơn, ngày nhận đơn (AcceptTime) chưa có.
2.2. Ở phần Thông tin khách hàng:
- Nếu ở Lập đơn hàng trước đó chưa chọn khách hàng, thì bây giờ sẽ có nút Thêm khách hàng ở đầu. Bấm vào thì hiển thị modal và thực hiện chức năng trong modal giống yêu cầu 1.2 và 1.3, cụ thể là giống bên modal khi Tìm kiếm thêm trong Lập đơn hàng.
- Nếu có rồi thì không có gì cả, giữ nguyên.
- Thêm xong nút vẫn còn đó để chỉnh sửa nếu cần.
- Chưa bắt buộc chọn.
2.3. Ở phần Thông tin giao hàng:
- Chưa có thông tin người giao hàng, cũng chưa có nút gì cả.
2.4. Ở phần Danh sách mặt hàng:
- Có nút Thêm mặt hàng ở đầu, bấm vào sẽ hiển thị modal với giao diện và chức năng hiển thị, tìm kiếm, phân trang giống phần Lựa chọn hàng cần bán trong trang Lập đơn hàng (Order/Create). Khi bấm Thêm vào đơn hàng (Giống nút Thêm vào giỏ) thì sẽ thêm hàng vào danh sách mặt hàng trong đơn hàng. Cuối modal có nút Hủy nếu không muốn thêm.
- Ở cuối mỗi dòng dữ liệu mặt hàng, cũng sẽ có 2 nút hình Sửa và Xóa giống bên Danh sách mặt hàng đã chọn trong Lập đơn hàng, nhưng sẽ không có nút Xóa tất cả hàng ở dưới cùng. Và cũng không cho phép xóa tất cả hàng trong đơn, tối thiểu phải còn lại 1 hàng trong đơn hàng (1 dòng dữ liệu).
- Không bắt buộc phải thao tác ở phần này, có thể làm hoặc không.
- Thêm sửa xóa xong nút vẫn còn đó.
2.5. Phần xử lý đơn hàng: Có thể duyệt đơn hàng (Trạng thái 2: Đơn hàng đã chấp nhận); Hủy đơn hàng (Trạng thái -1: Đơn hàng đã bị hủy); Từ chối đơn hàng (Trạng thái -2: Đơn hàng bị từ chối).
2.6. Có thể Xóa đơn hàng (Nút Xóa). Cập nhật trạng thái đơn hàng. Còn lại giữ nguyên.

3. Trong trang chi tiết đơn hàng (Order/Detail): Khi duyệt đơn hàng qua đang ở trạng thái 2: Đơn hàng đã chấp nhận:
3.1. Ở phần thông tin đơn hàng:
- Cập nhật Ngày nhận đơn là thời điểm duyệt.
3.2. Ở phần Thông tin khách hàng:
- Giống bên Trạng thái 1 - Đơn hàng vừa gửi/khởi tạo, chỉ có điều nếu trong phần xử lý đơn hàng, bấm qua trạng thái 3: Đơn hàng đang được vận chuyển thì bắt buộc phải chọn khách hàng cho có trước khi chuyển qua trạng thái 3. Ngoài ra cũng phải kiểm tra đã chọn shipper chưa ở phần Thông tin giao hàng trước khi chuyển qua trạng thái 3 (Phần dưới này).
- Thêm xong nút vẫn còn đó để chỉnh sửa nếu cần.
3.3. Ở phần Thông tin giao hàng:
- Chưa có thông tin người giao hàng, có nút Thêm người giao hàng ở vị tri giống trên phần Thông tin khách hàng.
- Khi bấm cũng hiện modal bootstrap 5 với dạng tương tự như bên Shipper/Edit, nhưng trường Tên người giao hàng có thể chọn 1 người trong Dropdown tất cả người giao hàng, cũng có thể nhập. Chọn thì cũng lấy thông tin người được chọn; còn nhập thì kiểm tra dữ liệu giống bên Shipper/Edit là tên người giao hàng phải có.
- Cuối modal cũng có nút Hủy nếu chưa muốn lưu thay đổi, và có nút Chọn nếu hợp lệ thì thêm vào Người giao hàng và Điện thoại (nếu có Điện thoại). 
- Có thêm nút Thêm địa chỉ bên cạnh nút Thêm người giao hàng. Khi bấm cũng hiện modal bootstrap 5 yêu cầu nhập không được rỗng gồm Địa chỉ giao hàng và Tỉnh/thành. Bấm Chọn để thêm vào 2 trường tương ứng ở ngoài và Hủy để không lưu.
- Phần này bắt buộc nhập cả 4 trường thông tin trên nếu muốn qua trạng thái 3 - Đơn hàng đang được vận chuyển.
- Thêm xong nút vẫn còn đó để chỉnh sửa nếu cần.
3.4. Ở phần Danh sách mặt hàng:
- Không thể thay đổi được nữa, chỉ xem, không có nút hay thao tác gì nữa.
3.5. Phần xử lý đơn hàng: Có thể giao hàng vận chuyển (Trạng thái 3: Đơn hàng đang được vận chuyển); Hủy đơn hàng (Trạng thái -1: Đơn hàng đã bị hủy). Nếu qua trạng thái 3 phải có đầy đủ Thông tin khách hàng và Thông tin giao hàng như đã ghi ở trên.
3.6. Không thể Xóa đơn hàng. Cập nhật trạng thái đơn hàng. Còn lại giữ nguyên.

4. Trong trang chi tiết đơn hàng (Order/Detail): Khi đơn hàng đang ở trạng thái 3 - Đơn hàng đang được vận chuyển:
4.1. Ở phần thông tin đơn hàng:
- Không đổi
4.2. Ở phần Thông tin khách hàng:
- Không đổi
4.3. Ở phần Thông tin giao hàng:
- Không đổi
4.4. Ở phần Danh sách mặt hàng:
- Không đổi
4.5. Phần xử lý đơn hàng: Có thể hoàn tất (Trạng thái 4: Đơn hàng đã hoàn tất); Hủy đơn hàng (Trạng thái -1: Đơn hàng đã bị hủy).
4.6. Không thể Xóa đơn hàng. Cập nhật trạng thái đơn hàng. Còn lại giữ nguyên.

5. Trong trang chi tiết đơn hàng (Order/Detail): Khi đơn hàng đang ở trạng thái 4 - Đơn hàng đã hoàn tất:
5.1. Ở phần thông tin đơn hàng:
- Không đổi
5.2. Ở phần Thông tin khách hàng:
- Không đổi
5.3. Ở phần Thông tin giao hàng:
- Không đổi
5.4. Ở phần Danh sách mặt hàng:
- Không đổi
5.5. Phần xử lý đơn hàng: Không có thao tác.
5.6. Không thể Xóa đơn hàng. Cập nhật trạng thái đơn hàng. Các hàng trong danh sách mặt hàng phải trừ số lượng cho tổng số lượng các hàng tương ứng trong Product (Mặt hàng).

6. Trong trang chi tiết đơn hàng (Order/Detail): Khi đơn hàng đang ở trạng thái -1 - Đơn hàng đã bị hủy:
6.1. Ở phần thông tin đơn hàng:
- Không thể đổi
6.2. Ở phần Thông tin khách hàng:
- Không thể đổi
6.3. Ở phần Thông tin giao hàng:
- Không thể đổi
6.4. Ở phần Danh sách mặt hàng:
- Không thể đổi
6.5. Phần xử lý đơn hàng: Không có thao tác.
6.6. Không thể Xóa đơn hàng. Cập nhật trạng thái đơn hàng. Còn lại giữ nguyên.

7. Trong trang chi tiết đơn hàng (Order/Detail): Khi đơn hàng đang ở trạng thái -2 - Đơn hàng bị từ chối:
7.1. Ở phần thông tin đơn hàng:
- Không thể đổi
7.2. Ở phần Thông tin khách hàng:
- Không thể đổi
7.3. Ở phần Thông tin giao hàng:
- Không thể đổi
7.4. Ở phần Danh sách mặt hàng:
- Không thể đổi
7.5. Phần xử lý đơn hàng: Không có thao tác.
7.6. Không thể Xóa đơn hàng. Cập nhật trạng thái đơn hàng. Còn lại giữ nguyên.

- IOrderRepository.cs
- OrderRepository.cs
- SalesDataService.cs
- Admin/Controllers/OrderController.cs
- Views/Order/_OrderDetails.cshtml
- Views/Order/Detail.cshtml
- Views/Order/Create.cshtml
- Views/Order/Shipping.cshtml
- Views/Order/Delete.cshtml
- Views/Order/Cancel.cshtml
- Views/Order/Reject.cshtml
- Views/Order/Finish.cshtml
- Views/Order/Accept.cshtml
- Các lỗi:
    - Tìm kiếm trong trang OrderDetail, phần Thêm mặt hàng cho Danh sách mặt hàng thì bị văng ra trang khác và không có CSS. Cần tìm được reload mặt hàng ngay trong modal.
    - Phần Thêm mặt hàng cho Danh sách mặt hàng trong trang OrderDetail chưa hoạt động, hàng được chọn chưa được thêm vào.
    - Phần Cập nhật hàng và Xóa hàng cho các mặt hàng trong Danh sách mặt hàng trong trang OrderDetail chưa hoạt động.
    - Phần Thêm khách hàng ở Thông tin khách hàng trong trang OrderDetail chưa chọn được, chỉ mới có thể thêm mới. Cần chọn được từ danh sách khách hàng đã có. Tương tự với modal Tìm kiếm thêm trong Lập đơn hàng.
    - Phần người giao hàng, khi chọn người giao hàng đã có thì chưa load được danh sách người giao hàng từ CSDL.
    - Lỗi xử lý đơn hàng khi qua trạng thái 3: chuyển giao hàng (qua trạng thái 3) không được:
Microsoft.Data.SqlClient.SqlException (0x80131904): The number of rows provided for a FETCH clause must be greater then zero. at Microsoft.Data.SqlClient.SqlConnection.OnError(SqlException exception, Boolean breakConnection, Action`1 wrapCloseInAction) at Microsoft.Data.SqlClient.SqlInternalConnection.OnError(SqlException exception, Boolean breakConnection, Action`1 wrapCloseInAction) at Microsoft.Data.SqlClient.TdsParser.ThrowExceptionAndWarning(TdsParserStateObject stateObj, SqlCommand command, Boolean callerHasConnectionLock, Boolean asyncClose) at Microsoft.Data.SqlClient.TdsParser.TryRun(RunBehavior runBehavior, SqlCommand cmdHandler, SqlDataReader dataStream, BulkCopySimpleResultSet bulkCopyHandler, TdsParserStateObject stateObj, Boolean& dataReady) at Microsoft.Data.SqlClient.SqlDataReader.TryHasMoreRows(Boolean& moreRows) at Microsoft.Data.SqlClient.SqlDataReader.TryReadInternal(Boolean setTimeout, Boolean& more) at Microsoft.Data.SqlClient.SqlDataReader.ReadAsyncExecute(Task task, Object state) at Microsoft.Data.SqlClient.SqlDataReader.InvokeAsyncCall[T](SqlDataReaderBaseAsyncCallContext`1 context) --- End of stack trace from previous location --- at Dapper.SqlMapper.GridReader.ReadBufferedAsync[T](Int32 index, Func`2 deserializer) in /_/Dapper/SqlMapper.GridReader.Async.cs:line 233 at Dapper.SqlMapper.GridReader.ReadBufferedAsync[T](Int32 index, Func`2 deserializer) in /_/Dapper/SqlMapper.GridReader.Async.cs:line 241 at SV22T1020362.DataLayers.SQLServer.ShipperRepository.ListAsync(PaginationSearchInput input) in D:\Nam4\HK2\LapTrinhUngDungWeb\BaiTap\SV22T1020362\SV22T1020362.DataLayers\SQLServer\ShipperRepository.cs:line 47 at SV22T1020362.BusinessLayers.PartnerDataService.ListShippersAsync(PaginationSearchInput input) in D:\Nam4\HK2\LapTrinhUngDungWeb\BaiTap\SV22T1020362\SV22T1020362.BusinessLayers\PartnerDataService.cs:line 243 at AspNetCoreGeneratedDocument.Views_Order_Shipping.
b__12_0() in D:\Nam4\HK2\LapTrinhUngDungWeb\BaiTap\SV22T1020362\SV22T1020362.Admin\Views\Order\Shipping.cshtml:line 26 at Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext.GetChildContentAsync(Boolean useCachedResult, HtmlEncoder encoder) at Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper.ProcessAsync(TagHelperContext context, TagHelperOutput output) at Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner.g__Awaited|0_0(Task task, TagHelperExecutionContext executionContext, Int32 i, Int32 count) at AspNetCoreGeneratedDocument.Views_Order_Shipping.ExecuteAsync() in D:\Nam4\HK2\LapTrinhUngDungWeb\BaiTap\SV22T1020362\SV22T1020362.Admin\Views\Order\Shipping.cshtml:line 4 at Microsoft.AspNetCore.Mvc.Razor.RazorView.RenderPageCoreAsync(IRazorPage page, ViewContext context) at Microsoft.AspNetCore.Mvc.Razor.RazorView.RenderPageAsync(IRazorPage page, ViewContext context, Boolean invokeViewStarts) at Microsoft.AspNetCore.Mvc.Razor.RazorView.RenderAsync(ViewContext context) at Microsoft.AspNetCore.Mvc.ViewFeatures.ViewExecutor.ExecuteAsync(ViewContext viewContext, String contentType, Nullable`1 statusCode) at Microsoft.AspNetCore.Mvc.ViewFeatures.ViewExecutor.ExecuteAsync(ViewContext viewContext, String contentType, Nullable`1 statusCode) at Microsoft.AspNetCore.Mvc.ViewFeatures.ViewExecutor.ExecuteAsync(ActionContext actionContext, IView view, ViewDataDictionary viewData, ITempDataDictionary tempData, String contentType, Nullable`1 statusCode) at Microsoft.AspNetCore.Mvc.ViewFeatures.PartialViewResultExecutor.ExecuteAsync(ActionContext context, PartialViewResult result) at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.g__Awaited|30_0[TFilter,TFilterAsync](ResourceInvoker invoker, Task lastTask, State next, Scope scope, Object state, Boolean isCompleted) at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.Rethrow(ResultExecutedContextSealed context) at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.ResultNext[TFilter,TFilterAsync](State& next, Scope& scope, Object& state, Boolean& isCompleted) at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.g__Awaited|28_0(ResourceInvoker invoker, Task lastTask, State next, Scope scope, Object state, Boolean isCompleted) at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.g__Awaited|25_0(ResourceInvoker invoker, Task lastTask, State next, Scope scope, Object state, Boolean isCompleted) at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.Rethrow(ResourceExecutedContextSealed context) at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.Next(State& next, Scope& scope, Object& state, Boolean& isCompleted) at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.g__Awaited|20_0(ResourceInvoker invoker, Task lastTask, State next, Scope scope, Object state, Boolean isCompleted) at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.g__Awaited|17_0(ResourceInvoker invoker, Task task, IDisposable scope) at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.g__Awaited|17_0(ResourceInvoker invoker, Task task, IDisposable scope) at Microsoft.AspNetCore.Session.SessionMiddleware.Invoke(HttpContext context) at Microsoft.AspNetCore.Session.SessionMiddleware.Invoke(HttpContext context) at Microsoft.AspNetCore.Authorization.AuthorizationMiddleware.Invoke(HttpContext context) at Microsoft.AspNetCore.Authentication.AuthenticationMiddleware.Invoke(HttpContext context) at Microsoft.AspNetCore.Diagnostics.DeveloperExceptionPageMiddlewareImpl.Invoke(HttpContext context) ClientConnectionId:968151aa-fdc1-492b-90b2-327cc7339606 Error Number:10744,State:1,Class:15 HEADERS ======= Accept: */* Connection: keep-alive Host: localhost:5204 User-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/146.0.0.0 Safari/537.36 Accept-Encoding: gzip, deflate, br, zstd Accept-Language: en-US,en;q=0.9,vi;q=0.8 Cookie: .AspNetCore.Antiforgery.39A-_mUPJeg=CfDJ8OOoybDCW9JEsuDeu6k6vUfPriQ3yGPRNCy2wi1qM5e3lRTrlQCYQ0-zW3qnieUTdv5xRTjv79ePNiav1dIJGuU6jdCwqLBrdMbNxhxRfKBhCJ_318qIykPMUiCuPJ7A79S743bk1038xQMI8oq07qs; LiteCommerce.Admin=CfDJ8OOoybDCW9JEsuDeu6k6vUe5Isrg4j25-tiwS1IFy_XsCd8wFi_Bk_QgtRSYiis6Ws5DLQ1UsHe_EDX9MGDm2CIG6JZ5uGBlvS68DxqH4uLNAHG2eh8ebiZKhNejX72vlIoYdL9M9bHl3dLt7q5F94piAkV5PtVXpmUS7EuIcYWmKvzP0cLeY2Q5K7LVw0K1mMIvmlniP5Ci23UszlDVbpBp8gP4cGVzJ9moIpvkMJ2VlFyWPNECKvFnYKZpPPhSOsHGJjQWwLopsh06ZKa8Xl0W5DKQJWBgoGSKV0hndq0JpshXi04nlfyiPO8RlHJRG7bYnDV3xzAo1rA8Em5E8jsrFRsgmJW6Ab5u4lv5uHO9VvEgnLofa5YrxyWzfAqeH-b4vv2b87mqw7LObNiyB0v3Ebc8yhKgDScvB1QI17q0_yQLfCoQmCb1Fs3j809MsdpMN4Fx21aLqpd0xN0UuzAETBngKUHFRqJnNg17ET41iT_IdLMEYxMEKyuHErLWrMenS_UW3ZT0TaGAeV_oz0Goq1-O1Qm35062NZVDYGLhsCL8dG7n4e4kOddSrpWfbPipDTQhpi2cHpOv1lK024BgG6unI_K6Ukm8u8ku9oMiyxtujHgG8bR4_bCzZXogBijdlSfKd7tWsEEjZjtW8HPS4l8NHu8uVC03euTqncXxb47weYdfKqFmPOo3Dxb2Hgq30P7QBERh_-UxHI4x1J8; .AspNetCore.Session=CfDJ8OOoybDCW9JEsuDeu6k6vUdJnLWAoocDBWpFBYPaoygkCjQuo6Zu5Hmktnz%2FhAro20TCnHPCX%2BPF1oE6AxnBpyHslAC5uNEJQE1wHouzhGmXlwYKvtmSKD2GqlWlpdWtjxz5hmipzaDfm4bHZQ7UAL0zmbshZpz4E1HKPaCPpF7m Referer: http://localhost:5204/Order/Detail/1004 sec-ch-ua-platform: "Windows" sec-ch-ua: "Chromium";v="146", "Not-A.Brand";v="24", "Google Chrome";v="146" sec-ch-ua-mobile: ?0 Sec-Fetch-Site: same-origin Sec-Fetch-Mode: cors Sec-Fetch-Dest: empty
    - Thêm người giao hàng (shipper) đang bị lỗi:
        - Lỗi ở trên
        - Thêm lỗi: Tải lại thì bị mất ở giao diện. Khi thêm người giao hàng thi Update cho Order đang xử lý luôn lưu vào CSDL luôn. Lưu vào CSDL lúc được lúc không, chưa được.
        - Đổi nút Thêm người giao hàng thành nút Sửa người giao hàng khi đã có thông tin người giao hàng (Đã update) cho Order đang xử lý trong CSDL.
        - Sửa tương tự với Thông tin khách hàng và Địa chỉ trong Thông tin giao hàng nếu cần, nhưng hiện tại tôi thấy 2 cái đó hoạt động đúng rồi.

#### Đã sửa 22:00
- SQLServer/ShipperRepository.cs 
    - (thêm nhánh PageSize == 0 giống CategoryRepository
    - SQL Server 2014 không hỗ trợ FETCH NEXT 0 ROWS — phải dùng nhánh riêng khi PageSize = 0.
- SQLServer/OrderRepository.cs
    - đơn hàng không có khách hàng không hiển thị
    - WHERE (c.CustomerName LIKE @searchValue OR c.Phone LIKE @searchValue) — khi CustomerID là NULL thì LEFT JOIN cho c là NULL, điều kiện WHERE loại bỏ chúng.
- Controllers/OrderController.cs
    - action Shipping GET
- Views/Order/Shipping.cshtml 
    - fix lỗi PageSize=0 trong view
    - thay toàn bộ
- Views/Order/AddOrderItem.cshtml
    - fix tìm kiếm modal không bị bay ra trang khác
    - thay toàn bộ
- Views/Order/_SearchProductInDetail.cshtml
    - dùng addOrderItemFromModal mới
- Controllers/OrderController.cs
    - action SearchProductInDetail
    - Thay vì nhận ProductSearchInput input, nhận các param rời để tránh model binding bị lẫn
- Views/Order/Detail.cshtml
    - 3 vấn đề: reload modal, shipper button, customer chọn từ danh sách
- Views/Order/Create.cshtml
    - modal tìm kiếm thêm khách hàng: chọn từ danh sách
    - Chỉ sửa hàm confirmSelectCustomer trong @section Scripts
- File không cần thiết cần xóa
    - Views/Product/DeleteAttribute.cshtml
    - Views/Product/DeletePhoto.cshtml
    - Views/Product/Detail.cshtml
    - Models/PaginationSerachViewModel.cs

- TODO (Luồng cơ bản đã tạm OK):
    - Lập đơn hàng, Thông tin khách hàng và nơi giao hàng, tìm kiếm / thêm khách hàng, hiện modal ra thì trường Tên khách hàng chưa hiện danh sách Dropdown để chọn khách hàng đã có để tự điền thông tin, mới cho nhập mới.
    - OrderDetail, trạng thái 1, thêm mặt hàng, sửa xóa hàng trong Danh sách mặt hàng chưa được. Thêm mặt hàng không hiển thị hàng trong danh sách trong modal, tìm kiếm bị văng ra ngoài màn hình chi tiết lại. Sửa xóa bấm nút không tác dụng.
    - OrderDetail, trạng thái 1 và 2, thêm khách hàng, trong modal, trường tên khách hàng chưa hiển thị danh sách drop down để chọn khách hàng đã có được, mới chỉ thêm mới.
    - HomeIndex (Trang chủ) chưa chèn dữ liệu thực.
    - Làm SV22T1020362.Shop (Bên User).

#### 27/03/2026 07:32
- SalesDataService.cs
    - Bỏ employeeID trong SalesDataService.cs
    - Thêm employeeID trong Cancel

- OrderController.cs
    - CreateOrder bỏ employeeID
    - Thêm employeeID trong POST Cancel
    
- Views/Order/Create.cshtml
    - Dropdown khách hàng
    - Modal thêm khách hàng

- Views/Order/Detail.cshtml
    - Thay Section Script

- Views/Order/_OrderDetails.cshtml 
    — Fix nút sửa/xóa trong danh sách mặt hàng

- Views/Order/AddOrderItem.cshtml
    - Fix tìm kiếm không bay ra ngoài

- Views/Order/_SearchProductInDetail.cshtml
    - thay paginateInDetail và hàm submit

- Views/Home/Index
    - Chèn dữ liệu thực

- Sales/OrderViewInfo.cs
    - Thêm SumOfPrice

- OrderRepository.cs
    - Sửa hàm ListAsync() để có thể SELECT toàn bộ bên cạnh offset

- CustomerRepository.cs
    - Sửa hàm ListAsync() để có thể SELECT toàn bộ bên cạnh offset

TODO:
    - Thêm/sửa/xóa hàng trong Danh sách mặt hàng thuộc trang Chi tiết đơn hàng chưa được.

#### 27/03/2026 12:00
- wwwroot/js/site.js
- Views/Order/AddOrderItem.cshtml
- Views/Order/EditOrderItem.cshtml
- Views/Order/DeleteOrderItem.cshtml
- Qua Shop:
    - Program.cs
    - appsettings.json
    - AppCodes:
        - ApplicationContext.cs
        - CryptHelper.cs
        - WebSecurityModels.cs
        - ShoppingCartHelper.cs
    - Controllers:
        - AccountController.cs
        - HomeController
        - ProductController
        - CartController
        - OrderController.cs
    - Views:
        - Shared
            - _Layout.cshtml (Bỏ file _Footer, _Header)
        - _ViewImports.cshtml
        - _ViewStart.cshtml
        - Home
            - Index.cshtml
        - Product
            - Index.cshtml (Bỏ Search)
            - Detail.cshtml
        - Cart
            - Index.cshtml
            - Checkout.cshtml
            - OrderSuccess.cshtml
        - Order
            - Index.cshtml (Bỏ History, Checkout)
            - Detail.cshtml
        - Account
            - Login
            - Register
            - Profile
            - ChangePassword
    - Properties/launchSettings.json
    - wwwroot:
        - css/shop.css
        - js/shop.js
        - images
            - employees
            - products
        - lib
            - favicon.ico
            - bootstrap
                - bootstrap.min.css
                - bootstrap.bundle.min.js
                - bootstrap-icons.min.css (và font icons)
#### 28/03/2026 11:30
- Test:
1.Đăng ký tài khoản mới.
2. Đăng nhập vào hệ thống.
3. Quản lý thông tin cá nhân và mật khẩu.
4. Xem, tìm kiếm danh mục mặt hàng theo loại hàng, tên hàng, khoảng giá.
5. Xem thông tin chi tiết của mặt hàng.
6. Đưa hàng vào giỏ hàng
7. Quản lý giỏ hàng
8. Đặt mua hàng
9. Theo dõi trạng thái xử lý của đơn hàng.
10. Theo dõi lịch sử mua hàng của cá nhân


