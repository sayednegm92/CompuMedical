using Microsoft.EntityFrameworkCore;

namespace CompuMedical.Core.Helper;

public class PagedList<T>
{
    #region Fields
    public object Data { get; }
    public object MetaData { get; set; }
    public int PageNumber { get; }
    public int PageSize { get; }
    public int TotalCount { get; }
    public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
    public bool HasNextPage => PageNumber * PageSize < TotalCount;
    public bool HasPreviousPage => PageNumber > 1;
    #endregion

    #region Constractor
    private PagedList(object data, int pageNumber, int pageSize, int totalCount)
    {
        Data = data;
        PageNumber = pageNumber;
        PageSize = pageSize;
        TotalCount = totalCount;
    }
    #endregion
    #region Handel Functions
    public static async Task<PagedList<T>> CreateAsync(IQueryable<T> query, int pageNumber, int pageSize, CancellationToken cancellation)
    {
        if (query == null) throw new Exception("Empty");
        pageNumber = pageNumber <= 0 ? 1 : pageNumber;
        pageSize = pageSize <= 0 ? 10 : pageSize;
        var totalCount = await query.CountAsync(cancellation);
        var items = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync(cancellation);
        return new(items, pageNumber, pageSize, totalCount);
    }
    #endregion
}