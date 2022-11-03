namespace Companies.Domain.Base.Models;

public class Response<T> where T: class
{
    public T? Data { get; set; }
    public List<Notification> Notifications { get; set; } = new List<Notification>();
    public bool HasNotifications => Notifications?.Any() == true;

    public static Response<T> Ok(T data)
    {
        return new Response<T>()
        {
            Data = data
        };
    }

    public static Response<T> Error(Notification notification)
    {
        var response = new Response<T>();
        response.Notifications.Add(notification);

        return response;
    }
}
