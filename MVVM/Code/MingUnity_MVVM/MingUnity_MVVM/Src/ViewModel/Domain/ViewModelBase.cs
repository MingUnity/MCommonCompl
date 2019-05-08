using System;
using System.ComponentModel;
using MingUnity.MVVM.Model;

namespace MingUnity.MVVM.ViewModel
{
    /// <summary>
    /// 视图模型基类
    /// </summary>
    public abstract class ViewModelBase<T> : IViewModel where T : IModel
    {
        private T _model;

        protected bool _active;

        /// <summary>
        /// 数据模型
        /// </summary>
        public T Model
        {
            get
            {
                return _model;
            }
            set
            {
                _model = value;

                Setup();
            }
        }

        /// <summary>
        /// 激活
        /// </summary>
        public abstract bool Active { get; set; }

        /// <summary>
        /// 属性改变事件
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 装载
        /// </summary>
        public abstract void Setup();

        /// <summary>
        /// 卸载
        /// </summary>
        public abstract void UnSetup();

        /// <summary>
        /// 执行属性改变
        /// </summary>
        protected void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
