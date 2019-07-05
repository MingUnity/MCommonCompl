using System;
using UnityEngine;

namespace MingUnity.ElementCore
{
    /// <summary>
    /// 元素对象接口
    /// </summary>
    public interface IElement
    {
        /// <summary>
        /// 元素对象模型
        /// </summary>
        IElementModel ElementModel { get; set; }

        /// <summary>
        /// 创建
        /// </summary>
        void Create(Transform parent = null, Action callback = null);
    }
}
