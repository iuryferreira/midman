namespace Midman;

public class Context<T>
{
    public T Value { get; }

    public Context(T value)
    {
        Value = value;
    }
}