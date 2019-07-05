namespace MingUnity.ElementCore
{
    /// <summary>
    /// 值类型参数
    /// </summary>
    public struct SPropertyArgs<T> : IPropertyChangedArgs where T : struct
    {
        public T value;

        public SPropertyArgs(T value)
        {
            this.value = value;
        }
    }
}
