namespace Ming.EventHub
{
    /// <summary>
    /// 通用事件参数
    /// </summary>
    public class CommonEventArgs<T> : IEventArgs
    {
        public T arg;

        public CommonEventArgs(T arg)
        {
            this.arg = arg;
        }
    }
}
