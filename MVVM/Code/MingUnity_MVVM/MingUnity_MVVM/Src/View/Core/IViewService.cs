using MingUnity.MVVM.ViewModel;
using System;
using UnityEngine;

namespace MingUnity.MVVM.View
{
    /// <summary>
    /// 视图服务接口
    /// </summary>
    public interface IViewService
    {
        /// <summary>
        /// 创建视图
        /// </summary>
        void Create<T>(IViewModel viewModel, Transform parent = null, Action<string> callback = null) where T : class, IView;

        /// <summary>
        /// 切换视图
        /// </summary>
        void Switch(string guid, bool record = false);

        /// <summary>
        /// 切换视图
        /// </summary>
        void Switch(string guid, IViewModel viewModel, bool record = false);

        /// <summary>
        /// 回退视图
        /// </summary>
        void Backwards();

        /// <summary>
        /// 销毁视图
        /// </summary>
        void Destroy(string guid);
    }
}
