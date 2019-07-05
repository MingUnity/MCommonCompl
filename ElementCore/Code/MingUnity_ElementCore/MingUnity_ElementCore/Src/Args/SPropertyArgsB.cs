namespace MingUnity.ElementCore
{
    /// <summary>
    /// 值类型参数
    /// 新旧值传递
    /// </summary>
    public struct SPropertyArgsB<T> : IPropertyChangedArgs where T : struct
    {
        public T oldValue;

        public T newValue;

        public SPropertyArgsB(T oldValue, T newValue)
        {
            this.oldValue = oldValue;

            this.newValue = newValue;
        }
    }
}
