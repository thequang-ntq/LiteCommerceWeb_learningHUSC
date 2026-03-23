using SV22T1020362.Models.Common;

namespace SV22T1020362.Admin.Models
{
    /// <summary>
    /// ViewModel dùng để biểu diễn kết quả đầu ra khi tìm kiếm và hiển thị
    /// dữ liệu dưới dạng phân trang
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PaginationSearchViewModel<T> where T : class
    {
        /// <summary>
        /// Đầu vào tìm kiếm
        /// </summary>
        public required PaginationSearchInput Input { get; set; }
        /// <summary>
        /// Đầu ra tìm kiếm
        /// </summary>
        public required PagedResult<T> Result { get; set; }
    }
}
