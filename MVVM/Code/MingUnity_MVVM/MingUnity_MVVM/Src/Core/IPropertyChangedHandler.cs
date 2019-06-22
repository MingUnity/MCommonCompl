namespace MingUnity.MVVM
{
    /// <summary>
    /// 属性改变委托
    /// </summary>
    public delegate void OnPropertyChanged(string propertyName, IPropertyChangedArgs args);

    /// <summary>
    /// 属性改变通知
    /// </summary>
    public interface IPropertyChangedDispatcher
    {
        /// <summary>
        /// 属性改变事件
        /// </summary>
        event OnPropertyChanged OnPropertyChangedEvent;
    }
}
