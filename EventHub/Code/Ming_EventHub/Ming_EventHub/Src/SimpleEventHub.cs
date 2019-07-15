using System;
using System.Collections.Generic;

namespace Ming.EventHub
{
    /// <summary>
    /// 简单事件中心
    /// </summary>
    public class SimpleEventHub
    {
        private Dictionary<int, List<Action<IEventArgs>>> _events = new Dictionary<int, List<Action<IEventArgs>>>();

        /// <summary>
        /// 添加监听
        /// </summary>
        public void AddListener(int eventId, Action<IEventArgs> action)
        {
            List<Action<IEventArgs>> acts = null;

            if (_events.TryGetValue(eventId, out acts))
            {
                if (acts == null)
                {
                    acts = new List<Action<IEventArgs>>();

                    _events[eventId] = acts;
                }
            }
            else
            {
                acts = new List<Action<IEventArgs>>();

                _events[eventId] = acts;
            }

            acts.Add(action);
        }

        /// <summary>
        /// 分发
        /// </summary>
        public void Dispatch(int eventId, IEventArgs args)
        {
            List<Action<IEventArgs>> acts = null;

            _events.TryGetValue(eventId, out acts);

            if (acts != null)
            {
                for (int i = 0; i < acts.Count; i++)
                {
                    acts[i]?.Invoke(args);
                }
            }
        }

        /// <summary>
        /// 移除监听
        /// </summary>
        public void RemoveListener(int eventId, Action<IEventArgs> action)
        {
            List<Action<IEventArgs>> acts = null;

            if (_events.TryGetValue(eventId, out acts))
            {
                acts?.Remove(action);
            }
        }
    }
}
