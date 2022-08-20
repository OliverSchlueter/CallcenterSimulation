using System;
using CustomerClient.forms;

namespace CustomerClient.Calls;

public enum CallStatus
{
    Calling,
    InCall,
    None
}

public class Call
{
    private string _channel;
    public string Channel { get => _channel; set => _channel = value; }

    private CallStatus _callStatus;
    public CallStatus CallStatus { get => _callStatus; set => _callStatus = value; }

    private CallForm _callForm;
    public CallForm CallForm { get => _callForm; set => _callForm = value; }

    public Call()
    {
        _channel = "";
        _callStatus = CallStatus.None;
    }

    public void CallAccepted()
    {
        _callStatus = CallStatus.InCall;
        OnCallAccepted(new CallAcceptedEventArgs(_channel, _callStatus));
    }

    private void OnCallAccepted(CallAcceptedEventArgs e)
    {
        EventHandler<CallAcceptedEventArgs> eventHandler = CallAcceptedEvent;
        if (eventHandler != null) 
            eventHandler(this, e);
        
    }

    public event EventHandler<CallAcceptedEventArgs> CallAcceptedEvent; 

    public void HangUp()
    {
        if (_callStatus == CallStatus.None)
            return;
        
        CustomerClientApp.Instance.WebSocketClient.Send(
            "{" +
            "\"call\":\"end\"," +
            "\"channel\":\"" + _channel + "\"" +
            "}"
        );
        
        _channel = "";
        _callStatus = CallStatus.None;
    }
}

public class CallAcceptedEventArgs : EventArgs
{
    public string Channel { get; set; }
    public CallStatus Status { get; set; }

    public CallAcceptedEventArgs(string channel, CallStatus status)
    {
        Channel = channel;
        Status = status;
    }
}