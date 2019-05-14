using MingUnity.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace MingUnity.MVVM.View
{
    /// <summary>
    /// 视图工厂
    /// </summary>
    public class ViewFactory : IViewFactory
    {
        /// <summary>
        /// 视图字典
        /// </summary>
        private Dictionary<string, IView> _viewDic = new Dictionary<string, IView>();

        /// <summary>
        /// 视图索引
        /// </summary>
        private IView this[string guid]
        {
            get
            {
                IView result = null;

                if (!string.IsNullOrEmpty(guid))
                {
                    _viewDic.TryGetValue(guid, out result);
                }

                return result;
            }
        }

        /// <summary>
        /// 创建视图
        /// </summary>
        public void Create<T, V>(Transform parent = null, Action<string> callback = null) where T : class, IView where V : class, IViewModel
        {
            T view = Activator.CreateInstance<T>();

            V viewModel = Activator.CreateInstance<V>();

            Create(view, viewModel, parent, callback);
        }

        /// <summary>
        /// 创建视图
        /// </summary>
        public void Create(IView view, IViewModel viewModel, Transform parent = null, Action<string> callback = null)
        {
            if (view != null)
            {
                view.Create(parent, () =>
                {
                    string guid = Guid.NewGuid().ToString();

                    _viewDic[guid] = view;

                    view.ViewModel = viewModel;

                    callback?.Invoke(guid);
                });
            }
            else
            {
                callback?.Invoke(string.Empty);
            }
        }

        /// <summary>
        /// 获取视图模型
        /// </summary>
        public IViewModel GetViewModel(string guid)
        {
            return this[guid]?.ViewModel;
        }

        /// <summary>
        /// 销毁视图
        /// </summary>
        public void Destroy(string guid)
        {
            this[guid]?.Dispose();
        }
    }
}
