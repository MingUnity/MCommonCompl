namespace Ming.EventHub
{
    /// <summary>
    /// 通用事件参数(Class)
    /// </summary>
    public class CommonCEventArgs<T> : IEventArgs where T : class
    {
        public T arg;

        public CommonCEventArgs(T arg)
        {
            this.arg = arg;
        }
    }
}
