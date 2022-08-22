using WebSocketSharp;

namespace Server.Clients;

public class Client
{
    private string _sessionId;
    public string SessionId { get => _sessionId; set => _sessionId = value; }
    
    private string? _clientId;
    public string? ClientId { get => _clientId; set => _clientId = value; }

    private readonly WebSocket _session;
    public WebSocket Session => _session;
    
    private Role _role;
    public Role Role { get => _role; set => _role = value; }
    
    private CallStatus _callStatus;
    public CallStatus CallStatus { get => _callStatus; set => _callStatus = value; }
    
    public Client(string sessionId, string? clientId, string service, Role role = Role.Unknown)
    {
        _sessionId = sessionId;
        _clientId = clientId;
        _callStatus = CallStatus.None;
        _role = role;
        _session = ServerMain.Instance.WebSocketServer.WebSocketServices[service].Sessions[sessionId].Context.WebSocket;
    }

    public void CallAction(CallStatus callStatus, string? channel)
    {
        switch (callStatus)
        {
            case CallStatus.Calling:
                JoinCallQueue(channel);
                break;
            case CallStatus.HangUp:
                HangUp(channel);
                break;
            case CallStatus.Pull:
                PullCustomer(channel);
                break;
        }
    }

    private void JoinCallQueue(string? channel)
    {
        if (channel == null)
            return;
        
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
        else if (ServerMain.Instance.WaitingCustomers[channel.ToLower()].Contains(this))
        {
            // TODO: remove from waiting customers
        }
        
        _callStatus = CallStatus.HangUp;
    }

    private void PullCustomer(string? channel)
    {
        if (channel == null)
            return;

        if (_role != Role.Employee)
            return;
                
        if (!ServerMain.Instance.WaitingCustomers.ContainsKey(channel.ToLower()) || ServerMain.Instance.WaitingCustomers[channel.ToLower()].Count == 0)
        {
            _session.Send("{\"call_status\": \"no_customer\"}");
            return;
        }

        Client customer = ServerMain.Instance.WaitingCustomers[channel.ToLower()].Dequeue();
                
        Call call = new Call(customer, this);
        call.Start();
    }
}