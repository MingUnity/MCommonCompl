using System;

namespace Ming.EventHub
{
    public interface IActionEventHub
    {
        void AddListener(int eventId, Action<IEventArgs> action);

        void Dispatch(int eventId, IEventArgs args);

        void RemoveListener(int eventId, Action<IEventArgs> action);
    }
}
