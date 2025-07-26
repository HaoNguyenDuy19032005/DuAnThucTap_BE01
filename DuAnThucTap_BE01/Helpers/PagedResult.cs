using System;
using System.Collections.Generic;

namespace DuAnThucTap_BE01.Helpers
{
    /// <summary>
    /// Lớp chứa kết quả trả về của một danh sách đã được phân trang.
    /// </summary>
    /// <typeparam name="T">Kiểu dữ liệu của các phần tử trong danh sách.</typeparam>
    public class PagedResult<T>
    {
        /// <summary>
        /// Danh sách các mục trên trang hiện tại.
        /// </summary>
        public List<T> Items { get; set; }

        /// <summary>
        /// Số trang hiện tại.
        /// </summary>
        public int PageNumber { get; set; }

        /// <summary>
        /// Kích thước của trang (số mục mỗi trang).
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Tổng số mục trong toàn bộ danh sách (trước khi phân trang).
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// Tổng số trang.
        /// </summary>
        public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);

        /// <summary>
        /// True nếu có trang trước đó.
        /// </summary>
        public bool HasPreviousPage => PageNumber > 1;

        /// <summary>
        /// True nếu có trang kế tiếp.
        /// </summary>
        public bool HasNextPage => PageNumber < TotalPages;

        public PagedResult()
        {
            Items = new List<T>();
        }
    }
}
