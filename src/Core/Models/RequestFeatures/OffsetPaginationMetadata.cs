namespace Core.Models.RequestFeatures
{
    public record OffsetPaginationMetadata(int TotalCount, int PageNumber, int PageSize)
    {
        public int PageNumber { get; set; } = PageNumber;
        public int PageSize { get; set; } = PageSize;
        public int TotalPages { get; set; } = (int)Math.Ceiling(TotalCount / (double)PageSize);
        public bool HasPrevious => PageNumber > 1;
        public bool HasNext => PageNumber < TotalPages;
    }
}
