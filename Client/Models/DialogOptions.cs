namespace Client.Models;

public class DialogOptions
{
    public string Title { get; set; } = "";

    public string Message { get; set; } = "";

    public string ConfirmText { get; set; } = "OK";

    public string CancelText { get; set; } = "Cancel";

    public ButtonStyle ConfirmStyle { get; set; } = ButtonStyle.Primary;
}