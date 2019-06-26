using System.Net.Sockets;

namespace Ming.SocketServer
{
    public class ClientSocket : IClientSocket
    {
        private Socket _socket;

        private byte[] _cacheBuffer;

        public byte[] CacheBuffer
        {
            get
            {
                return _cacheBuffer;
            }
            set
            {
                _cacheBuffer = value;
            }
        }

        public Socket Socket
        {
            get
            {
                return _socket;
            }
        }

        public ClientSocket(Socket socket)
        {
            this._socket = socket;

            _cacheBuffer = new byte[_socket.ReceiveBufferSize];
        }
        
        public void Close()
        {
            if (_socket != null && _socket.Connected)
            {
                _socket.Shutdown(SocketShutdown.Both);

                _socket.Close();
            }
        }
    }
}
