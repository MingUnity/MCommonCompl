namespace Ming.EventHub
{
    /// <summary>
    /// 通用事件参数(Struct)
    /// </summary>
    public struct CommonSEventArgs<T> : IEventArgs where T : struct
    {
        public T arg;

        public CommonSEventArgs(T arg)
        {
            this.arg = arg;
        }
    }
}
