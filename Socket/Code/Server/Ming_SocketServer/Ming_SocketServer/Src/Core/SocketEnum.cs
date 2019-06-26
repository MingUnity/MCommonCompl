namespace Ming.SocketServer
{
    public enum SocketServerResult
    {
        /// <summary>
        /// 无
        /// </summary>
        None = -1,

        /// <summary>
        /// 成功
        /// </summary>
        Success = 0,

        /// <summary>
        /// 服务器未启动
        /// </summary>
        ServerNotRunning = 1,

        /// <summary>
        /// 异常
        /// </summary>
        Exception = 2
    }

}
