namespace Server.Clients;

public class Call
{
    private readonly Client _client1;
    public Client Client1 => _client1;
    
    private readonly Client _client2;
    public Client Client2 => _client2;
    
    private DateTime? _startTime;
    public DateTime? StartTime => _startTime;
    
    private DateTime? _endTime;
    public DateTime? EndTime => _endTime;
    
    private CallStatus _callStatus;
    public CallStatus CallStatus => _callStatus;
    
    public Call(Client client1, Client client2)
    {
        _client1 = client1;
        _client2 = client2;
        _callStatus = CallStatus.Calling;
    }

    public void Start()
    {
        _startTime = DateTime.Now;
        _callStatus = CallStatus.InCall;
        
        ServerMain.Instance.CurrentCalls.Add(_client1, this);
        ServerMain.Instance.CurrentCalls.Add(_client2, this);
        
        ServerMain.Instance.Logger.Debug($"Starting call with: {_client1.ClientId} and {_client2.ClientId}");
        
        //TODO: inform both clients about the start of the call
    }


    /// <param name="client">Client who caused the stop</param>
    public void Stop(Client client)
    {
        _endTime = DateTime.Now;
        _callStatus = CallStatus.HangUp;
        
        ServerMain.Instance.CurrentCalls.Remove(_client1);
        ServerMain.Instance.CurrentCalls.Remove(_client2);
    }
}