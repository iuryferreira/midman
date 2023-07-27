using Midman.DataTypes;

namespace Midman;

public interface IMediator
{
    Task<Response<TResponse>> Send<TRequest, TResponse>(TRequest request) where TRequest : Request;
}