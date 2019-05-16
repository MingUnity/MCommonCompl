using MingUnity.MVVM.Model;

namespace MingUnity.MVVM.ViewModel
{
    /// <summary>
    /// 简单视图模型
    /// </summary>
    public sealed class SimpleViewModel : ViewModelBase<IModel>
    {
        /// <summary>
        /// 激活
        /// </summary>
        public override bool Active
        {
            get
            {
                return _active;
            }
            set
            {
                _active = value;

                RaisePropertyChanged("Active");
            }
        }

        /// <summary>
        /// 装载
        /// </summary>
        public override void Setup()
        {
            Active = _active;
        }
    }
}
