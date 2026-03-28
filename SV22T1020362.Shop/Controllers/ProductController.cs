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
        /// Trang danh sách sản phẩm với lọc và tìm kiếm
        /// </summary>
        public async Task<IActionResult> Index(int categoryID = 0, int supplierID = 0,
            decimal minPrice = 0, decimal maxPrice = 0, string searchValue = "",
            int page = 1, int pageSize = 0)
        {
            // Sử dụng pageSize từ appsettings nếu không truyền vào
            if (pageSize == 0) pageSize = ApplicationContext.PageSize;

            var input = new ProductSearchInput
            {
                Page = page,
                PageSize = pageSize,
                SearchValue = searchValue,
                CategoryID = categoryID,
                SupplierID = supplierID,
                MinPrice = minPrice,
                MaxPrice = maxPrice
            };

            var result = await CatalogDataService.ListProductsAsync(input);

            // Lấy danh mục cho sidebar
            var categories = await CatalogDataService.ListCategoriesAsync(
                new PaginationSearchInput { Page = 1, PageSize = 0, SearchValue = "" });

            ViewBag.Categories = categories.DataItems;
            ViewBag.SearchInput = input;
            return View(result);
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