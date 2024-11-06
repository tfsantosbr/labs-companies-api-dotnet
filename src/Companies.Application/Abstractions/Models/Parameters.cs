namespace Companies.Application.Abstractions.Models;

public abstract class Parameters
{
    private const int MaxPageSize = 50;

    private int _pageSize = 25;
    private string? _orderBy;

    public int Page { get; set; } = 1;

    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = value > MaxPageSize ? MaxPageSize : value;
    }

    public string? OrderBy
    {
        get => _orderBy;
        set => _orderBy = !string.IsNullOrWhiteSpace(value) ? value.Trim().ToLowerInvariant() : null;
    }

    public string? Query { get; set; }
}