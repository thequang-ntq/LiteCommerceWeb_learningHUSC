using SV22T1020362.Models.Sales;

namespace SV22T1020362.Shop
{
    /// <summary>
    /// Tiện ích quản lý giỏ hàng lưu trong session (dành cho Shop)
    /// </summary>
    public static class ShoppingCartHelper
    {
        private const string CART_KEY = "ShopCart";

        /// <summary>
        /// Lấy giỏ hàng từ session (tạo mới nếu chưa có)
        /// </summary>
        public static List<OrderDetailViewInfo> GetCart()
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
        /// Lấy 1 mặt hàng trong giỏ theo ProductID
        /// </summary>
        public static OrderDetailViewInfo? GetItem(int productID)
            => GetCart().Find(x => x.ProductID == productID);

        /// <summary>
        /// Thêm hoặc cộng dồn mặt hàng vào giỏ
        /// </summary>
        public static void AddItem(OrderDetailViewInfo item)
        {
            var cart = GetCart();
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
        /// Cập nhật số lượng và giá bán
        /// </summary>
        public static void UpdateItem(int productID, int quantity, decimal salePrice)
        {
            var cart = GetCart();
            var item = cart.Find(x => x.ProductID == productID);
            if (item != null)
            {
                item.Quantity = quantity;
                item.SalePrice = salePrice;
                ApplicationContext.SetSessionData(CART_KEY, cart);
            }
        }

        /// <summary>
        /// Xóa 1 mặt hàng khỏi giỏ
        /// </summary>
        public static void RemoveItem(int productID)
        {
            var cart = GetCart();
            int index = cart.FindIndex(x => x.ProductID == productID);
            if (index >= 0)
            {
                cart.RemoveAt(index);
                ApplicationContext.SetSessionData(CART_KEY, cart);
            }
        }

        /// <summary>
        /// Xóa toàn bộ giỏ hàng
        /// </summary>
        public static void ClearCart()
            => ApplicationContext.SetSessionData(CART_KEY, new List<OrderDetailViewInfo>());

        /// <summary>
        /// Số lượng mặt hàng (dòng) trong giỏ
        /// </summary>
        public static int Count => GetCart().Count;
    }
}