using System;
using UnityEngine;

namespace MingUnity.VoiceInput
{
    /// <summary>
    /// 声音接收器
    /// </summary>
    public partial class VoiceReceiver
    {
        private int _index;

        public VoiceReceiver(int index)
        {
            this._index = index;
        }

        public string Name
        {
            get
            {
                string res = string.Empty;

                string[] devices = Microphone.devices;

                TryGetValue(devices, _index, ref res);

                return res;
            }
        }

        /// <summary>
        /// 从数组中获取值
        /// </summary>
        private void TryGetValue<T>(T[] array, int index, ref T value)
        {
            if (IndexValid(array, index))
            {
                value = array[index];
            }
        }

        /// <summary>
        /// 数组取值有效性
        /// </summary>
        private bool IndexValid(Array array, int index)
        {
            bool result = false;

            if (array != null && index >= 0 && index < array.Length)
            {
                result = true;
            }

            return result;
        }
    }
}
