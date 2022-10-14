namespace Companies.Domain.Base.Persistence;

public interface IUnitOfWork
{
    Task Commit();
}