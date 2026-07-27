namespace Client.Models;

public class FilterItem<T>
{
    public T Value { get; set; } = default!;

    public string Text { get; set; } = string.Empty;
}