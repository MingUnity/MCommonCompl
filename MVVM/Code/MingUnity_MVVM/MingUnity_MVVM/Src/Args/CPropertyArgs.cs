namespace MingUnity.MVVM
{
    public class CPropertyArgs<T> : IPropertyChangedArgs where T : class
    {
        public T oldValue;

        public T newValue;

        public CPropertyArgs(T oldValue, T newValue)
        {
            this.oldValue = oldValue;

            this.newValue = newValue;
        }
    }
}
