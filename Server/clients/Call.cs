using Server.Utils;

namespace Server.Clients;

public class Call
{
    private readonly Client _customer;
    public Client Customer => _customer;
    
    private readonly Client _employee;
    public Client Employee => _employee;
    
    private DateTime? _startTime;
    public DateTime? StartTime => _startTime;
    
    private DateTime? _endTime;
    public DateTime? EndTime => _endTime;
    
    private CallStatus _callStatus;
    public CallStatus CallStatus => _callStatus;
    
    public Call(Client customer, Client employee)
    {
        _customer = customer;
        _employee = employee;
        _callStatus = CallStatus.Calling;
    }

    public void Start()
    {
        ServerMain.Instance.Logger.Debug($"Starting call with: {_customer.ClientId} and {_employee.ClientId}");
        
        _startTime = DateTime.Now;
        _callStatus = CallStatus.InCall;
        _customer.CallStatus = CallStatus.InCall;
        _employee.CallStatus = CallStatus.InCall;

        _customer.Session.Send("{\"call_status\": \"InCall\", \"call_partner\": \"%client_id%\"}".Replace("%client_id%", _employee.ClientId));
        Thread.Sleep(50);
        _employee.Session.Send("{\"call_status\": \"InCall\", \"call_partner\": \"%client_id%\"}".Replace("%client_id%", _customer.ClientId));

        ServerMain.Instance.CurrentCalls.Add(_customer, this);
        ServerMain.Instance.CurrentCalls.Add(_employee, this);
    }


    /// <param name="client">Client who caused the stop</param>
    public void Stop(Client client)
    {
        _endTime = DateTime.Now;
        _callStatus = CallStatus.HangUp;
        _customer.CallStatus = CallStatus.HangUp;
        _employee.CallStatus = CallStatus.HangUp;
        
        _customer.Session.Send("{\"call_status\": \"HangUp\", \"who_ended\": \"%client_id%\"}".Replace("%client_id%", client.ClientId));
        Thread.Sleep(50);
        _employee.Session.Send("{\"call_status\": \"HangUp\", \"who_ended\": \"%client_id%\"}".Replace("%client_id%", client.ClientId));
        
        ServerMain.Instance.CurrentCalls.Remove(_customer);
        ServerMain.Instance.CurrentCalls.Remove(_employee);
    }

    public void Send(Client sender, string message)
    {
        Client receiver = sender.ClientId == _customer.ClientId ? _employee : _customer;
        
        receiver.Session.Send("{\"call_message_from\": \"%senderId%\", \"call_message_hash\": \"%hash%\",  \"call_message\": \"%data%\"}"
            .Replace("%senderId%", sender.ClientId)
            .Replace("%hash%", HashUtils.Sha256(message))
            .Replace("%data%", message));
    }
}