using System;
using System.IO;
using UnityEngine;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.Threading;

namespace MingUnity.FaceRec
{
    /// <summary>
    /// 人脸识别处理
    /// </summary>
    public class FaceDetect : FaceRecBase
    {
        public FaceDetect(FaceAppData appData) : base(appData)
        {

        }

        /// <summary>
        /// 人脸检测
        /// </summary>
        /// <param name="imagePath">本地图片地址</param>        
        public FaceDetectRes Detect(string imagePath)
        {
            FaceDetectRes faceDetectRes = null;

            if (!string.IsNullOrEmpty(imagePath) && File.Exists(imagePath))
            {
                byte[] imageBytes = File.ReadAllBytes(imagePath);

                faceDetectRes = Detect(imageBytes);
            }

            return faceDetectRes;
        }

        /// <summary>
        /// 人脸检测
        /// </summary>
        /// <param name="texture">图片Texture</param>
        public FaceDetectRes Detect(Texture2D texture)
        {
            FaceDetectRes faceDetectRes = null;

            if (texture != null)
            {
                byte[] imageBytes = texture.EncodeToJPG();

                faceDetectRes = Detect(imageBytes);
            }

            return faceDetectRes;
        }

        /// <summary>
        /// 人脸检测
        /// </summary>
        /// <param name="imageBytes">图片byte数组</param>
        public FaceDetectRes Detect(byte[] imageBytes)
        {
            FaceDetectRes faceDetectRes = null;

            try
            {
                if (imageBytes != null && imageBytes.Length > 0)
                {
                    string base64Image = Convert.ToBase64String(imageBytes);

                    string imageType = "BASE64";

                    Dictionary<string, object> options = new Dictionary<string, object>();

                    options.Add("face_field", "age,beauty,quality,landmark");

                    JObject result = _faceClient?.Detect(base64Image, imageType, options);

                    faceDetectRes = result.ToObject<FaceDetectRes>();
                }
            }
            catch (Exception e)
            {
                Debug.LogErrorFormat("<Ming> ## Uni Error ## Cls:FaceRec Func:Detect Info:{0}", e);
            }
            return faceDetectRes;
        }

        /// <summary>
        /// 异步人脸检测
        /// </summary>
        public void AsyncDetect(string imagePath, Action<FaceDetectRes> callback)
        {
            ThreadPool.QueueUserWorkItem((state) =>
            {
                if (callback != null)
                {
                    callback.Invoke(Detect(imagePath));
                }
            });
        }

        /// <summary>
        /// 异步人脸检测
        /// </summary>
        public void AsyncDetect(byte[] imageBytes, Action<FaceDetectRes> callback)
        {
            ThreadPool.QueueUserWorkItem((state) =>
            {
                if (callback != null)
                {
                    callback.Invoke(Detect(imageBytes));
                }
            });
        }
    }
}
