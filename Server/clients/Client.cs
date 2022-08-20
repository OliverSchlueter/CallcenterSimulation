using WebSocketSharp;

namespace Server.Clients;

public class Client
{
    private string _sessionID;
    public string SessionId { get => _sessionID; set => _sessionID = value; }
    
    private string? _clientID;
    public string? ClientId { get => _clientID; set => _clientID = value; }

    private readonly WebSocket _session;
    
    private Role _role;
    public Role Role { get => _role; set => _role = value; }
    
    private CallStatus _callStatus;
    public CallStatus CallStatus { get => _callStatus; set => _callStatus = value; }
    
    public Client(string sessionId, string? clientId, Role role = Role.Unknown)
    {
        _sessionID = sessionId;
        _clientID = clientId;
        _callStatus = CallStatus.None;
        _role = role;
        _session = ServerMain.Instance.WebSocketServer.WebSocketServices["/call"].Sessions[sessionId].Context.WebSocket;
    }

    public void CallAction(CallStatus callStatus, string? channel)
    {
        switch (callStatus)
        {
            case CallStatus.Calling:
                if (channel == null)
                    return;
                
                JoinCallQueue(channel);
                break;
            case CallStatus.HangUp:
                HangUp(channel);
                break;
        }
    }

    private void JoinCallQueue(string channel)
    {
        // create queue for channel if not exists
        if (!ServerMain.Instance.WaitingCustomers.ContainsKey(channel.ToLower()))
            ServerMain.Instance.WaitingCustomers.Add(channel.ToLower(), new Queue<Client>());
        
        // client is already in queue
        if (ServerMain.Instance.WaitingCustomers[channel.ToLower()].Contains(this))
            return;
        
        ServerMain.Instance.WaitingCustomers[channel.ToLower()].Enqueue(this);
        _callStatus = CallStatus.Calling;
    }

    private void HangUp(string? channel)
    {
        if (ServerMain.Instance.CurrentCalls.ContainsKey(this))
        {
            Call call = ServerMain.Instance.CurrentCalls[this];
            call.Stop(this);   
        }
        else if (channel == null)
        {
            // TODO: search in all waiting queues
        }
        else if (ServerMain.Instance.WaitingCustomers[channel].Contains(this))
        {
            // TODO: remove from waiting customers
        }
        
        _callStatus = CallStatus.HangUp;
    }
}