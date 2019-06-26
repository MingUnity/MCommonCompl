using System;
using System.Net;

namespace Ming.SocketServer
{
    /// <summary>
    /// socket服务接口
    /// </summary>
    public interface ISocketServer
    {
        /// <summary>
        /// 接收客户端连接完成事件
        /// </summary>
        event Action<string, SocketServerResult> OnAcceptCompletedEvent;

        /// <summary>
        /// 发送数据完成事件
        /// </summary>
        event Action<string, SocketServerResult> OnSendCompletedEvent;

        /// <summary>
        /// 接收客户端数据完成事件
        /// </summary>
        event Action<string, byte[], SocketServerResult> OnReceiveCompletedEvent;

        /// <summary>
        /// 关闭完成事件
        /// </summary>
        event Action<SocketServerResult> OnCloseCompletedEvent;

        /// <summary>
        /// 断开客户端连接完成事件
        /// </summary>
        event Action<string, SocketServerResult> OnDisconnectClientCompletedEvent;

        /// <summary>
        /// 运行标识
        /// </summary>
        bool IsRunning { get; }

        /// <summary>
        /// ip地址
        /// </summary>
        string IP { get; }

        /// <summary>
        /// 客户端数量
        /// </summary>
        int ClientCount { get; }

        /// <summary>
        /// 开始
        /// </summary>
        void Start();

        /// <summary>
        /// 发送数据
        /// </summary>
        void Send(string ip, byte[] dataBuffer);

        /// <summary>
        /// 给所有客户端发送数据
        /// </summary>
        void SendAll(byte[] dataBuffer);

        /// <summary>
        /// 关闭
        /// </summary>
        void Close();

        /// <summary>
        /// 断开客户端
        /// </summary>
        void DisconnectClient(string ip);

        /// <summary>
        /// 断开所有客户端
        /// </summary>
        void DisconnectAllClient();
    }
}
