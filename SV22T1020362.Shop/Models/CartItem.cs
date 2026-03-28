namespace SV22T1020362.Shop.Models
{
    /// <summary>
    /// Một mặt hàng trong giỏ hàng (lưu vào Session)
    /// </summary>
    public class CartItem
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; } = "";
        public string Unit { get; set; } = "";
        public string Photo { get; set; } = "";
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal SalePrice { get; set; }

        /// <summary>
        /// Thành tiền
        /// </summary>
        public decimal TotalPrice => Quantity * SalePrice;
    }
}