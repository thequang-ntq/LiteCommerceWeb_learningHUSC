using Microsoft.AspNetCore.Mvc;
using SV22T1020362.BusinessLayers;
using SV22T1020362.Models.Catalog;
using SV22T1020362.Models.Common;

namespace SV22T1020362.Shop.Controllers
{
    /// <summary>
    /// Xem, tìm kiếm danh mục mặt hàng và chi tiết sản phẩm
    /// </summary>
    public class ProductController : Controller
    {
        private const string SEARCH_KEY = "ShopProductSearch";

        /// <summary>
        /// Trang danh sách sản phẩm — hiển thị form tìm kiếm, kết quả load qua AJAX
        /// </summary>
        public async Task<IActionResult> Index(int categoryID = 0, int supplierID = 0,
            decimal minPrice = 0, decimal maxPrice = 0, string searchValue = "",
            int page = 1, int pageSize = 0)
        {
            if (pageSize == 0) pageSize = ApplicationContext.PageSize;

            // Ưu tiên tham số URL; nếu không có thì lấy từ session
            ProductSearchInput input;
            bool hasQueryParam = categoryID > 0 || supplierID > 0 || minPrice > 0
                                 || maxPrice > 0 || !string.IsNullOrWhiteSpace(searchValue);

            if (hasQueryParam)
            {
                input = new ProductSearchInput
                {
                    Page = page,
                    PageSize = pageSize,
                    SearchValue = searchValue,
                    CategoryID = categoryID,
                    SupplierID = supplierID,
                    MinPrice = minPrice,
                    MaxPrice = maxPrice
                };
            }
            else
            {
                input = ApplicationContext.GetSessionData<ProductSearchInput>(SEARCH_KEY)
                        ?? new ProductSearchInput
                        {
                            Page = 1,
                            PageSize = pageSize,
                            SearchValue = "",
                            CategoryID = 0
                        };
            }

            // Lấy danh mục cho sidebar
            var categories = await CatalogDataService.ListCategoriesAsync(
                new PaginationSearchInput { Page = 1, PageSize = 0, SearchValue = "" });

            ViewBag.Categories = categories.DataItems;
            return View(input); // View nhận ProductSearchInput, kết quả tải AJAX
        }

        /// <summary>
        /// Trả về partial HTML kết quả tìm kiếm sản phẩm (dùng cho AJAX từ Index)
        /// </summary>
        public async Task<IActionResult> Search(ProductSearchInput input)
        {
            if (input.PageSize == 0) input.PageSize = ApplicationContext.PageSize;

            var result = await CatalogDataService.ListProductsAsync(input);

            // Lưu điều kiện tìm kiếm vào session để giữ lại khi quay lại
            ApplicationContext.SetSessionData(SEARCH_KEY, input);

            return View(result); // Views/Product/Search.cshtml — Layout = null
        }

        /// <summary>
        /// Xem chi tiết mặt hàng
        /// </summary>
        public async Task<IActionResult> Detail(int id)
        {
            var product = await CatalogDataService.GetProductAsync(id);
            if (product == null)
                return RedirectToAction("Index");

            var photos = await CatalogDataService.ListPhotosAsync(id);
            var attributes = await CatalogDataService.ListAttributesAsync(id);

            // Sản phẩm liên quan (cùng danh mục, lấy 8 sản phẩm)
            var related = await CatalogDataService.ListProductsAsync(new ProductSearchInput
            {
                Page = 1,
                PageSize = 8,
                SearchValue = "",
                CategoryID = product.CategoryID ?? 0,
            });

            ViewBag.Photos = photos;
            ViewBag.Attributes = attributes;
            ViewBag.Related = related.DataItems.Where(p => p.ProductID != id).Take(6).ToList();

            return View(product);
        }
    }
}