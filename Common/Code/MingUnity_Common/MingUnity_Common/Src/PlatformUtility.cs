using UnityEngine;

namespace MingUnity.Common
{
    /// <summary>
    /// 平台通用
    /// </summary>
    public static class PlatformUtility
    {
        /// <summary>
        /// 获取加载资源时streamingAssets路径
        /// </summary>
        public static string GetResStreamingAssets(bool syncLoad = true)
        {
            string dir = string.Empty;

            switch (Application.platform)
            {
                case RuntimePlatform.Android:
                    {
                        dir = syncLoad ? "{0}!assets" : "jar:file://{0}!/assets";

                        dir = string.Format(dir, Application.dataPath);
                    }

                    break;

                case RuntimePlatform.IPhonePlayer:
                    {
                        dir = syncLoad ? "{0}/Raw" : "file://{0}/Raw";

                        dir = string.Format(dir, Application.dataPath);
                    }

                    break;

                default:
                    dir = Application.streamingAssetsPath;
                    break;
            }

            return dir;
        }
    }
}
