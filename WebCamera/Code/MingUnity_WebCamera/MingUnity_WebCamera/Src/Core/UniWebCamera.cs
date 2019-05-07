using System;
using UnityEngine;

namespace MingUnity.WebCamera
{
    /// <summary>
    /// 网络摄像头
    /// </summary>
    public partial class UniWebCamera
    {
        private int _camIndex;

        private WebCamTexture _camTex;

        private Texture2D _cacheTex;

        /// <summary>
        /// 索引
        /// </summary>
        public int Index
        {
            get
            {
                return _camIndex;
            }
        }

        /// <summary>
        /// 有效性
        /// </summary>
        public bool IsValid
        {
            get
            {
                bool res = false;

                WebCamDevice[] devices = WebCamTexture.devices;

                if (IndexValid(devices, _camIndex))
                {
                    res = true;
                }

                return res;
            }
        }

        /// <summary>
        /// 名字
        /// </summary>
        public string Name
        {
            get
            {
                return GetDevice().name;
            }
        }

        /// <summary>
        /// 前置摄像头标识
        /// </summary>
        public bool IsFrontFacing
        {
            get
            {
                return GetDevice().isFrontFacing;
            }
        }

        /// <summary>
        /// 摄像头纹理
        /// </summary>
        public Texture CamTexture
        {
            get
            {
                return _camTex;
            }
        }

        /// <summary>
        /// 宽
        /// </summary>
        public int Width
        {
            get
            {
                int res = 0;

                if (_camTex != null)
                {
                    res = _camTex.width;
                }

                return res;
            }
        }

        /// <summary>
        /// 高
        /// </summary>
        public int Height
        {
            get
            {
                int res = 0;

                if (_camTex != null)
                {
                    res = _camTex.height;
                }

                return res;
            }
        }

        /// <summary>
        /// FPS
        /// </summary>
        public float FPS
        {
            get
            {
                float res = 0;

                if (_camTex != null)
                {
                    res = _camTex.requestedFPS;
                }

                return res;
            }
        }

        /// <summary>
        /// 摄像头开启标识
        /// </summary>
        public bool IsPlaying
        {
            get
            {
                bool res = false;

                if (_camTex != null)
                {
                    res = _camTex.isPlaying;
                }

                return res;
            }
        }

        /// <summary>
        /// 旋转角
        /// </summary>
        public int RotationAngle
        {
            get
            {
                int res = 0;

                if (_camTex != null)
                {
                    res = _camTex.videoRotationAngle;
                }

                return res;
            }
        }

        /// <summary>
        /// 垂直镜像
        /// </summary>
        public bool VerticalMirror
        {
            get
            {
                bool res = false;

                if (_camTex != null)
                {
                    res = _camTex.videoVerticallyMirrored;
                }

                return res;
            }
        }

        /// <summary>
        /// 打开
        /// </summary>
        public void Open(int width, int height, bool focus = false, float fps = 30)
        {
            if (!IsValid)
            {
                return;
            }

            try
            {
                if (_camTex != null)
                {
                    if (!_camTex.isPlaying)
                    {
                        _camTex.requestedWidth = width;

                        _camTex.requestedHeight = height;

                        _camTex.requestedFPS = fps;

                        _camTex.deviceName = Name;

                        _camTex.Play();
                    }
                    else
                    {
                        if (focus)
                        {
                            //如果分辨率或fps有修改 强制重启摄像头
                            if (_camTex.requestedWidth != width || _camTex.requestedHeight != height || _camTex.requestedFPS != fps)
                            {
                                ReConnect(width, height, fps);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogErrorFormat("<Ming> <Ming> ## Uni Error ## Cls:UniWebCamera Func:Open CamIndex:{0} Info:{1}", _camIndex, e);
            }

        }

        /// <summary>
        /// 关闭
        /// </summary>
        public void Close()
        {
            try
            {
                if (_camTex != null)
                {
                    if (_camTex.isPlaying)
                    {
                        _camTex.Stop();
                    }
                    else
                    {
                        Debug.LogWarningFormat("<Ming> <Ming> ## Uni Warning ## Cls:UniWebCamera Func:Close CamIndex:{0} Info:Has closed", _camIndex);
                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogErrorFormat("<Ming> <Ming> ## Uni Error ## Cls:UniWebCamera Func:Close CamIndex:{0} Info:{1}", _camIndex, e);
            }
        }

        /// <summary>
        /// 重连
        /// </summary>
        public void ReConnect(int width, int height, float fps = 30)
        {
            if (!IsValid)
            {
                return;
            }

            try
            {
                if (_camTex != null)
                {
                    if (_camTex.isPlaying)
                    {
                        _camTex.Stop();
                    }

                    _camTex.requestedWidth = width;

                    _camTex.requestedHeight = height;

                    _camTex.requestedFPS = fps;

                    _camTex.deviceName = Name;

                    _camTex.Play();
                }
            }
            catch (Exception e)
            {
                Debug.LogErrorFormat("<Ming> <Ming> ## Uni Error ## Cls:UniWebCamera Func:ReConnect CamIndex:{0} Info:{1}", _camIndex, e);
            }
        }

        /// <summary>
        /// 摄像头快照
        /// </summary>
        public void Snapshot(Action<Texture2D> callback)
        {
            try
            {
                if (_camTex != null)
                {
                    if (_camTex.isPlaying)
                    {
                        Color[] colors = _camTex.GetPixels();

                        int width = _camTex.width;

                        int height = _camTex.height;

                        if (_cacheTex == null)
                        {
                            _cacheTex = new Texture2D(width, height);
                        }
                        else if (_cacheTex.width != width || _cacheTex.height != height)
                        {
                            _cacheTex.Resize(width, height);
                        }

                        _cacheTex.SetPixels(colors);

                        _cacheTex.Apply();
                    }
                    else
                    {
                        Debug.LogErrorFormat("<Ming> <Ming> ## Uni Error ## Cls:UniWebCamera Func:Snapshot CamIndex:{0} Info:Camera not opened", _camIndex);
                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogErrorFormat("<Ming> <Ming> ## Uni Error ## Cls:UniWebCamera Func:Snapshot CamIndex:{0} Info:{1}", _camIndex, e);
            }

            callback?.Invoke(_cacheTex);
        }

        /// <summary>
        /// 摄像头快照
        /// </summary>
        public void Snapshot(Action<Color[]> callback)
        {
            Color[] result = null;

            try
            {
                if (_camTex != null)
                {
                    if (_camTex.isPlaying)
                    {
                        result = _camTex.GetPixels();
                    }
                    else
                    {
                        Debug.LogErrorFormat("<Ming> <Ming> ## Uni Error ## Cls:UniWebCamera Func:Snapshot CamIndex:{0} Info:Camera not opened", _camIndex);
                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogErrorFormat("<Ming> <Ming> ## Uni Error ## Cls:UniWebCamera Func:Snapshot CamIndex:{0} Info:{1}", _camIndex, e);
            }

            callback?.Invoke(result);
        }

        /// <summary>
        /// 摄像头快照
        /// </summary>
        public void Snapshot(Action<Color32[]> callback)
        {
            Color32[] result = null;

            try
            {
                if (_camTex != null)
                {
                    if (_camTex.isPlaying)
                    {
                        result = _camTex.GetPixels32();
                    }
                    else
                    {
                        Debug.LogErrorFormat("<Ming> ## Uni Error ## Cls:UniWebCamera Func:Snapshot CamIndex:{0} Info:Camera not open", _camIndex);
                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogErrorFormat("<Ming> ## Uni Error ## Cls:UniWebCamera Func:Snapshot CamIndex:{0} Info:{1}", _camIndex, e);
            }

            callback?.Invoke(result);
        }

        public override string ToString()
        {
            return string.Format("CamIndex:{0} Name:{1} IsFrontFacing:{2} Width:{3} Height:{4} FPS:{5} IsPlaying:{6}", _camIndex, Name, IsFrontFacing, Width, Height, FPS, IsPlaying);
        }

        private UniWebCamera(int index)
        {
            this._camIndex = index;

            this._camTex = new WebCamTexture();
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

        /// <summary>
        /// 获取设备
        /// </summary>
        private WebCamDevice GetDevice()
        {
            WebCamDevice result = default(WebCamDevice);

            WebCamDevice[] devices = WebCamTexture.devices;

            TryGetValue(devices, _camIndex, ref result);

            return result;
        }
    }
}
