namespace Client.Models;

public static class BadgeStyleExtensions
{
    public static string ToCss(this BadgeStyle style)
    {
        return style switch
        {
            BadgeStyle.Primary => "badge badge-primary",
            BadgeStyle.Secondary => "badge badge-secondary",
            BadgeStyle.Success => "badge badge-success",
            BadgeStyle.Warning => "badge badge-warning",
            BadgeStyle.Danger => "badge badge-error",
            BadgeStyle.Info => "badge badge-info",
            _ => "badge"
        };
    }
}