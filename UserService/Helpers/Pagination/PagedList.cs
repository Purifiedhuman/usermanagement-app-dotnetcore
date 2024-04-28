using Microsoft.EntityFrameworkCore;

namespace UserService.Helpers.Pagination;

public class PagedList<T>
{
    public int Page { get; }
    public int PageSize { get; }
    public int TotalCount { get; }
    public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
    public List<T> Items { get; }
    public bool hasNextPage => Page * PageSize < TotalCount;
    public bool hasPreviousPage => Page > 1;

    private PagedList(List<T> items, int page, int pageSize, int totalCount)
    {
        Items = items;
        Page = page;
        PageSize = pageSize;
        TotalCount = totalCount;
    }

    public static async Task<PagedList<T>> CreateAsync(IQueryable<T> query, int page, int pageSize)
    {
        var totalCount = await query.CountAsync();
        var items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

        return new PagedList<T>(items, page, pageSize, totalCount);
    }
}