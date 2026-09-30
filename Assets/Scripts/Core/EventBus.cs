using System;
using System.Collections.Generic;

public interface IEvent { }

public static class EventBus<T> where T : IEvent
{
    private static readonly List<Action<T>> OnEventRaised = new List<Action<T>>();

    public static void Subscribe(Action<T> listener)
    {
        if (!OnEventRaised.Contains(listener))
        {
            OnEventRaised.Add(listener);
        }
    }

    public static void Unsubscribe(Action<T> listener)
    {
        if (OnEventRaised.Contains(listener))
        {
            OnEventRaised.Remove(listener);
        }
    }

    public static void Publish(T eventData)
    {
        // 리스트를 역순으로 순회하여, 이벤트 처리 중 구독 해제가 일어나도 에러가 나지 않도록 방지
        for (int i = OnEventRaised.Count - 1; i >= 0; i--)
        {
            OnEventRaised[i]?.Invoke(eventData);
        }
    }
}