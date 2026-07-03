namespace Client.Components.Shared.Notifications;

public class ToastMessage
{
    public Guid Id { get; init; } = Guid.NewGuid();

    public ToastType Type { get; init; }

    public string Message { get; init; } = string.Empty;

    public int Duration { get; init; } = 3000;
}