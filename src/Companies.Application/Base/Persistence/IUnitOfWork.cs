namespace Companies.Application.Base.Persistence;

public interface IUnitOfWork
{
    Task CommitAsync();
}