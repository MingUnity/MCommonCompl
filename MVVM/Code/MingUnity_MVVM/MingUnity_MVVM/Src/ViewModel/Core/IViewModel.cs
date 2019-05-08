using System.ComponentModel;

namespace MingUnity.MVVM.ViewModel
{
    /// <summary>
    /// 视图模型接口
    /// </summary>
    public interface IViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// 激活
        /// </summary>
        bool Active { get; set; }

        /// <summary>
        /// 装载
        /// </summary>
        void Setup();

        /// <summary>
        /// 卸载
        /// </summary>
        void UnSetup();
    }
}
