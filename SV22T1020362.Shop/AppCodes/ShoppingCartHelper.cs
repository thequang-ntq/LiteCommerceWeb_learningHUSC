using SV22T1020362.Models.Sales;

namespace SV22T1020362.Shop
{
    /// <summary>
    /// Tiện ích quản lý giỏ hàng.
    /// - Khi chưa đăng nhập: lưu trong Session
    /// - Khi đã đăng nhập: lưu trong CSDL (Orders với Status=0)
    /// ShoppingCartHelper chỉ dùng cho thao tác Session phía client.
    /// Các thao tác CSDL được gọi trực tiếp qua SalesDataService từ Controller.
    /// </summary>
    public static class ShoppingCartHelper
    {
        private const string CART_KEY = "ShopCart";

        /// <summary>
        /// Lấy giỏ hàng từ Session (dành cho khách chưa đăng nhập)
        /// </summary>
        public static List<OrderDetailViewInfo> GetSessionCart()
        {
            var cart = ApplicationContext.GetSessionData<List<OrderDetailViewInfo>>(CART_KEY);
            if (cart == null)
            {
                cart = new List<OrderDetailViewInfo>();
                ApplicationContext.SetSessionData(CART_KEY, cart);
            }
            return cart;
        }

        /// <summary>
        /// Lấy 1 mặt hàng trong giỏ Session theo ProductID
        /// </summary>
        public static OrderDetailViewInfo? GetSessionItem(int productID)
            => GetSessionCart().Find(x => x.ProductID == productID);

        /// <summary>
        /// Thêm hoặc cộng dồn mặt hàng vào giỏ Session
        /// </summary>
        public static void AddSessionItem(OrderDetailViewInfo item)
        {
            var cart = GetSessionCart();
            var existing = cart.Find(x => x.ProductID == item.ProductID);
            if (existing == null)
                cart.Add(item);
            else
            {
                existing.Quantity += item.Quantity;
                existing.SalePrice = item.SalePrice;
            }
            ApplicationContext.SetSessionData(CART_KEY, cart);
        }

        /// <summary>
        /// Cập nhật số lượng và giá bán trong giỏ Session
        /// </summary>
        public static void UpdateSessionItem(int productID, int quantity, decimal salePrice)
        {
            var cart = GetSessionCart();
            var item = cart.Find(x => x.ProductID == productID);
            if (item != null)
            {
                item.Quantity = quantity;
                item.SalePrice = salePrice;
                ApplicationContext.SetSessionData(CART_KEY, cart);
            }
        }

        /// <summary>
        /// Xóa 1 mặt hàng khỏi giỏ Session
        /// </summary>
        public static void RemoveSessionItem(int productID)
        {
            var cart = GetSessionCart();
            int index = cart.FindIndex(x => x.ProductID == productID);
            if (index >= 0)
            {
                cart.RemoveAt(index);
                ApplicationContext.SetSessionData(CART_KEY, cart);
            }
        }

        /// <summary>
        /// Xóa toàn bộ giỏ hàng Session
        /// </summary>
        public static void ClearSessionCart()
            => ApplicationContext.SetSessionData(CART_KEY, new List<OrderDetailViewInfo>());

        /// <summary>
        /// Số lượng dòng trong giỏ Session
        /// </summary>
        public static int SessionCount => GetSessionCart().Count;
    }
}