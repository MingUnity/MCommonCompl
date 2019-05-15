namespace Ming.EventHub
{
    /// <summary>
    /// 通用事件参数
    /// </summary>
    public sealed class MEventArgs : IEventArgs
    {
        private static MEventArgs _empty;

        public static MEventArgs Empty
        {
            get
            {
                if (_empty == null)
                {
                    _empty = new MEventArgs();
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

        public MEventArgs(params object[] keys)
        {
            this._args = keys;
        }
    }
}
