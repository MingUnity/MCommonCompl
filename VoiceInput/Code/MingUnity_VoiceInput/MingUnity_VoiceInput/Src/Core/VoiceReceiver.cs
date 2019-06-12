using System;
using System.Collections;
using UnityEngine;

namespace MingUnity.VoiceInput
{
    /// <summary>
    /// 声音接收器
    /// </summary>
    public partial class VoiceReceiver
    {
        private int _index;

        private AudioClip _audioClip;

        private Task _recordTask;

        private bool _isDefault;

        private VoiceReceiver(int index)
        {
            this._index = index;

            if (index == -1)
            {
                _isDefault = true;
            }
        }

        /// <summary>
        /// 设备名
        /// </summary>
        public string Name
        {
            get
            {
                string res = string.Empty;

                if (_isDefault)
                {
                    res = null;
                }
                else
                {
                    string[] devices = Microphone.devices;

                    TryGetValue(devices, _index, ref res);
                }

                return res;
            }
        }

        /// <summary>
        /// 有效性
        /// </summary>
        public bool IsValid
        {
            get
            {
                bool valid = false;

                if (_isDefault)
                {
                    valid = Count > 0;
                }
                else
                {
                    string name = Name;

                    valid = !string.IsNullOrEmpty(name);
                }

                if (!valid)
                {
                    Debug.LogErrorFormat("<Ming> ## Uni Error ## Cls:VoiceReceiver Func:IsValid Info:invalid");
                }

                return valid;
            }
        }

        /// <summary>
        /// 开始录制
        /// </summary>
        public bool StartRecord(int maxTime = 60, int recordRate = 16000)
        {
            bool success = false;

            if (IsValid)
            {
                string name = Name;

                Microphone.End(name);

                _audioClip = Microphone.Start(name, false, maxTime, recordRate);
                
                while (!(Microphone.GetPosition(name) > 0)) { }
                
                success = true;
            }

            return success;
        }

        /// <summary>
        /// 停止录制
        /// </summary>
        public void StopRecord(Action<AudioClip> callback)
        {
            _recordTask?.Stop();

            _recordTask = null;

            if (IsValid)
            {
                string name = Name;

                if (Microphone.IsRecording(name))
                {
                    Microphone.End(name);
                }
            }

            callback?.Invoke(_audioClip);

            _audioClip = null;
        }

        /// <summary>
        /// 停止录制
        /// </summary>
        public void StopRecord(Action<byte[]> callback)
        {
            StopRecord((AudioClip clip) =>
            {
                callback?.Invoke(WavUtility.FromAudioClip(clip));
            });
        }

        /// <summary>
        /// 录制
        /// </summary>
        public void Record(int duration, Action<byte[]> callback, int recordRate = 16000)
        {
            Record(duration, (AudioClip clip) =>
            {
                callback?.Invoke(WavUtility.FromAudioClip(clip));
            }, recordRate);
        }

        /// <summary>
        /// 录制
        /// </summary>
        public void Record(int duration, Action<AudioClip> callback, int recordRate = 16000)
        {
            if (StartRecord(duration + 1, recordRate))
            {
                _recordTask = Task.CreateTask(CheckRecordEnd(duration, () =>
                {
                    _recordTask = null;

                    StopRecord(callback);
                }));
            }
        }

        /// <summary>
        /// 计时器
        /// </summary>
        private IEnumerator CheckRecordEnd(int time, Action callback)
        {
            yield return new WaitForSeconds(time);

            callback?.Invoke();
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
