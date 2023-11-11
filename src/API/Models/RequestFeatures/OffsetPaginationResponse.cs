namespace API.Models.RequestFeatures
{
    public record OffsetPaginationResponse<T>(IEnumerable<T> Data, OffsetPaginationMetadata Pagination);
}
