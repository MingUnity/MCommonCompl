using System;

namespace MingUnity.AssetBundles
{
    /// <summary>
    /// AB包加载接口
    /// </summary>
    public interface IAssetBundlesLoader
    {
        /// <summary>
        /// 获取资源
        /// </summary>
        T GetAsset<T>(string abPath, string assetName) where T : UnityEngine.Object;

        /// <summary>
        /// 异步获取资源
        /// </summary>
        void GetAssetAsync<T>(string abPath, string assetName, Action<T> callback) where T : UnityEngine.Object;

        /// <summary>
        /// 卸载
        /// </summary>
        void Unload(string abPath, bool unloadAllLoadedObjects);

        /// <summary>
        /// 卸载全部
        /// </summary>
        void UnloadAll(bool unloadAllLoadedObjects);
    }
}
