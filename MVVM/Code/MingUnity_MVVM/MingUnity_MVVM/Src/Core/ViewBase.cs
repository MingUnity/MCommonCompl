using System;
using UnityEngine;

namespace MingUnity.MVVM
{
    /// <summary>
    /// 视图基类
    /// </summary>
    public abstract class ViewBase : IView
    {
        /// <summary>
        /// 视图模型
        /// </summary>
        protected IViewModel _viewModel;

        /// <summary>
        /// 根节点
        /// </summary>
        protected RectTransform _root;

        /// <summary>
        /// 视图模型
        /// </summary>
        public IViewModel ViewModel
        {
            get
            {
                return _viewModel;
            }
            set
            {
                if (_viewModel != null)
                {
                    _viewModel.OnPropertyChangedEvent -= ViewModelPropertyChanged;
                }

                _viewModel = value;

                if (_viewModel != null)
                {
                    SyncModel();

                    _viewModel.OnPropertyChangedEvent += ViewModelPropertyChanged;

                    _viewModel.Setup();
                }
            }
        }

        /// <summary>
        /// 激活
        /// </summary>
        public abstract bool Active { get; set; }

        /// <summary>
        /// 创建
        /// </summary>
        public abstract void Create(Transform parent, bool active, Action callback = null);

        /// <summary>
        /// 显示
        /// </summary>
        public abstract void Show(Action callback = null);

        /// <summary>
        /// 隐藏
        /// </summary>
        public abstract void Hide(Action callback = null);

        /// <summary>
        /// 注销
        /// </summary>
        public void Dispose()
        {
            OnDisposing();

            if (_viewModel != null)
            {
                _viewModel.OnPropertyChangedEvent -= ViewModelPropertyChanged;
            }

            if (_root != null)
            {
                GameObject.DestroyImmediate(_root.gameObject);
            }
        }

        /// <summary>
        /// 销毁
        /// </summary>
        protected virtual void OnDisposing() { }

        /// <summary>
        /// 同步数据模型
        /// </summary>
        protected virtual void SyncModel() { }

        /// <summary>
        /// 视图模型属性改变
        /// </summary>
        protected abstract void ViewModelPropertyChanged(string propertyName, IPropertyChangedArgs args);
    }
}
