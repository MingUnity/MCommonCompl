namespace MingUnity.ElementCore
{
    /// <summary>
    /// 引用类型参数
    /// 新旧值传递
    /// </summary>
    public class CPropertyArgsB<T> : IPropertyChangedArgs where T : class
    {
        public T oldValue;

        public T newValue;

        public CPropertyArgsB(T oldValue, T newValue)
        {
            this.oldValue = oldValue;

            this.newValue = newValue;
        }
    }
}
