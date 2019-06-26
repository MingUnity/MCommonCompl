using Ming.SocketServer;
using System;
using System.Text;
using System.Threading;

namespace ServerDemo
{
    public class Program
    {
        private static ISocketServer _server;

        private static void Main(string[] args)
        {
            _server = new TcpSocketServer("127.0.0.1", 6666);

            _server.OnAcceptCompletedEvent += Server_OnAcceptCompletedEvent;

            _server.OnCloseCompletedEvent += Server_OnCloseCompletedEvent;

            _server.OnDisconnectClientCompletedEvent += Server_OnDisconnectClientCompletedEvent;

            _server.OnReceiveCompletedEvent += Server_OnReceiveCompletedEvent;

            _server.OnSendCompletedEvent += Server_OnSendCompletedEvent;

            _server.Start();

            Console.WriteLine("Start server ...");

            while (true)
            {
                string val = Console.ReadLine();

                if (val == "exit")
                {
                    _server.Close();

                    break;
                }

                _server.SendAll(Encoding.UTF8.GetBytes(val));
            }
        }

        private static void Server_OnSendCompletedEvent(string ip, SocketServerResult res)
        {
            Console.WriteLine(string.Format("Send data done ! ip:{0} result:{1}", ip, res));
        }

        private static void Server_OnReceiveCompletedEvent(string ip, byte[] data, SocketServerResult res)
        {
            Console.WriteLine(string.Format("Receive data done ! ip:{0} result:{1} data:{2}", ip, res, Encoding.UTF8.GetString(data)));
        }

        private static void Server_OnDisconnectClientCompletedEvent(string ip, SocketServerResult res)
        {
            Console.WriteLine(string.Format("Disconnect client done ! ip:{0} result:{1}", ip, res));
        }

        private static void Server_OnCloseCompletedEvent(SocketServerResult res)
        {
            Console.WriteLine(string.Format("Close server done ! result:{0}", res));
        }

        private static void Server_OnAcceptCompletedEvent(string ip, SocketServerResult res)
        {
            Console.WriteLine(string.Format("Accept client done ! ip:{0} result:{1}", ip, res));
        }
    }
}
