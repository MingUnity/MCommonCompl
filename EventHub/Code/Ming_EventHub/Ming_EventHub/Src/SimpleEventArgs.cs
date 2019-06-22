namespace Ming.EventHub
{
    /// <summary>
    /// 简单事件参数
    /// </summary>
    public sealed class SimpleEventArgs : IEventArgs
    {
        private static SimpleEventArgs _empty;

        public static SimpleEventArgs Empty
        {
            get
            {
                if (_empty == null)
                {
                    _empty = new SimpleEventArgs();
                }

                return _empty;
            }
        }

        private object[] _args;

        /// <summary>
        /// 参数集
        /// </summary>
        public object[] Args
        {
            get
            {
                return _args;
            }
        }

        public SimpleEventArgs(params object[] keys)
        {
            this._args = keys;
        }
    }
}
