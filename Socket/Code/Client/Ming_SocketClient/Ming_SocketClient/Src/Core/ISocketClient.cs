using System;
using System.Net;

namespace Ming.SocketClient
{
    /// <summary>
    /// Socket客户端接口
    /// </summary>
    public interface ISocketClient 
    {
        /// <summary>
        /// 连接完成事件
        /// </summary>
        event Action<SocketClientResult> OnConnectCompletedEvent;

        /// <summary>
        /// 断开连接完成事件
        /// </summary>
        event Action<SocketClientResult> OnDisconnectCompletedEvent;

        /// <summary>
        /// 接收数据完成事件
        /// </summary>
        event Action<byte[], SocketClientResult> OnReceiveCompletedEvent;

        /// <summary>
        /// 发送数据完成事件
        /// </summary>
        event Action<SocketClientResult> OnSendCompletedEvent;

        /// <summary>
        /// 连接标识
        /// </summary>
        bool IsConnected { get; }

        /// <summary>
        /// 连接
        /// </summary>
        void Connect();

        /// <summary>
        /// 断开连接
        /// </summary>
        void Disconnect();

        /// <summary>
        /// 发送数据
        /// </summary>
        void Send(byte[] data);
    }
}
