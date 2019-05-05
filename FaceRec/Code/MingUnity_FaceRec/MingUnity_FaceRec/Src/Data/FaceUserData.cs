namespace MingUnity.FaceRec
{
    /// <summary>
    /// 人脸识别用户数据
    /// </summary>
    public struct FaceUserData
    {
        /// <summary>
        /// 用户组
        /// </summary>
        public string groupId;

        /// <summary>
        /// 用户Id
        /// </summary>
        public string userId;

        /// <summary>
        /// 用户资料
        /// </summary>
        public string userInfo;

        public FaceUserData(string groupId, string userId, string userInfo)
        {
            this.groupId = groupId;

            this.userId = userId;

            this.userInfo = userInfo;
        }
    }
}
