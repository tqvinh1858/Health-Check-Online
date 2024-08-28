using Microsoft.EntityFrameworkCore;

namespace BHEP.Contract.Abstractions.Shared;
public class PagedResult<T>
{
    public const int upperPageSize = 100;
    public const int defaultPageIndex = 1;
    public const int defaultPageSize = 10;

    public PagedResult(List<T> items, int pageIndex, int pageSize, int totalCount)
    {
        this.items = items;
        this.pageIndex = pageIndex;
        this.pageSize = pageSize;
        this.totalCount = totalCount;
    }

    public List<T> items { get; }
    public int pageIndex { get; }
    public int pageSize { get; }
    public int totalCount { get; }
    public bool HasNextPage => pageIndex * pageSize < totalCount;
    public bool HasPreviousPage => pageIndex > 1;

    public static async Task<PagedResult<T>> CreateAsync(IQueryable<T> query, int pageIndex, int pageSize)
    {
        pageIndex = pageIndex <= 0 ? defaultPageIndex : pageIndex;
        pageSize = pageSize > 0
            ? pageSize > upperPageSize
            ? upperPageSize : pageSize : defaultPageSize;

        var totalCount = await query.CountAsync();
        var items = await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
        return new(items, pageIndex, pageSize, totalCount);
    }

    public static PagedResult<T> Create(List<T> items, int pageIndex, int pageSize, int totalCount)
        => new(items, pageIndex, pageSize, totalCount);
}
