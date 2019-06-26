using System.Net.Sockets;

namespace Ming.SocketServer
{
    /// <summary>
    /// 客户端socket接口
    /// </summary>
    public interface IClientSocket
    {
        Socket Socket { get; }

        byte[] CacheBuffer { get; set; }

        void Close();
    }
}
