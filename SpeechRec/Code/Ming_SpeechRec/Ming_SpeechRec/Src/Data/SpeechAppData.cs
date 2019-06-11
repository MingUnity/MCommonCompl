namespace Ming.SpeechRec
{
    /// <summary>
    /// 语音识别应用数据
    /// </summary>
    public struct SpeechAppData
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

        public SpeechAppData(string appId, string apiKey, string secretKey)
        {
            this.appId = appId;

            this.apiKey = apiKey;

            this.secretKey = secretKey;
        }
    }
}
