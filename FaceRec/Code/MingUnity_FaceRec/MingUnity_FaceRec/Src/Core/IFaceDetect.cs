using System;
using UnityEngine;

namespace MingUnity.FaceRec
{
    /// <summary>
    /// 人脸检测接口
    /// </summary>
    public interface IFaceDetect
    {
        /// <summary>
        /// 检测
        /// </summary>
        /// <param name="imagePath">图片本地路径</param>
        FaceDetectRes Detect(string imagePath);

        /// <summary>
        /// 检测
        /// </summary>
        /// <param name="texture">图片纹理</param>
        FaceDetectRes Detect(Texture2D texture);

        /// <summary>
        /// 检测
        /// </summary>
        /// <param name="imageBytes">图片源数据</param>
        FaceDetectRes Detect(byte[] imageBytes);

        /// <summary>
        /// 异步检测
        /// </summary>
        void AsyncDetect(string imagePath, Action<FaceDetectRes> callback);

        /// <summary>
        /// 异步检测
        /// </summary>
        void AsyncDetect(byte[] imageBytes, Action<FaceDetectRes> callback);

        /// <summary>
        /// 异步检测
        /// </summary>
        void AsyncDetect(Color32[] colors,int width,int height, Action<FaceDetectRes> callback);
    }
}
