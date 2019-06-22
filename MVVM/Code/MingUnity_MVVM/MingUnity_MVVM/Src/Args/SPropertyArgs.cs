namespace MingUnity.MVVM
{
    public struct SPropertyArgs<T> : IPropertyChangedArgs where T : struct
    {
        public T oldValue;

        public T newValue;

        public SPropertyArgs(T oldValue, T newValue)
        {
            this.oldValue = oldValue;

            this.newValue = newValue;
        }
    }
}
