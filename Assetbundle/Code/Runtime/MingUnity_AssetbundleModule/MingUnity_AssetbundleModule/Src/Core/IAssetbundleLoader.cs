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
        void Preload(string abPath);

        /// <summary>
        /// 获取资源
        /// </summary>
        void GetAsset<T>(string abPath, string assetName, Action<T> callback = null) where T : UnityEngine.Object;

        /// <summary>
        /// 卸载AB包
        /// </summary>
        void Unload(string abPath, bool unloadAllLoadedObjects = false);

        /// <summary>
        /// 卸载所有AB包
        /// </summary>
        void UnloadAll(bool unloadAllLoadedObjects = false);
    }
}
