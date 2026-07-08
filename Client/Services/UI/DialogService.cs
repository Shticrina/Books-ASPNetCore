namespace Client.Services.UI;

public class DialogService
{
    public event Func<string, string, Task<bool>>? OnConfirm;

    public async Task<bool> ConfirmAsync(string title, string message)
    {
        if (OnConfirm is null)
            return false;

        return await OnConfirm.Invoke(title, message);
    }
}