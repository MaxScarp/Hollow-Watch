using System;
using System.Collections.Generic;

public static class GameEventBus
{
    private static readonly Dictionary<Type, List<Delegate>> handlers;

    static GameEventBus()
    {
        handlers = new();
    }

    public static void Subscribe<T>(Action<T> handler)
    {
        Type type = typeof(T);

        if (!handlers.ContainsKey(type))
        {
            handlers[type] = new();
        }

        handlers[type].Add(handler);
    }

    public static void Unsubscribe<T>(Action<T> handler)
    {
        Type type = typeof(T);

        if (handlers.TryGetValue(type, out List<Delegate> list))
        {
            list.Remove(handler);
        }
    }

    public static void Publish<T>(T evt)
    {
        Type type = typeof(T);

        if (!handlers.TryGetValue(type, out List<Delegate> list))
        {
            return;
        }

        Delegate[] snapshot = list.ToArray();

        foreach (Delegate handler in snapshot)
        {
            (handler as Action<T>)?.Invoke(evt);
        }
    }
}
