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
        void Create<T>(IViewModel viewModel, Transform parent = null, Action<string> callback = null) where T : class, IView;

        /// <summary>
        /// 创建视图
        /// </summary>
        void Create(IView view, IViewModel viewModel, Transform parent = null, Action<string> callback = null);

        /// <summary>
        /// 获取视图模型
        /// </summary>
        IViewModel GetViewModel(string guid);

        /// <summary>
        /// 销毁视图
        /// </summary>
        void Destroy(string guid);
    }
}
