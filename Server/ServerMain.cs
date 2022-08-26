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

    public const string Version = "1.1.2-rc.1";

    private const int AutoRestartDelay = 60*6; // in minutes

    private readonly Utils.Logger _logger;
    public Utils.Logger Logger => _logger;
    
    private WebSocketServer? _webSocketServer;
    public WebSocketServer? WebSocketServer => _webSocketServer;

    private bool _isStopping;
    public bool IsStopping => _isStopping;

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
        _isStopping = false;
        _sessionIdClientId = new Dictionary<string, string>();
        _clientCache = new Cache<string, Client>(false);
        _clientCache.AddIndex("SessionId", client => { return client.SessionId; } );
        _unknownClientCache = new Cache<string, Client>(false);
        _waitingCustomers = new Dictionary<string, Queue<Client>>();
        _currentCalls = new Dictionary<Client, Call>();
        
        new Thread(AutoRestartThread).Start();
        new Thread(CLIThread).Start();
        new Thread(CheckClientConnectionThread).Start();
    }

    public void Start()
    {
        _isStopping = false;
        _webSocketServer = new WebSocketServer("ws://127.0.0.01:1337");
        _webSocketServer.KeepClean = true;
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

        _isStopping = true;
        
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
        
        closeConnectionsThread.Start();
        
        // wait for the thread to close all sessions
        closeConnectionsThread.Join();
        
        _clientCache.Clear();
        _unknownClientCache.Clear();
        _currentCalls.Clear();
        _sessionIdClientId.Clear();
        _waitingCustomers.Clear();

        _webSocketServer.Stop(closeStatusCode, reason);
        _webSocketServer = null;
        _logger.Info("WebSocket server is now stopped");
    }

    private void AutoRestartThread()
    {
        Thread.Sleep(1000*60*AutoRestartDelay);
        
        while (true)
        {
            Logger.Info("Automatically restarting server.");
            DateTime restartStart = DateTime.Now;

            if(_webSocketServer == null || !_webSocketServer.IsListening)
                Start();
            else
            {
                Stop("Auto restarting server");
                Thread.Sleep(5000);
                Start();
            }
            
            DateTime restartEnd = DateTime.Now;
            TimeSpan timeDifference = restartEnd.Subtract(restartStart);
            
            _logger.Info("Auto restart took " + timeDifference.Seconds + " seconds. Next auto restart: " + restartEnd.AddMinutes(AutoRestartDelay));
            
            Thread.Sleep(1000*60*AutoRestartDelay);
        }
    }

    private void CheckClientConnectionThread()
    {
        while (true)
        {
            foreach (var client in _clientCache.GetAll())
            {
                switch (client.Role)
                {
                    case Role.Customer:
                        bool ping = _webSocketServer.WebSocketServices["/call"].Sessions.PingTo(client.SessionId);
                        if (!ping)
                        {
                            if (_currentCalls.ContainsKey(client))
                                _currentCalls[client].Stop(client);

                            if (_waitingCustomers.ContainsKey(client.ClientId))
                                _waitingCustomers.Remove(client.ClientId);

                            _sessionIdClientId.Remove(client.SessionId);
                            _clientCache.Remove(client.ClientId);
                        }
                        break;
                    
                    case Role.Employee:
                        break;
                }
            }
            
            foreach (var client in _unknownClientCache.GetAll())
            {
                bool ping = _webSocketServer.WebSocketServices["/call"].Sessions.PingTo(client.SessionId);
                if (!ping)
                {
                    _unknownClientCache.Remove(client.SessionId);
                }
            }

            Thread.Sleep(1000*30);
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
                    else if (args[1].ToLower() == "restart")
                    {
                        _logger.Info("Manually restarting WebSocket server");
                        if (_webSocketServer == null || !_webSocketServer.IsListening)
                            Start();
                        else
                        {
                            Stop("Server is restarting");
                            Thread.Sleep(1000);
                            Start();
                        }
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