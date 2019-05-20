using System;
using UnityEngine;
using System.Collections;

namespace MingUnity.AssetBundles
{
    /// <summary>
    /// AB包任务
    /// </summary>
    internal class AssetBundleTask
    {
        /// <summary>
        /// 加载任务
        /// </summary>
        private Task _loadTask;

        /// <summary>
        /// AB包
        /// </summary>
        private AssetBundle _assetbundle;

        /// <summary>
        /// 回调缓存
        /// </summary>
        private Action<AssetBundle> _cacheCallback;

        /// <summary>
        /// 加载中标识
        /// </summary>
        private bool _loading = false;

        /// <summary>
        /// 加载
        /// </summary>
        public void Load(string abPath, bool async, Action<AssetBundle> callback)
        {
            _cacheCallback += callback;

            if (_assetbundle != null)
            {
                InvokeCache();
            }
            else
            {
                if (!string.IsNullOrEmpty(abPath))
                {
                    if (async) //异步
                    {
                        if (!_loading)
                        {
                            _loading = true;

                            _loadTask = Task.CreateTask(LoadABAsync(abPath, (ab) =>
                             {
                                 _loadTask = null;

                                 _loading = false;

                                 this._assetbundle = ab;

                                 InvokeCache();
                             }));
                        }
                    }
                    else  //同步
                    {
                        if (_loading)
                        {
                            _loadTask?.Stop();

                            _loadTask = null;

                            _loading = false;
                        }

                        _assetbundle = AssetBundle.LoadFromFile(abPath);

                        InvokeCache();
                    }
                }
            }
        }

        /// <summary>
        /// 卸载
        /// </summary>
        public void Unload(bool unloadAllLoadedObjects)
        {
            _assetbundle?.Unload(unloadAllLoadedObjects);
        }

        /// <summary>
        /// 异步加载AB包
        /// </summary>
        private IEnumerator LoadABAsync(string abPath, Action<AssetBundle> callback)
        {
            WWW www = new WWW(abPath);

            yield return www;

            if (www.isDone && string.IsNullOrEmpty(www.error))
            {
                callback?.Invoke(www.assetBundle);
            }
            else
            {
                callback?.Invoke(null);
            }
        }

        /// <summary>
        /// 执行缓存动作
        /// </summary>
        private void InvokeCache()
        {
            _cacheCallback?.Invoke(_assetbundle);

            _cacheCallback = null;
        }
    }
}
