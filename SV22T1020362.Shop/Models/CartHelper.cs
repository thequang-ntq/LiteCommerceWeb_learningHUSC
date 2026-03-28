using SV22T1020362.Shop.Models;

namespace SV22T1020362.Shop
{
    /// <summary>
    /// Tiện ích quản lý giỏ hàng qua Session
    /// </summary>
    public static class CartHelper
    {
        private const string CART_SESSION_KEY = "ShoppingCart";

        /// <summary>
        /// Lấy giỏ hàng từ session (trả về danh sách rỗng nếu chưa có)
        /// </summary>
        public static List<CartItem> GetCart()
        {
            return ApplicationContext.GetSessionData<List<CartItem>>(CART_SESSION_KEY) ?? new List<CartItem>();
        }

        /// <summary>
        /// Lưu giỏ hàng vào session
        /// </summary>
        public static void SaveCart(List<CartItem> cart)
        {
            ApplicationContext.SetSessionData(CART_SESSION_KEY, cart);
        }

        /// <summary>
        /// Xóa toàn bộ giỏ hàng
        /// </summary>
        public static void ClearCart()
        {
            SaveCart(new List<CartItem>());
        }

        /// <summary>
        /// Thêm sản phẩm vào giỏ. Nếu đã có thì cập nhật số lượng và giá.
        /// </summary>
        public static void AddToCart(CartItem item)
        {
            var cart = GetCart();
            var existing = cart.FirstOrDefault(c => c.ProductID == item.ProductID);
            if (existing != null)
            {
                existing.Quantity = item.Quantity;
                existing.SalePrice = item.SalePrice;
            }
            else
            {
                cart.Add(item);
            }
            SaveCart(cart);
        }

        /// <summary>
        /// Cập nhật số lượng và giá bán của một sản phẩm trong giỏ
        /// </summary>
        public static void UpdateCartItem(int productID, int quantity, decimal salePrice)
        {
            var cart = GetCart();
            var item = cart.FirstOrDefault(c => c.ProductID == productID);
            if (item != null)
            {
                item.Quantity = quantity;
                item.SalePrice = salePrice;
            }
            SaveCart(cart);
        }

        /// <summary>
        /// Xóa một sản phẩm khỏi giỏ
        /// </summary>
        public static void RemoveFromCart(int productID)
        {
            var cart = GetCart();
            cart.RemoveAll(c => c.ProductID == productID);
            SaveCart(cart);
        }

        /// <summary>
        /// Tổng số lượng sản phẩm trong giỏ
        /// </summary>
        public static int GetCartCount()
        {
            return GetCart().Sum(c => c.Quantity);
        }

        /// <summary>
        /// Tổng tiền của giỏ hàng
        /// </summary>
        public static decimal GetCartTotal()
        {
            return GetCart().Sum(c => c.TotalPrice);
        }
    }
}