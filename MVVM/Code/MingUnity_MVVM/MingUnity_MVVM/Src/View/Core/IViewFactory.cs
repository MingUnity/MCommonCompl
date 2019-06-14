using MingUnity.MVVM.ViewModel;
using System;
using UnityEngine;

namespace MingUnity.MVVM.View
{
    /// <summary>
    /// 视图服务接口
    /// </summary>
    public interface IViewFactory
    {
        /// <summary>
        /// 创建视图
        /// </summary>
        void Create<T, V>(Transform parent = null, Action<string> callback = null) where T : class, IView where V : class, IViewModel;

        /// <summary>
        /// 创建视图
        /// </summary>
        void Create(IView view, IViewModel viewModel, Transform parent = null, Action<string> callback = null);
    }
}
