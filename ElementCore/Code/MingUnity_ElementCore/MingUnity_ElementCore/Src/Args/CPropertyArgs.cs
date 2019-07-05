namespace MingUnity.ElementCore
{
    /// <summary>
    /// 引用类型参数
    /// </summary>
    public class CPropertyArgs<T> : IPropertyChangedArgs where T : class
    {
        public T value;

        public CPropertyArgs(T value)
        {
            this.value = value;
        }
    }
}
