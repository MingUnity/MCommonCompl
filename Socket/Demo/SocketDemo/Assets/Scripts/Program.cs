using UnityEngine;
using Ming.SocketClient;
using System.Text;

public class Program : MonoBehaviour
{
    private ISocketClient _client;

    private void Awake()
    {
        _client = new TcpSocketClient("127.0.0.1", 6666);

        _client.OnConnectCompletedEvent += Client_OnConnectCompletedEvent;

        _client.OnDisconnectCompletedEvent += Client_OnDisconnectCompletedEvent;

        _client.OnReceiveCompletedEvent += Client_OnReceiveCompletedEvent;

        _client.OnSendCompletedEvent += Client_OnSendCompletedEvent;

        _client.Connect();
    }

    private void Client_OnSendCompletedEvent(SocketClientResult res)
    {
        Debug.LogFormat("Send done ! Result:{0}", res);
    }

    private void Client_OnReceiveCompletedEvent(byte[] data, SocketClientResult res)
    {
        Debug.LogFormat("Receive done ! Data:{0} Result:{1}", Encoding.UTF8.GetString(data), res);
    }

    private void Client_OnDisconnectCompletedEvent(SocketClientResult res)
    {
        Debug.LogFormat("Disconnect done ! Result:{0}", res);
    }

    private void Client_OnConnectCompletedEvent(SocketClientResult res)
    {
        Debug.LogFormat("Connect done ! Result:{0}", res);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            _client.Send(Encoding.UTF8.GetBytes(string.Format("Hello i am the client")));
        }
    }

    private void OnApplicationQuit()
    {
        _client.Disconnect();
    }
}
