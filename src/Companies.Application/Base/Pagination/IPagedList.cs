namespace Companies.Application.Base.Pagination;

public interface IPagedList<T> where T : new()
{
    int PageNumber { get; }
    int PageSize { get; }
    long TotalPages { get; }
    long TotalRecords { get; }
    IEnumerable<T> Data { get; }
}