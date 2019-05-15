using System;
using MingUnity.MVVM.ViewModel;
using UnityEngine;
using System.ComponentModel;

namespace MingUnity.MVVM.View
{
    /// <summary>
    /// 视图基类
    /// </summary>
    public abstract class ViewBase<T> : IView where T : class, IViewModel
    {
        /// <summary>
        /// 视图模型
        /// </summary>
        protected T _viewModel;

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
                    _viewModel.PropertyChanged -= ViewModelPropertyChanged;
                }

                if (value is T)
                {
                    _viewModel = value as T;

                    _viewModel.PropertyChanged += ViewModelPropertyChanged;

                    _viewModel.Setup();
                }
            }
        }

        /// <summary>
        /// 创建
        /// </summary>
        public abstract void Create(Transform parent, Action callback);

        /// <summary>
        /// 注销
        /// </summary>
        public void Dispose()
        {
            Release();

            if (_viewModel != null)
            {
                _viewModel.PropertyChanged -= ViewModelPropertyChanged;
            }

            if (_root != null)
            {
                GameObject.DestroyImmediate(_root.gameObject);
            }
        }

        /// <summary>
        /// 属性改变
        /// </summary>
        protected abstract void PropertyChanged(string propertyName);

        /// <summary>
        /// 销毁
        /// </summary>
        protected virtual void Release() { }

        /// <summary>
        /// 视图模型属性改变
        /// </summary>
        private void ViewModelPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (args == null)
            {
                return;
            }

            PropertyChanged(args.PropertyName);
        }
    }
}
