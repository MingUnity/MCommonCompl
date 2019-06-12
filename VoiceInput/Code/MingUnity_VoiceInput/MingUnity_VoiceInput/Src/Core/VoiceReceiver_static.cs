using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MingUnity.VoiceInput
{
    /// <summary>
    /// 声音接收器
    /// </summary>
    public partial class VoiceReceiver
    {
        /// <summary>
        /// 声音接收器列表
        /// </summary>
        private static Dictionary<int, VoiceReceiver> _voiceReceiverDic = new Dictionary<int, VoiceReceiver>();

        /// <summary>
        /// 初始化
        /// </summary>
        public static void Initialize(Action<bool> callback = null)
        {
            if (Application.HasUserAuthorization(UserAuthorization.Microphone))
            {
                callback?.Invoke(true);
            }
            else
            {
                TaskManager.CreateTask(AuthorizeRequest(callback));
            }
        }

        /// <summary>
        /// 麦克数目
        /// </summary>
        public static int Count
        {
            get
            {
                int res = 0;

                string[] devices = Microphone.devices;

                if (devices != null)
                {
                    res = devices.Length;
                }

                return res;
            }
        }

        /// <summary>
        /// 获取声音接收器
        /// </summary>
        public static VoiceReceiver Get(int index)
        {
            VoiceReceiver result = null;

            _voiceReceiverDic?.TryGetValue(index, out result);

            if (result == null)
            {
                result = new VoiceReceiver(index);
                
                if (_voiceReceiverDic != null)
                {
                    _voiceReceiverDic[index] = result;
                }
            }

            return result;
        }

        /// <summary>
        /// 获取声音接收器
        /// </summary>
        public static VoiceReceiver Get(string name)
        {
            VoiceReceiver result = null;

            for (int i = 0; i < Count; i++)
            {
                VoiceReceiver voiceReceiver = Get(i);

                if (voiceReceiver != null && voiceReceiver.Name == name)
                {
                    result = voiceReceiver;

                    break;
                }
            }

            return result;
        }

        /// <summary>
        /// 请求授权
        /// </summary>
        private static IEnumerator AuthorizeRequest(Action<bool> callback = null)
        {
            yield return Application.RequestUserAuthorization(UserAuthorization.Microphone);

            callback?.Invoke(Application.HasUserAuthorization(UserAuthorization.Microphone));
        }
    }
}
