namespace BlogEngine.Web.Core.Models;

public sealed class PaginatedApiResult<TViewModel>
{
    public List<TViewModel> Data { get; set; }
    public int? TotalCount { get; set; }
    public int? TotalPages { get; set; }
    public int PageNumber { get; init; }
    public int ItemsPerPage { get; init; }
}
