namespace MingUnity.MVVM
{
    /// <summary>
    /// 视图模型接口
    /// </summary>
    public interface IViewModel : IPropertyChangedDispatcher
    {
        /// <summary>
        /// 装载
        /// </summary>
        void Setup();
    }
}
