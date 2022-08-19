using System.Diagnostics;
using Server.Clients;
using Server.Services;
using Server.Utils;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace Server;

public class ServerMain
{
    private static ServerMain _instance;
    public static ServerMain Instance { get => _instance; }

    public const string Version = "0.0.1-ALPHA";

    private readonly Utils.Logger _logger;
    public Utils.Logger Logger { get => _logger; }
    
    private WebSocketServer? _webSocketServer;
    public WebSocketServer? WebSocketServer { get => _webSocketServer; }

    private Cache<string, Client> _clientCache;
    public Cache<string, Client> ClientCache { get => _clientCache; }
    
    private Cache<string, Client> _unknownClientCache;
    public Cache<string, Client> UnknownClientCache { get => _unknownClientCache; }
    
    private ServerMain()
    {
        _instance = this;
        _logger = new Utils.Logger(true);
        _clientCache = new Cache<string, Client>(false);
        _unknownClientCache = new Cache<string, Client>(false);
        new Thread(CLIThread).Start();
    }

    public void Start()
    {
        _webSocketServer = new WebSocketServer("ws://127.0.0.01:1337");
        _webSocketServer.AddWebSocketService<IncomingCallService>("/call");
        _webSocketServer.Start();
        
        _logger.Info("WebSocket server is now running");
        
    }

    public void Stop(string reason, CloseStatusCode closeStatusCode = CloseStatusCode.Normal)
    {
        if (_webSocketServer != null)
        {
            _webSocketServer.Stop(closeStatusCode, reason);
            _webSocketServer = null;
            _logger.Info("WebSocket server is now stopped");
        }
    }

    private void CLIThread()
    {
        while (true)
        {
            string? input = Console.ReadLine();
            string[] args = input.Split(" ");

            if (args.Length == 0)
            {
                _logger.Warning("Invalid arguments length");
                continue;
            }

            switch (args[0].ToLower())
            {
            case "cache":
                if (args.Length < 2)
                {
                    _logger.Warning("Invalid arguments length");
                    continue;
                }
                
                if (args[1].ToLower() == "clients")
                {
                    List<Client> clients = _clientCache.GetAll();
                    clients.AddRange(_unknownClientCache.GetAll());

                    _logger.Info($"All connected clients ({clients.Count}):");
                    clients.ForEach(c =>
                    {
                        Console.WriteLine("---------------------------------------------");
                        Console.WriteLine($"Session ID: {c.SessionId}");
                        Console.WriteLine($"Client ID: {c.ClientId}");
                        Console.WriteLine("---------------------------------------------");
                    });
                }
                    
                break;
            
                case "server":
                    if (args.Length < 2)
                    {
                        _logger.Warning("Invalid arguments length");
                        continue;
                    }

                    if (args[1].ToLower() == "status")
                    {
                        _logger.Info("Server status: " + (_webSocketServer != null && _webSocketServer.IsListening ? "running" : "N/A"));
                    } 
                    else if (args[1].ToLower() == "start")
                    {
                        if (_webSocketServer != null && _webSocketServer.IsListening)
                        {
                            _logger.Warning("WebSocket server is already running");
                            continue;
                        }

                        Start();
                        _logger.Info("Manually started WebSocket server");
                    }
                    else if (args[1].ToLower() == "stop")
                    {
                        if (_webSocketServer == null || !_webSocketServer.IsListening)
                        {
                            _logger.Warning("WebSocket server is already stopped");
                            continue;
                        }

                        Stop("Manually stopped");
                        _logger.Info("Manually stopped WebSocket server");
                    }
                    
                    break;
            }
        }
    }

    public static ServerMain CreateOrGetInstance()
    {
        return _instance != null ? _instance : new ServerMain();
    }
}