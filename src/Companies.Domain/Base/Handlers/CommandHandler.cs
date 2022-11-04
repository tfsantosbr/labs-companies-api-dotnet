using Companies.Domain.Base.Models;

namespace Companies.Domain.Base.Handlers;

public abstract class CommandHandler
{
    protected Response RequestErrorsResponse(IEnumerable<Notification> notifications) =>
        Response.Error(notifications);

    protected Response ErrorResponse(string code, string errorMessage) =>
        Response.Error(new Notification(code, errorMessage));
}

public abstract class CommandHandler<T> where T : class
{
    protected Response<T> ErrorResponse(string code, string errorMessage) =>
        Response<T>.Error(new Notification(code, errorMessage));

    protected Response<T> RequestErrorsResponse(IEnumerable<Notification> notifications) =>
        Response<T>.Error(notifications);
}