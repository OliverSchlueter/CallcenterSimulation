namespace Server.Clients;

public class Call
{
    private Client _client1;
    public Client Client1 => _client1;
    
    private Client _client2;
    public Client Client2 => _client2;
    
    private DateTime? _startTime;
    public DateTime? StartTime => _startTime;
    
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
        
    }


    /// <param name="client">Client who caused the stop</param>
    public void Stop(Client client)
    {
        
    }
}