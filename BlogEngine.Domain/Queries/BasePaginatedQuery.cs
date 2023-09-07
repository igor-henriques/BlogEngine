namespace BlogEngine.Domain.Queries;

public record BasePaginatedQuery
{
    private const int DEFAULT_PAGE_NUMBER = 1;
    private const int DEFAULT_ITEMS_PER_PAGE = 10;

    public BasePaginatedQuery(int pageNumber, int itemsPerPage)
    {
        PageNumber = pageNumber <= 0 ? DEFAULT_PAGE_NUMBER : pageNumber;
        ItemsPerPage = itemsPerPage <= 0 ? DEFAULT_ITEMS_PER_PAGE : itemsPerPage;
    }

    public int PageNumber { get; init; }
    public int ItemsPerPage { get; init; }
}
