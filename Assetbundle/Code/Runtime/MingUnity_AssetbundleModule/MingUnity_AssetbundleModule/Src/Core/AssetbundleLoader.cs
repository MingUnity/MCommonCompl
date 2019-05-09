using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Object = UnityEngine.Object;

namespace MingUnity.AssetbundleModule
{
    /// <summary>
    /// 本地AB包加载
    /// </summary>
    public class LocalAssetbundleLoader : IAssetbundleLoader
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
        public void Preload(string abPath, Action<bool> callback = null)
        {
            Task.CreateTask(AsyncLoadAB(abPath, (ab) =>
            {
                if (ab != null)
                {
                    this[abPath] = ab;
                }

                callback?.Invoke(ab != null);
            }));
        }

        /// <summary>
        /// 加载资源
        /// </summary>
        public T GetAsset<T>(string abPath, string assetName) where T : Object
        {
            T result = null;

            AssetBundle assetbundle = this[abPath];

            if (assetbundle == null)
            {
                if (!string.IsNullOrEmpty(abPath) && File.Exists(abPath))
                {
                    assetbundle = AssetBundle.LoadFromFile(abPath);

                    this[abPath] = assetbundle;
                }
            }

            result = assetbundle?.LoadAsset<T>(assetName);

            return result;
        }

        /// <summary>
        /// 异步加载资源
        /// </summary>
        public void AsyncGetAsset<T>(string abPath, string assetName, Action<T> callback = null) where T : Object
        {
            AssetBundle assetbundle = this[abPath];

            if (assetbundle == null)
            {
                TaskManager.CreateTask(AsyncLoadAB(abPath, (ab) =>
                {
                    this[abPath] = ab;

                    TaskManager.CreateTask(AsyncGetAsset(ab, assetName, (asset) =>
                     {
                         callback?.Invoke(asset as T);
                     }));
                }));
            }
            else
            {
                TaskManager.CreateTask(AsyncGetAsset(assetbundle, assetName, (asset) =>
                {
                    callback?.Invoke(asset as T);
                }));
            }
        }

        /// <summary>
        /// 加载AB包
        /// </summary>
        private IEnumerator AsyncLoadAB(string abPath, Action<AssetBundle> callback = null)
        {
            AssetBundle result = null;

            if (!string.IsNullOrEmpty(abPath) && File.Exists(abPath))
            {
                WWW www = new WWW(abPath);

                yield return www;

                if (www.isDone && !string.IsNullOrEmpty(www.error))
                {
                    result = www.assetBundle;
                }
            }

            callback?.Invoke(result);
        }

        /// <summary>
        /// 异步获取资源
        /// </summary>
        private IEnumerator AsyncGetAsset(AssetBundle assetBundle, string assetName, Action<Object> callback)
        {
            if (assetBundle != null)
            {
                AssetBundleRequest request = assetBundle.LoadAssetAsync(assetName);

                yield return request;

                if (request.isDone)
                {
                    callback?.Invoke(request.asset);
                }
                else
                {
                    callback?.Invoke(null);
                }
            }
            else
            {
                callback?.Invoke(null);
            }
        }
    }
}
