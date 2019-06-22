using System;
using UnityEngine;

namespace MingUnity.MVVM
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
        void Create(Transform parent, bool active = false, Action callback = null);

        /// <summary>
        /// 显示
        /// </summary>
        void Show(Action callback = null);

        /// <summary>
        /// 隐藏
        /// </summary>
        void Hide(Action callback = null);
    }
}
