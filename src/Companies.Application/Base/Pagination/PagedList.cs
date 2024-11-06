namespace Companies.Application.Base.Pagination;

public class PagedList<T> : IPagedList<T> where T : new()
{
    public PagedList(IEnumerable<T> items, long count, int pageNumber, int pageSize)
    {
        TotalRecords = count;
        PageSize = pageSize;
        PageNumber = pageNumber;
        TotalPages = (long)Math.Ceiling(count / (double)pageSize);
        Data = items;
    }

    public int PageNumber { get; }
    public int PageSize { get; }
    public long TotalPages { get; }
    public long TotalRecords { get; }
    public IEnumerable<T> Data { get; private set; }
}

