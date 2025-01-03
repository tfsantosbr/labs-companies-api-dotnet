using Companies.Application.Abstractions.Results;

namespace Companies.Application.Abstractions.Models;

public class Response<T> : Response where T : class
{
    public T? Data { get; set; }

    public static Response<T> Ok(T data)
    {
        return new Response<T>()
        {
            Data = data
        };
    }

    public static new Response<T> Ok()
    {
        return new Response<T>();
    }

    public static new Response<T> Error(Error notification)
    {
        var response = new Response<T>();
        response.Notifications.Add(notification);

        return response;
    }

    public static new Response<T> Error(IEnumerable<Error> notifications)
    {
        var response = new Response<T>();

        foreach (var notification in notifications)
            response.Notifications.Add(notification);

        return response;
    }
}

public class Response
{
    public List<Error> Notifications { get; set; } = [];
    public bool HasNotifications => Notifications?.Any() == true;

    public static Response Error(Error notification)
    {
        var response = new Response();
        response.Notifications.Add(notification);

        return response;
    }

    public static Response Error(IEnumerable<Error> notifications)
    {
        var response = new Response();

        foreach (var notification in notifications)
            response.Notifications.Add(notification);

        return response;
    }

    public static Response Ok()
    {
        return new Response();
    }
}
