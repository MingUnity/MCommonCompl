namespace MingUnity.FaceRec
{
    /// <summary>
    /// 人脸识别结果
    /// </summary>
    public class FaceResBase
    {
        /// <summary>
        /// 错误码
        /// </summary>
        public int error_code;

        /// <summary>
        /// 错误信息
        /// </summary>
        public string error_msg;

        /// <summary>
        /// 日志Id
        /// </summary>
        public long log_id;

        /// <summary>
        /// 时间戳
        /// </summary>
        public long timestamp;

        /// <summary>
        /// 缓存
        /// </summary>
        public long cached;
    }
}
