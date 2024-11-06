namespace Companies.Application.Base.Models;

public class Notification
{
    public Notification(string code, string message)
    {
        Code = code;
        Message = message;
    }

    public string Code { get; set; } = default!;
    public string Message { get; set; } = default!;
}
