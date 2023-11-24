namespace Core.Models.RequestFeatures
{
    public record WorkoutParameters(bool? Template, DateTime? StartFrom, DateTime? EndTo, string? SearchTerm,
        int PageNumber = 1, int PageSize = 10, bool SortDescending = true);
}
