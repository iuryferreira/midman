using Midman.DataTypes;

namespace Midman;

public delegate Task<Response<TResponse>> MediatorEventHandler<T, TResponse>(Context<T> context);
public delegate Task<Response> MediatorEventHandler<T>(Context<T> context);

public class Mediator : IMediator
{
    private readonly Dictionary<Type, object> _eventHandlers = new();

    public void Register<TRequest>(IHandler<TRequest> handler)
    {
        var eventType = typeof(TRequest);

        if (!_eventHandlers.ContainsKey(eventType))
        {
            _eventHandlers[eventType] = new List<MediatorEventHandler<TRequest>>();
        }

        ((List<MediatorEventHandler<TRequest>>)_eventHandlers[eventType]).Add(handler.Handle);
    }
    public void Register<TRequest, TResponse>(IHandler<TRequest, TResponse> handler)
    {
        var eventType = typeof(TRequest);

        if (!_eventHandlers.ContainsKey(eventType))
        {
            _eventHandlers[eventType] = new List<MediatorEventHandler<TRequest, TResponse>>();
        }

        ((List<MediatorEventHandler<TRequest, TResponse>>)_eventHandlers[eventType]).Add(handler.Handle);
    }

    public async Task<Response<TResponse>> Send<TRequest, TResponse>(TRequest request) where TRequest : Request
    {
        var eventType = typeof(TRequest);
        if (!_eventHandlers.TryGetValue(eventType, out var eventHandler))
        {
            Console.WriteLine($"Handler to {eventType.Name} not found.");
            return Response<TResponse>.None();
        }

        var handlers = (List<MediatorEventHandler<TRequest, TResponse>>)eventHandler;
        var context = new Context<TRequest>(request);

        foreach (var handler in handlers)
        {
            return await handler(context);
        }
        
        return Response<TResponse>.None();
    }
}