using System;
using UnityEngine;

namespace MingUnity.FaceRec
{
    public interface IFaceSearch
    {
        /// <summary>
        /// 搜索
        /// </summary>
        /// <param name="imagePath">图片本地路径</param>
        FaceSearchRes Search(string imagePath);

        /// <summary>
        /// 检测
        /// </summary>
        /// <param name="texture">图片纹理</param>
        FaceSearchRes Search(Texture2D texture);

        /// <summary>
        /// 检测
        /// </summary>
        /// <param name="imageBytes">图片源数据</param>
        FaceSearchRes Search(byte[] imageBytes);

        /// <summary>
        /// 异步检测
        /// </summary>
        void AsyncSearch(string imagePath, Action<FaceSearchRes> callback);

        /// <summary>
        /// 异步检测
        /// </summary>
        void AsyncSearch(byte[] imageBytes, Action<FaceSearchRes> callback);
    }
}
