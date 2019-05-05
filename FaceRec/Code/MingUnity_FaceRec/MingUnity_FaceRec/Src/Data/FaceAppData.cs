namespace MingUnity.FaceRec
{
    /// <summary>
    /// 人脸识别应用数据
    /// </summary>
    public struct FaceAppData
    {
        /// <summary>
        /// app id
        /// </summary>
        public string appId;

        /// <summary>
        /// api key 
        /// </summary>
        public string apiKey;

        /// <summary>
        /// secret key
        /// </summary>
        public string secretKey;

        public FaceAppData(string appId, string apiKey, string secretKey)
        {
            this.appId = appId;

            this.apiKey = apiKey;

            this.secretKey = secretKey;
        }
    }
}
