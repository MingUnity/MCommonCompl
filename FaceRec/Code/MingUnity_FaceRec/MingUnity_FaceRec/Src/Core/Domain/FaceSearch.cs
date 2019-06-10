using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using UnityEngine;

namespace MingUnity.FaceRec
{
    /// <summary>
    /// 人脸搜索
    /// </summary>
    public class FaceSearch : FaceRecBase, IFaceSearch
    {
        private List<string> _groupIdList;

        public FaceSearch(string groupId, FaceAppData appData) : base(appData)
        {
            this._groupIdList = new List<string>() { groupId };
        }

        public FaceSearch(List<string> groupIdList, FaceAppData appData) : base(appData)
        {
            this._groupIdList = groupIdList;
        }

        /// <summary>
        /// 人脸搜索
        /// </summary>
        /// <param name="imagePath">本地图片地址</param>        
        /// <param name="groupIdList">从指定的group中进行查找</param>        
        public FaceSearchRes Search(string imagePath)
        {
            FaceSearchRes faceSearchRes = null;

            if (!string.IsNullOrEmpty(imagePath) && File.Exists(imagePath))
            {
                byte[] imageBytes = File.ReadAllBytes(imagePath);

                faceSearchRes = Search(imageBytes);
            }

            return faceSearchRes;
        }

        /// <summary>
        /// 人脸搜索
        /// </summary>
        /// <param name="texture">图片Texture</param>
        /// <param name="groupIdList">从指定的group中进行查找</param>        
        public FaceSearchRes Search(Texture2D texture)
        {
            FaceSearchRes faceSearchRes = null;

            if (texture != null)
            {
                byte[] imageBytes = texture.EncodeToJPG();

                faceSearchRes = Search(imageBytes);
            }

            return faceSearchRes;
        }

        /// <summary>
        /// 人脸搜索
        /// </summary>
        /// <param name="imageBytes">图片byte数组</param>
        /// <param name="groupIdList">从指定的group中进行查找</param>        
        public FaceSearchRes Search(byte[] imageBytes)
        {
            FaceSearchRes faceSearchRes = null;

            try
            {
                if (imageBytes != null && imageBytes.Length > 0 && _groupIdList != null && _groupIdList.Count > 0)
                {
                    string base64Image = Convert.ToBase64String(imageBytes);

                    string imageType = "BASE64";

                    StringBuilder grouplist = new StringBuilder(_groupIdList[0]);

                    for (int i = 1; i < _groupIdList.Count; i++)
                    {
                        grouplist.Append(string.Format(",{0}", _groupIdList[i]));
                    }

                    JObject result = _faceClient.Search(base64Image, imageType, grouplist.ToString());

                    faceSearchRes = result.ToObject<FaceSearchRes>();
                }
            }
            catch (Exception e)
            {
                Debug.LogErrorFormat("<Ming> ## Uni Error ## Cls:FaceSearch Func:Search info:{0}", e);
            }

            return faceSearchRes;
        }

        /// <summary>
        /// 异步人脸搜索
        /// </summary>
        public void AsyncSearch(string imagePath, Action<FaceSearchRes> callback)
        {
            ThreadPool.QueueUserWorkItem((state) =>
            {
                if (callback != null)
                {
                    callback.Invoke(Search(imagePath));
                }
            });
        }

        /// <summary>
        /// 异步人脸搜索
        /// </summary>
        public void AsyncSearch(byte[] imageBytes, Action<FaceSearchRes> callback)
        {
            ThreadPool.QueueUserWorkItem((state) =>
            {
                if (callback != null)
                {
                    callback.Invoke(Search(imageBytes));
                }
            });
        }
    }
}
