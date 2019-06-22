using System;
using UnityEngine;

namespace MingUnity.MVVM
{
    /// <summary>
    /// 视图工厂
    /// </summary>
    public class ViewFactory : IViewFactory
    {
        /// <summary>
        /// 创建视图
        /// </summary>
        public void Create<T, V>(Transform parent = null, Action<IView, IViewModel> callback = null, bool active = false) where T : class, IView where V : class, IViewModel
        {
            T view = Activator.CreateInstance<T>();

            V viewModel = Activator.CreateInstance<V>();

            Create(view, viewModel, parent, () =>
            {
                callback?.Invoke(view, viewModel);
            }, active);
        }

        /// <summary>
        /// 创建视图
        /// </summary>
        public void Create(IView view, IViewModel viewModel, Transform parent = null, Action callback = null, bool active = false)
        {
            if (view != null)
            {
                view.Create(parent, active, () =>
                 {
                     view.ViewModel = viewModel;

                     callback?.Invoke();
                 });
            }
            else
            {
                callback?.Invoke();
            }
        }
    }
}
