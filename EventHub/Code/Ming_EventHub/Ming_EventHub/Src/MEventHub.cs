using System.Collections.Generic;

namespace Ming.EventHub
{
    /// <summary>
    /// 事件处理中心
    /// </summary>
    public class MEventHub
    {
        private static MEventHub _instance;

        private static object _locker = new object();

        public static MEventHub Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_locker)
                    {
                        if (_instance == null)
                        {
                            _instance = new MEventHub();
                        }
                    }
                }

                return _instance;
            }
        }

        private MEventHub()
        {

        }

        private Dictionary<int, List<IEventListener>> _eventDic = new Dictionary<int, List<IEventListener>>();

        /// <summary>
        /// 添加监听
        /// </summary>
        public void AddListener(int eventId, IEventListener listener)
        {
            if (_eventDic != null)
            {
                List<IEventListener> listeners = null;

                if (_eventDic.TryGetValue(eventId, out listeners))
                {
                    if (listeners == null)
                    {
                        listeners = new List<IEventListener>();

                        _eventDic[eventId] = listeners;
                    }
                }
                else
                {
                    listeners = new List<IEventListener>();

                    _eventDic[eventId] = listeners;
                }

                listeners?.Add(listener);
            }
        }

        /// <summary>
        /// 移除监听
        /// </summary>
        public void RemoveListener(int eventId, IEventListener listener)
        {
            List<IEventListener> listeners = null;

            _eventDic?.TryGetValue(eventId, out listeners);

            if (listeners != null && listeners.Contains(listener))
            {
                listeners.Remove(listener);
            }
        }

        /// <summary>
        /// 分发
        /// </summary>
        public void Dispatch(int eventId, IEventArgs args)
        {
            List<IEventListener> listeners = null;

            _eventDic?.TryGetValue(eventId, out listeners);

            if (listeners != null)
            {
                for (int i = 0; i < listeners.Count; i++)
                {
                    IEventListener listener = listeners[i];

                    listener?.HandleEvent(eventId, args);
                }
            }
        }
    }
}
