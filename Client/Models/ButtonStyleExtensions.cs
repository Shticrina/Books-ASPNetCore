namespace Client.Models;

public static class ButtonStyleExtensions
{
    public static string ToCss(this ButtonStyle style)
    {
        return style switch
        {
            ButtonStyle.Primary => "btn-primary",
            ButtonStyle.Secondary => "btn-secondary",
            ButtonStyle.Success => "btn-success",
            ButtonStyle.Warning => "btn-warning",
            ButtonStyle.Danger => "btn-error",
            ButtonStyle.Ghost => "btn-ghost",
            ButtonStyle.Default => "btn-default",
            _ => "btn-primary"
        };
    }
}