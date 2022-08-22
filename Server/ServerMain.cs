using Server.Clients;
using Server.Services;
using Server.Utils;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace Server;

public class ServerMain
{
    private static ServerMain _instance;
    public static ServerMain Instance => _instance;

    public const string Version = "0.0.2-ALPHA";

    private readonly Utils.Logger _logger;
    public Utils.Logger Logger => _logger;
    
    private WebSocketServer? _webSocketServer;
    public WebSocketServer? WebSocketServer => _webSocketServer;

    private readonly Cache<string, Client> _clientCache;
    public Cache<string, Client> ClientCache => _clientCache;

    private readonly Dictionary<string, string> _sessionIdClientId; // key: sessionId value: clientId
    public Dictionary<string, string> SessionIdClientId => _sessionIdClientId;

    private readonly Cache<string, Client> _unknownClientCache;
    public Cache<string, Client> UnknownClientCache => _unknownClientCache;

    private readonly Dictionary<string, Queue<Client>> _waitingCustomers;
    public Dictionary<string, Queue<Client>> WaitingCustomers => _waitingCustomers;

    private readonly Dictionary<Client, Call> _currentCalls;
    public Dictionary<Client, Call> CurrentCalls => _currentCalls;

    private ServerMain()
    {
        _instance = this;
        _logger = new Utils.Logger(true);
        _sessionIdClientId = new Dictionary<string, string>();
        _clientCache = new Cache<string, Client>(false);
        _unknownClientCache = new Cache<string, Client>(false);
        _waitingCustomers = new Dictionary<string, Queue<Client>>();
        _currentCalls = new Dictionary<Client, Call>();
        
        new Thread(CLIThread).Start();
    }

    public void Start()
    {
        _webSocketServer = new WebSocketServer("ws://127.0.0.01:1337");
        _webSocketServer.AddWebSocketService<IncomingCallService>("/call");
        _webSocketServer.AddWebSocketService<EmployeeService>("/employee");
        _webSocketServer.Start();
        
        _logger.Info("WebSocket server is now running");
    }

    public void Stop(string reason, CloseStatusCode closeStatusCode = CloseStatusCode.Normal)
    {
        if (_webSocketServer == null)
            return;
        
        _logger.Info("Stopping WebSocket server now");
        
        Thread closeConnectionsThread = new Thread(() =>
        {
            IEnumerator<IWebSocketSession> enumerator = _webSocketServer.WebSocketServices["/call"].Sessions.Sessions.GetEnumerator();
            
            while (enumerator.MoveNext())
            {
                IWebSocketSession session = enumerator.Current;
                session.Context.WebSocket.Close();
                
                Thread.Sleep(50);
            }
        });
            
        _clientCache.Clear();
        _unknownClientCache.Clear();
        _currentCalls.Clear();
        _sessionIdClientId.Clear();
        _waitingCustomers.Clear();
        
        
        closeConnectionsThread.Start();
        
        // wait for the thread to close all sessions
        closeConnectionsThread.Join();

        _webSocketServer.Stop(closeStatusCode, reason);
        _webSocketServer = null;
        _logger.Info("WebSocket server is now stopped");
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
                        Console.WriteLine($"Role: {c.Role}");
                        Console.WriteLine($"Call status: {c.CallStatus}");
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
                
                case "customer_queue":
                    string channel = args[1].ToLower();

                    if (!_waitingCustomers.ContainsKey(channel))
                        return;
                    
                    _logger.Info($"All waiting customers in {channel} channel ({_waitingCustomers[channel].Count}):");
                    foreach (var client in _waitingCustomers[channel])
                    {
                        Console.WriteLine(" - " + client.ClientId);
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