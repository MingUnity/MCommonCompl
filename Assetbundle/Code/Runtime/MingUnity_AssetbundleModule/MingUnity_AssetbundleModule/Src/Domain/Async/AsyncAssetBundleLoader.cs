using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace MingUnity.AssetbundleModule
{
    /// <summary>
    /// 异步AB包加载
    /// </summary>
    public class AsyncAssetBundleLoader : IAssetbundleLoader
    {
        private Dictionary<string, AssetBundleModel> _abDic = new Dictionary<string, AssetBundleModel>();

        private AssetBundleModel this[string abPath]
        {
            get
            {
                AssetBundleModel result = null;

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
            AssetBundleModel abModel = this[abPath];

            if (abModel == null)
            {
                abModel = new AssetBundleModel();

                this[abPath] = abModel;
            }

            if (abModel.loadState == AssetBundleModel.LoadState.Unload)
            {
                AsyncLoadAB(abPath, abModel);
            }
        }

        /// <summary>
        /// 异步加载资源
        /// </summary>
        public void GetAsset<T>(string abPath, string assetName, Action<T> callback = null) where T : Object
        {
            AssetBundleModel abModel = this[abPath];

            if (abModel == null)
            {
                abModel = new AssetBundleModel();

                this[abPath] = abModel;
            }

            Action getAssetAct = new Action(() =>
            {
                Task.CreateTask(AsyncGetAsset(abModel.assetBundle, assetName, (asset) =>
                {
                    callback?.Invoke(asset as T);
                }));
            });

            switch (abModel.loadState)
            {
                case AssetBundleModel.LoadState.Unload:
                    {
                        abModel.LoadedAction += getAssetAct;

                        AsyncLoadAB(abPath, abModel);
                    }

                    break;

                case AssetBundleModel.LoadState.Loading:
                    {
                        abModel.LoadedAction += getAssetAct;
                    }

                    break;

                case AssetBundleModel.LoadState.Loaded:
                    {
                        getAssetAct.Invoke();
                    }

                    break;
            }
        }

        /// <summary>
        /// 加载AB包
        /// </summary>
        private IEnumerator AsyncLoadAB(string abPath, Action<AssetBundle> callback = null)
        {
            AssetBundle result = null;

            if (!string.IsNullOrEmpty(abPath))
            {
                WWW www = new WWW(abPath);

                yield return www;

                if (www.isDone && string.IsNullOrEmpty(www.error))
                {
                    result = www.assetBundle;
                }
                else
                {
                    Debug.LogErrorFormat("<Ming> ## Uni Error ## Cls:AsyncAssetBundleLoader Func:AsyncLoadAB Info:Load fail {0}", www.error);
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

        /// <summary>
        /// 异步加载AB包 
        /// </summary>
        private void AsyncLoadAB(string abPath, AssetBundleModel abModel)
        {
            if (abModel != null)
            {
                abModel.loadState = AssetBundleModel.LoadState.Loading;

                Task.CreateTask(AsyncLoadAB(abPath, (assetbundle) =>
                {
                    if (assetbundle != null)
                    {
                        abModel.assetBundle = assetbundle;

                        abModel.loadState = AssetBundleModel.LoadState.Loaded;

                        abModel.LoadedAction?.Invoke();

                        abModel.LoadedAction = null;
                    }
                    else
                    {
                        abModel.loadState = AssetBundleModel.LoadState.Unload;
                    }
                }));
            }
        }

        /// <summary>
        /// 卸载
        /// </summary>
        public void Unload(string abPath, bool unloadAllLoadedObjects = false)
        {
            AssetBundleModel abModel = this[abPath];

            if (abModel != null)
            {
                _abDic?.Remove(abPath);

                try
                {
                    abModel.assetBundle?.Unload(unloadAllLoadedObjects);
                }
                catch (Exception e)
                {
                    Debug.LogErrorFormat("<Ming> ## Uni Error ## Cls:ASyncAssetBundleLoader Func:Unload AbPath:{0} Info:{1}", abPath, e);
                }
            }
            else
            {
                Debug.LogErrorFormat("<Ming> ## Uni Error ## Cls:ASyncAssetBundleLoader Func:Unload AbPath:{0} Info:ab not exists", abPath);
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
                    foreach (AssetBundleModel abModel in _abDic.Values)
                    {
                        abModel?.assetBundle?.Unload(unloadAllLoadedObjects);
                    }

                    _abDic.Clear();
                }
            }
            catch (Exception e)
            {
                Debug.LogErrorFormat("<Ming> ## Uni Error ## Cls:ASyncAssetBundleLoader Func:UnloadAll Info:{0}", e);
            }
        }
    }
}
