using System;
using System.Collections.Generic;

namespace Core
{
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
            // 발행 도중 삭제되는 경우에도 정상 처리되도록 스냅샷을 만든다
            var listenersSnapsoht = OnEventRaised.ToArray();
            foreach (var listener in listenersSnapsoht)
            {
                listener?.Invoke(eventData);
            }
        }
    }
}