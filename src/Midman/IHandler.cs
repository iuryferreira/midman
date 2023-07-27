using Midman.DataTypes;

namespace Midman;

public interface IHandler<T>
{
    Task<Response> Handle(Context<T> context);
}

public interface IHandler<TRequest, TResponse>
{
    Task<Response<TResponse>> Handle(Context<TRequest> context);
}