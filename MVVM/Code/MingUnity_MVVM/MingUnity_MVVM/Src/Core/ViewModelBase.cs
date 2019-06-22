namespace MingUnity.MVVM
{
    /// <summary>
    /// 视图模型基类
    /// </summary>
    public abstract class ViewModelBase : IViewModel
    {
        public event OnPropertyChanged OnPropertyChangedEvent;

        /// <summary>
        /// 装载
        /// </summary>
        public abstract void Setup();

        /// <summary>
        /// 执行属性改变(类)
        /// </summary>
        protected void RaiseCPropertyChanged<T>(string propertyName, T oldValue, T newValue) where T : class
        {
            OnPropertyChangedEvent?.Invoke(propertyName, new CPropertyArgs<T>(oldValue, newValue));
        }

        /// <summary>
        /// 执行属性改变(值)
        /// </summary>
        protected void RaiseSPropertyChanged<T>(string propertyName, T oldValue, T newValue) where T : struct
        {
            OnPropertyChangedEvent?.Invoke(propertyName, new SPropertyArgs<T>(oldValue, newValue));
        }
    }
}
