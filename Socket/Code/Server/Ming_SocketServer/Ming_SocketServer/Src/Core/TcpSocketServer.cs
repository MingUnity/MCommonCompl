using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace Ming.SocketServer
{
    /// <summary>
    /// Tcp socket 服务
    /// </summary>
    public class TcpSocketServer : ISocketServer
    {
        private Socket _socket;

        private IPEndPoint _ipEndPoint;

        private int _listenerCount;

        private bool _isRunning = false;

        private Dictionary<string, IClientSocket> _clientDic = new Dictionary<string, IClientSocket>();

        private IClientSocket this[string ip]
        {
            get
            {
                IClientSocket client = null;

                if (!string.IsNullOrEmpty(ip))
                {
                    _clientDic.TryGetValue(ip, out client);
                }

                return client;
            }
            set
            {
                lock (_clientDic)
                {
                    _clientDic[ip] = value;
                }
            }
        }

        public event Action<string, SocketServerResult> OnAcceptCompletedEvent;

        public event Action<string, SocketServerResult> OnSendCompletedEvent;

        public event Action<string, byte[], SocketServerResult> OnReceiveCompletedEvent;

        public event Action<SocketServerResult> OnCloseCompletedEvent;

        public event Action<string, SocketServerResult> OnDisconnectClientCompletedEvent;

        public bool IsRunning
        {
            get
            {
                return _isRunning;
            }
        }

        public int ClientCount
        {
            get
            {
                return _clientDic.Count;
            }
        }

        public string IP
        {
            get
            {
                return _ipEndPoint?.Address?.ToString();
            }
        }

        public TcpSocketServer(IPEndPoint ipEndPoint, int listener = 1024)
        {
            this._ipEndPoint = ipEndPoint;

            this._listenerCount = listener;
        }

        public TcpSocketServer(IPAddress ipAddress, int port, int listener = 1024)
        {
            this._ipEndPoint = new IPEndPoint(ipAddress, port);

            this._listenerCount = listener;
        }

        public TcpSocketServer(string ip, int port, int listener = 1024)
        {
            this._ipEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);

            this._listenerCount = listener;
        }

        public void Start()
        {
            if (!IsRunning)
            {
                try
                {
                    _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                    _socket.Bind(_ipEndPoint);

                    _socket.Listen(_listenerCount);

                    _isRunning = true;

                    Accept();
                }
                catch (Exception e)
                {
                    Close();

                    _isRunning = false;

                    throw e;
                }
            }
        }

        public void Send(string ip, byte[] dataBuffer)
        {
            if (!IsRunning)
            {
                OnSendCompletedEvent?.Invoke(ip, SocketServerResult.ServerNotRunning);

                return;
            }

            try
            {
                if (dataBuffer != null)
                {
                    this[ip].Socket.BeginSend(dataBuffer, 0, dataBuffer.Length, SocketFlags.None, OnSendCallback, ip);
                }
            }
            catch
            {
                OnSendCompletedEvent?.Invoke(ip, SocketServerResult.Exception);
            }
        }

        public void SendAll(byte[] dataBuffer)
        {
            foreach (string id in _clientDic.Keys)
            {
                Send(id, dataBuffer);
            }
        }

        public void Close()
        {
            if (!IsRunning)
            {
                OnCloseCompletedEvent?.Invoke(SocketServerResult.ServerNotRunning);

                return;
            }

            SocketServerResult sRes = SocketServerResult.None;

            DisconnectAllClient();

            try
            {
                _isRunning = false;

                _socket.Close();

                _socket = null;

                sRes = SocketServerResult.Success;
            }
            catch
            {
                sRes = SocketServerResult.Exception;
            }
            finally
            {
                OnCloseCompletedEvent?.Invoke(sRes);
            }
        }

        public void DisconnectClient(string ip)
        {
            if (!IsRunning)
            {
                OnDisconnectClientCompletedEvent?.Invoke(ip, SocketServerResult.ServerNotRunning);

                return;
            }

            SocketServerResult sRes = SocketServerResult.None;

            try
            {
                IClientSocket client = this[ip];

                client.Close();

                _clientDic.Remove(ip);

                sRes = SocketServerResult.Success;
            }
            catch
            {
                sRes = SocketServerResult.Exception;
            }
            finally
            {
                OnDisconnectClientCompletedEvent?.Invoke(ip, sRes);
            }
        }

        public void DisconnectAllClient()
        {
            foreach (string ip in _clientDic.Keys)
            {
                DisconnectClient(ip);
            }
        }

        private void Accept()
        {
            if (!IsRunning)
            {
                OnAcceptCompletedEvent?.Invoke(string.Empty, SocketServerResult.ServerNotRunning);

                return;
            }

            _socket.BeginAccept(OnAcceptCallback, _socket);
        }

        private void OnAcceptCallback(IAsyncResult result)
        {
            string ip = string.Empty;

            SocketServerResult sRes = SocketServerResult.None;

            try
            {
                Socket socket = result.AsyncState as Socket;

                Socket clientSocket = socket.EndAccept(result);

                ip = (clientSocket.RemoteEndPoint as IPEndPoint)?.Address?.ToString();

                if (!string.IsNullOrEmpty(ip))
                {
                    IClientSocket cSocket = new ClientSocket(clientSocket);

                    this[ip] = cSocket;

                    Receive(ip);

                    sRes = SocketServerResult.Success;
                }
                else
                {
                    throw new Exception("<Ming> ## Uni Excption ## Cls:TcpSocketServer Func:OnAcceptCallback Info:Accept ip is null");
                }
            }
            catch
            {
                sRes = SocketServerResult.Exception;
            }
            finally
            {
                OnAcceptCompletedEvent?.Invoke(ip, sRes);

                Accept();
            }
        }

        private void OnSendCallback(IAsyncResult result)
        {
            SocketServerResult sRes = SocketServerResult.None;

            string ip = string.Empty;

            try
            {
                ip = result.AsyncState as string;

                this[ip].Socket.EndSend(result);

                sRes = SocketServerResult.Success;
            }
            catch
            {
                sRes = SocketServerResult.Exception;
            }
            finally
            {
                OnSendCompletedEvent?.Invoke(ip, sRes);
            }
        }

        private void OnReceiveCallback(IAsyncResult result)
        {
            if (!IsRunning)
            {
                OnReceiveCompletedEvent?.Invoke(string.Empty, null, SocketServerResult.ServerNotRunning);

                return;
            }

            string ip = string.Empty;

            SocketServerResult sRes = SocketServerResult.None;

            byte[] data = null;

            int length = 0;

            try
            {
                ip = result.AsyncState as string;

                IClientSocket client = this[ip];

                length = client.Socket.EndReceive(result);

                if (length > 0)
                {
                    data = new byte[length];

                    Array.Copy(client.CacheBuffer, data, length);
                    
                    sRes = SocketServerResult.Success;
                }
                else
                {
                    DisconnectClient(ip);
                }
            }
            catch
            {
                sRes = SocketServerResult.Exception;
            }
            finally
            {
                if (length > 0)
                {
                    OnReceiveCompletedEvent?.Invoke(ip, data, sRes);

                    Receive(ip);
                }
            }
        }

        private void Receive(string ip)
        {
            IClientSocket client = this[ip];

            client.Socket.BeginReceive(client.CacheBuffer, 0, client.CacheBuffer.Length, SocketFlags.None, OnReceiveCallback, ip);
        }
    }
}
