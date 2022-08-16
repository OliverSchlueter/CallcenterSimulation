using System.Diagnostics;
using Server.Services;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace Server;

public class ServerMain
{
    private static ServerMain? _instance;
    public static ServerMain? Instance { get => _instance; }

    public const string Version = "0.0.1-ALPHA";
    
    private WebSocketServer? _webSocketServer;
    public WebSocketServer? WebSocketServer { get => _webSocketServer; }
    private ServerMain()
    {
        Console.WriteLine("Starting Main Instance v" + Version);
        _instance = this;
    }

    public void Initialize()
    {
        _webSocketServer = new WebSocketServer("ws://127.0.0.01:1337");
        _webSocketServer.AddWebSocketService<IncomingCallService>("/call");
    }
    
    public void Start()
    {
        if(_webSocketServer == null) Initialize();
        
        Debug.Assert(_webSocketServer != null);
        _webSocketServer.Start();

        while(_webSocketServer.IsListening) { }
    }

    public void Stop(string reason, CloseStatusCode closeStatusCode = CloseStatusCode.Normal)
    {
        if(_webSocketServer != null)
            _webSocketServer.Stop(closeStatusCode, reason);
    }

    public static ServerMain CreateOrGetInstance()
    {
        return _instance != null ? _instance : new ServerMain();
    }
}