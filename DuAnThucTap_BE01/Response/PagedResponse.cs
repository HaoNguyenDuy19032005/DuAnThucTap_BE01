namespace DuAnThucTap_BE01.Response
{
    public class PagedResponse<T>
    {
        public int PageNumber { get; set; }
        // Kích thước trang (số mục mỗi trang)
        public int PageSize { get; set; }
        // Tổng số trang
        public int TotalPages { get; set; }
        // Tổng số mục
        public int TotalRecords { get; set; }
        // Dữ liệu của trang hiện tại
        public IEnumerable<T> Data { get; set; }

        public PagedResponse(IEnumerable<T> data, int pageNumber, int pageSize, int totalRecords)
        {
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
            this.TotalRecords = totalRecords;
            this.TotalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);
            this.Data = data;
        }
    }
}
