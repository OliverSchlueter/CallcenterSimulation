namespace Server.Clients;

public class Client
{
    private string _sessionID;
    public string SessionId { get => _sessionID; }
    
    private string? _clientID;
    public string? ClientId { get => _clientID; }
    
    public Client(string sessionId, string? clientId)
    {
        _sessionID = sessionId;
        _clientID = clientId;
    }
}