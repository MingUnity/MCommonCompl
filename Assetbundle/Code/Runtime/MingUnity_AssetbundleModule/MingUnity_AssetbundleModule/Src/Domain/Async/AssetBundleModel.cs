using System;
using UnityEngine;

namespace MingUnity.AssetbundleModule
{
    /// <summary>
    /// AB包数据模型
    /// </summary>
    internal sealed class AssetBundleModel
    {
        /// <summary>
        /// 加载完成动作
        /// </summary>
        public Action LoadedAction;

        /// <summary>
        /// AB包
        /// </summary>
        public AssetBundle assetBundle = null;

        /// <summary>
        /// 加载状态
        /// </summary>
        public LoadState loadState = LoadState.Unload;

        /// <summary>
        /// 加载状态枚举
        /// </summary>
        public enum LoadState
        {
            /// <summary>
            /// 未加载
            /// </summary>
            Unload = 0,

            /// <summary>
            /// 正在加载
            /// </summary>
            Loading,

            /// <summary>
            /// 加载完成
            /// </summary>
            Loaded
        }
    }
}
