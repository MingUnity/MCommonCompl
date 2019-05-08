using System;
using System.IO;
using UnityEngine;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.Threading;
using System.Drawing;
using System.Drawing.Imaging;

namespace MingUnity.FaceRec
{
    /// <summary>
    /// 人脸识别处理
    /// </summary>
    public sealed class FaceDetect : FaceRecBase, IFaceDetect
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
        /// <param name="texture">图片纹理</param>
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
        /// <param name="colors">像素颜色数组</param>
        /// <param name="width">图片宽</param>
        /// <param name="height">图片高</param>
        public FaceDetectRes Detect(Color32[] colors, int width, int height)
        {
            return Detect(Color32ArrayToImageBuffer(colors, width, height));
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
                callback?.Invoke(Detect(imagePath));
            });
        }

        /// <summary>
        /// 异步人脸检测
        /// </summary>
        public void AsyncDetect(byte[] imageBytes, Action<FaceDetectRes> callback)
        {
            ThreadPool.QueueUserWorkItem((state) =>
            {
                callback?.Invoke(Detect(imageBytes));
            });
        }

        /// <summary>
        /// 异步人脸检测
        /// </summary>
        public void AsyncDetect(Color32[] colors, int width, int height, Action<FaceDetectRes> callback)
        {
            ThreadPool.QueueUserWorkItem((state) =>
            {
                callback?.Invoke(Detect(Color32ArrayToImageBuffer(colors, width, height)));
            });
        }

        /// <summary>
        /// 像素颜色数组转图片二进制数据
        /// </summary>
        private byte[] Color32ArrayToImageBuffer(Color32[] colors, int width, int height)
        {
            byte[] result = null;

            try
            {
                if (colors != null && colors.Length == width * height)
                {
                    using (Bitmap bitmap = new Bitmap(width, height))
                    {
                        for (int x = 0; x < width; x++)
                        {
                            for (int y = 0; y < height; y++)
                            {
                                Color32 pixelClr = colors[x + width * y];

                                bitmap.SetPixel(x, height - 1 - y, System.Drawing.Color.FromArgb(pixelClr.a, pixelClr.r, pixelClr.g, pixelClr.b));
                            }
                        }

                        using (MemoryStream stream = new MemoryStream())
                        {
                            bitmap.Save(stream, ImageFormat.Jpeg);

                            stream.Seek(0, SeekOrigin.Begin);

                            result = stream.GetBuffer();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                result = null;

                Debug.LogErrorFormat("<Ming> ## Uni Error ## Cls:FaceDetect Func:Color32ArrayToImageBuffer Info:{0}", e);
            }

            return result;
        }
    }
}
