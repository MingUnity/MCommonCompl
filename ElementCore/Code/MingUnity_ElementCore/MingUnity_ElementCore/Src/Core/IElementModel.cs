namespace MingUnity.ElementCore
{
    public interface IElementModel : IPropertyChangedDispatcher
    {
        /// <summary>
        /// 装载数据
        /// </summary>
        void Setup();
    }
}
