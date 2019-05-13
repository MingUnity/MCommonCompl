using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace MingUnity.AssetbundleModule
{
    /// <summary>
    /// 同步ab包加载
    /// </summary>
    public sealed class SyncAssetBundleLoader : IAssetBundleLoader
    {
        private Dictionary<string, AssetBundle> _abDic = new Dictionary<string, AssetBundle>();

        private AssetBundle this[string abPath]
        {
            get
            {
                AssetBundle result = null;

                if (!string.IsNullOrEmpty(abPath))
                {
                    _abDic?.TryGetValue(abPath, out result);
                }

                return result;
            }
            set
            {
                if (_abDic != null)
                {
                    _abDic[abPath] = value;
                }
            }
        }

        /// <summary>
        /// 预加载
        /// </summary>
        public void Preload(string abPath)
        {
            if (!string.IsNullOrEmpty(abPath) && !_abDic.ContainsKey(abPath))
            {
                AssetBundle assetbundle = LoadAb(abPath);

                this[abPath] = assetbundle;
            }
            else
            {
                Debug.LogErrorFormat("<Ming> ## Uni Error ## Cls:SyncAssetBundleLoader Func:Preload Abpath:{0} Info:Preload assetbundle fail", abPath);
            }
        }

        /// <summary>
        /// 获取资源
        /// </summary>
        public void GetAsset<T>(string abPath, string assetName, Action<T> callback = null) where T : UnityEngine.Object
        {
            T result = null;

            AssetBundle assetbundle = this[abPath];

            if (assetbundle == null)
            {
                assetbundle = LoadAb(abPath);

                this[abPath] = assetbundle;
            }

            result = assetbundle?.LoadAsset<T>(assetName);

            callback?.Invoke(result);
        }

        /// <summary>
        /// 加载AB包
        /// </summary>
        private AssetBundle LoadAb(string abPath)
        {
            AssetBundle result = null;

            try
            {
                if (!string.IsNullOrEmpty(abPath) && File.Exists(abPath))
                {
                    result = AssetBundle.LoadFromFile(abPath);
                }
            }
            catch (Exception e)
            {
                Debug.LogErrorFormat("<Ming> ## Uni Error ## Cls:SyncAssetBundleLoader Func:LoadAb Info:{0} ", e);
            }

            return result;
        }

        /// <summary>
        /// 卸载
        /// </summary>
        public void Unload(string abPath, bool unloadAllLoadedObjects = false)
        {
            AssetBundle assetbundle = this[abPath];

            if (assetbundle != null)
            {
                _abDic?.Remove(abPath);
            }
            else
            {
                Debug.LogErrorFormat("<Ming> ## Uni Error ## Cls:SyncAssetBundleLoader Func:Unload AbPath:{0} Info:ab not exists", abPath);
            }

            try
            {
                assetbundle?.Unload(unloadAllLoadedObjects);
            }
            catch (Exception e)
            {
                Debug.LogErrorFormat("<Ming> ## Uni Error ## Cls:SyncAssetBundleLoader Func:Unload AbPath:{0} Info:{1}", abPath, e);
            }
        }

        /// <summary>
        /// 卸载全部
        /// </summary>
        public void UnloadAll(bool unloadAllLoadedObjects = false)
        {
            try
            {
                if (_abDic != null)
                {
                    foreach (AssetBundle assetbundle in _abDic.Values)
                    {
                        assetbundle?.Unload(unloadAllLoadedObjects);
                    }

                    _abDic.Clear();
                }
            }
            catch (Exception e)
            {
                Debug.LogErrorFormat("<Ming> ## Uni Error ## Cls:SyncAssetBundleLoader Func:UnloadAll Info:{0}", e);
            }

        }
    }
}
