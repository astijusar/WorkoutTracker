namespace Core.Models.RequestFeatures
{
    public record OffsetPaginationResponse<T>(IEnumerable<T> Data, OffsetPaginationMetadata Pagination);
}
