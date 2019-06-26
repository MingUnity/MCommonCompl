namespace Ming.SocketClient
{
    /// <summary>
    /// Socket客户端返回结果
    /// </summary>
    public enum SocketClientResult
    {
        /// <summary>
        /// 无
        /// </summary>
        None = 0,

        /// <summary>
        /// 成功
        /// </summary>
        Success = 1,

        /// <summary>
        /// 未连接
        /// </summary>
        NotConnect = 2,

        /// <summary>
        /// 异常
        /// </summary>
        Exception = 3
    }
}
