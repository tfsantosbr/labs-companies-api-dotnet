namespace Companies.Application.Abstractions.Persistence;

public interface IUnitOfWork
{
    Task CommitAsync();
}