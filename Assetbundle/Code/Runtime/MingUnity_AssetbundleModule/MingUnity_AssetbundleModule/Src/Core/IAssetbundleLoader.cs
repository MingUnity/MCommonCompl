using System;

namespace MingUnity.AssetbundleModule
{
    /// <summary>
    /// AB包加载接口
    /// </summary>
    public interface IAssetbundleLoader
    {
        /// <summary>
        /// 预加载AB包
        /// </summary>
        void Preload(string abPath, Action<bool> callback = null);

        /// <summary>
        /// 获取资源
        /// </summary>
        T GetAsset<T>(string abPath, string assetName) where T : UnityEngine.Object;

        /// <summary>
        /// 异步获取资源
        /// </summary>
        void AsyncGetAsset<T>(string abPath, string assetName, Action<T> callback = null) where T : UnityEngine.Object;
    }
}
