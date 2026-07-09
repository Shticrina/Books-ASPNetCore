namespace Client.Models;

public class DialogResult<T>
{
    public bool Confirmed { get; set; }

    public T? Value { get; set; }
}