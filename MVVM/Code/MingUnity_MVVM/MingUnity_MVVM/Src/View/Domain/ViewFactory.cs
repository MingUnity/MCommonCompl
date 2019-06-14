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
                    
                    view.ViewModel = viewModel;

                    callback?.Invoke(guid);
                });
            }
            else
            {
                callback?.Invoke(string.Empty);
            }
        }
    }
}
