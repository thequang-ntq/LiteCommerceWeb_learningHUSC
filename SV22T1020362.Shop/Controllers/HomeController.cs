using Microsoft.AspNetCore.Mvc;
using SV22T1020362.BusinessLayers;
using SV22T1020362.Models.Catalog;
using SV22T1020362.Models.Common;

namespace SV22T1020362.Shop.Controllers
{
    /// <summary>
    /// Trang chủ Shop - hiển thị danh mục và sản phẩm nổi bật
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// Trang chủ: hiển thị danh mục và sản phẩm đang bán
        /// </summary>
        public async Task<IActionResult> Index()
        {
            // Lấy danh mục để hiển thị menu
            var categories = await CatalogDataService.ListCategoriesAsync(
                new PaginationSearchInput { Page = 1, PageSize = 0, SearchValue = "" });

            // Lấy sản phẩm nổi bật (24 sản phẩm đầu đang bán)
            var products = await CatalogDataService.ListProductsAsync(new ProductSearchInput
            {
                Page = 1,
                PageSize = 24,
                SearchValue = "",
                CategoryID = 0,
                SupplierID = 0,
                MinPrice = 0,
                MaxPrice = 0
            });

            ViewBag.Categories = categories.DataItems;
            return View(products.DataItems);
        }
    }
}