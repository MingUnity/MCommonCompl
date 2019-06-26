using System;
using System.Net;
using System.Net.Sockets;

namespace Ming.SocketClient
{
    public class TcpSocketClient : ISocketClient
    {
        private Socket _socket;

        private IPEndPoint _ipEndPoint;

        public event Action<SocketClientResult> OnConnectCompletedEvent;

        public event Action<SocketClientResult> OnDisconnectCompletedEvent;

        public event Action<byte[], SocketClientResult> OnReceiveCompletedEvent;

        public event Action<SocketClientResult> OnSendCompletedEvent;

        private byte[] _receiveBuffer = new byte[1024];

        public TcpSocketClient(IPEndPoint ipEndPoint)
        {
            this._ipEndPoint = ipEndPoint;
        }

        public TcpSocketClient(IPAddress ipAddress, int port)
        {
            this._ipEndPoint = new IPEndPoint(ipAddress, port);
        }

        public TcpSocketClient(string ip, int port)
        {
            this._ipEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);
        }

        public bool IsConnected
        {
            get
            {
                return _socket != null && _socket.Connected;
            }
        }

        public void Connect()
        {
            try
            {
                _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                _socket.BeginConnect(_ipEndPoint, OnConnectCallback, _socket);
            }
            catch
            {
                _socket?.Close();

                _socket = null;

                OnConnectCompletedEvent?.Invoke(SocketClientResult.Exception);
            }
        }

        public void Disconnect()
        {
            SocketClientResult cRes = SocketClientResult.None;

            try
            {
                if (IsConnected)
                {
                    _socket.Shutdown(SocketShutdown.Both);

                    _socket.Close();

                    _socket = null;

                    cRes = SocketClientResult.Success;
                }
                else
                {
                    cRes = SocketClientResult.NotConnect;
                }
            }
            catch
            {
                cRes = SocketClientResult.Exception;
            }
            finally
            {
                OnDisconnectCompletedEvent?.Invoke(cRes);
            }
        }

        public void Send(byte[] data)
        {
            try
            {
                if (IsConnected)
                {
                    _socket.BeginSend(data, 0, data.Length, SocketFlags.None, OnSendCallback, _socket);
                }
                else
                {
                    OnSendCompletedEvent?.Invoke(SocketClientResult.NotConnect);
                }
            }
            catch
            {
                OnSendCompletedEvent?.Invoke(SocketClientResult.Exception);
            }
        }

        private void OnConnectCallback(IAsyncResult result)
        {
            SocketClientResult cRes = SocketClientResult.None;

            try
            {
                if (IsConnected)
                {
                    Socket socket = result.AsyncState as Socket;

                    socket.EndConnect(result);

                    Receive();

                    cRes = SocketClientResult.Success;
                }
                else
                {
                    cRes = SocketClientResult.NotConnect;
                }
            }
            catch
            {
                cRes = SocketClientResult.Exception;

                Disconnect();
            }
            finally
            {
                OnConnectCompletedEvent?.Invoke(cRes);
            }
        }

        private void Receive()
        {
            if (IsConnected)
            {
                _socket.BeginReceive(_receiveBuffer, 0, _receiveBuffer.Length, SocketFlags.None, OnReceiveCallback, _socket);
            }
            else
            {
                OnReceiveCompletedEvent?.Invoke(null, SocketClientResult.NotConnect);
            }
        }

        private void OnReceiveCallback(IAsyncResult result)
        {
            SocketClientResult cRes = SocketClientResult.None;

            byte[] buffer = null;

            int length = 0;

            try
            {
                if (IsConnected)
                {
                    Socket socket = result.AsyncState as Socket;

                    length = socket.EndReceive(result);

                    buffer = new byte[length];

                    Array.Copy(_receiveBuffer, buffer, length);

                    cRes = SocketClientResult.Success;
                }
                else
                {
                    cRes = SocketClientResult.NotConnect;
                }
            }
            catch
            {
                cRes = SocketClientResult.Exception;
            }
            finally
            {
                if (length > 0)
                {
                    OnReceiveCompletedEvent?.Invoke(buffer, cRes);

                    Receive();
                }
                else
                {
                    Disconnect();
                }
            }
        }

        private void OnSendCallback(IAsyncResult result)
        {
            SocketClientResult cRes = SocketClientResult.None;

            try
            {
                if (IsConnected)
                {
                    Socket socket = result.AsyncState as Socket;

                    socket.EndSend(result);

                    cRes = SocketClientResult.Success;
                }
                else
                {
                    cRes = SocketClientResult.NotConnect;
                }
            }
            catch
            {
                cRes = SocketClientResult.Exception;
            }
            finally
            {
                OnSendCompletedEvent?.Invoke(cRes);
            }
        }
    }
}
