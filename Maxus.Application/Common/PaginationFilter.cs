namespace Maxus.Application.Common
{
    public class PaginationFilter
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int SortBy { get; set; } = 0; // Default sort field
        public string SortDir { get; set; } = "asc";
        public int? SearchColumn { get; set; }
        public string? SearchTerm { get; set; }

    }
}
