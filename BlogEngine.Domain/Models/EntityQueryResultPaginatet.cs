namespace BlogEngine.Domain.Models;

public sealed class EntityQueryResultPaginated<TEntity>
{
    public List<TEntity> Data { get; set; }
    public int? TotalCount { get; set; }
    public int? TotalPages { get; set; }
    public int PageNumber { get; init; }
    public int ItemsPerPage { get; init; }

    public EntityQueryResultPaginated<TDtoData> MapToDto<TDtoData>(IEnumerable<TDtoData> dtoData)
    {
        return new EntityQueryResultPaginated<TDtoData>()
        {
            Data = dtoData?.ToList() ?? Enumerable.Empty<TDtoData>().ToList(),
            ItemsPerPage = ItemsPerPage,
            PageNumber = PageNumber,
            TotalCount = TotalPages,
            TotalPages = TotalCount
        };
    }
}
