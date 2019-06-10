using System.Collections.Generic;

namespace MingUnity.FaceRec
{
    /// <summary>
    /// 人脸搜索结果
    /// </summary>
    public class FaceSearchRes : FaceResBase
    {
        /// <summary>
        /// 结果
        /// </summary>
        public Result result;

        public class Result
        {
            public string face_token;

            /// <summary>
            /// 用户列表
            /// </summary>
            public List<User> user_list;

            public class User
            {
                /// <summary>
                /// 组Id
                /// </summary>
                public string group_id;

                /// <summary>
                /// 用户Id
                /// </summary>
                public string user_id;

                /// <summary>
                /// 用户信息
                /// </summary>
                public string user_info;

                /// <summary>
                /// 相似度
                /// </summary>
                public float score;
            }
        }
    }
}
