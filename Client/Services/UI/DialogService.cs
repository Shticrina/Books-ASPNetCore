using Client.Models;

namespace Client.Services.UI;

public class DialogService
{
    public event Func<DialogOptions, Task<bool>>? OnConfirm;

    public async Task<bool> ConfirmAsync(DialogOptions options)
    {
        if (OnConfirm is null)
            return false;

        return await OnConfirm.Invoke(options);
    }
}