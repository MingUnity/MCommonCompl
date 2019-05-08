using MingUnity.MVVM.ViewModel;
using System;
using UnityEngine;

namespace MingUnity.MVVM.View
{
    /// <summary>
    /// 视图接口
    /// </summary>
    public interface IView : IDisposable
    {
        /// <summary>
        /// 视图模型
        /// </summary>
        IViewModel ViewModel { get; set; }

        /// <summary>
        /// 激活
        /// </summary>
        bool Active { get; }

        /// <summary>
        /// 创建
        /// </summary>
        void Create(Transform parent = null, Action callback = null);
    }
}
