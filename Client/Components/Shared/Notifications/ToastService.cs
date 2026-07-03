namespace Client.Components.Shared.Notifications;

public class ToastService
{
    public event Func<ToastMessage, Task>? OnShow;

    public Task Success(string message)
        => Show(message, ToastType.Success);

    public Task Error(string message)
        => Show(message, ToastType.Error);

    public Task Warning(string message)
        => Show(message, ToastType.Warning);

    public Task Info(string message)
        => Show(message, ToastType.Info);

    private Task Show(string message, ToastType type)
    {
        if (OnShow is null)
            return Task.CompletedTask;

        return OnShow.Invoke(new ToastMessage
        {
            Message = message,
            Type = type
        });
    }
}