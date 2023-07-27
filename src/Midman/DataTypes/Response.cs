namespace Midman.DataTypes;

public class Response : Response<object> { }

public class Response<T>
{
    private readonly T? _value;
    public bool HasValue => _value != null;

    public Response() { }

    public Response(T value)
    {
        _value = value;
    }

    public static Response<T> Some(T value)
    {
        if (value == null)
            throw new ArgumentNullException(nameof(value), "Cannot create Maybe with null value.");

        return new Response<T>(value);
    }

    public static Response<T> None()
    {
        return new Response<T>();
    }
    
    public static Response Empty()
    {
        return new Response();
    }
    

    public T? ValueOrDefault()
    {
        return HasValue ? _value : default;
    }

    public T? ValueOr(T? defaultValue)
    {
        return HasValue ? _value : defaultValue;
    }

    public Response<TResult> Map<TResult>(Func<T, TResult> mapper)
    {
        if (mapper == null)
            throw new ArgumentNullException(nameof(mapper), "Mapper function cannot be null.");

        return HasValue ? Response<TResult>.Some(mapper(_value!)) : Response<TResult>.None();
    }

    public override string ToString()
    {
        return (HasValue ? _value?.ToString() : "None")!;
    }
}