using System;
using System.Threading;
using CustomerClient.forms;

namespace CustomerClient.Calls;

public class CallHandler
{
    private Call _currentCall;
    public Call CurrentCall => _currentCall;

    public CallHandler()
    {
        _currentCall = new Call();
    }

    public void Call(string channel)
    {
        if (_currentCall.CallStatus != CallStatus.None)
        {
            //TODO: already in call
            return;
        }

        channel = channel.ToLower();

        _currentCall.Channel = channel;
        _currentCall.CallStatus = CallStatus.Calling;

        CallForm callForm = new CallForm(_currentCall);
        callForm.Show();

        new Thread(CallingBeep).Start();
        
        CustomerClientApp.Instance.WebSocketClient.Send(
                                                    "{" +
                                                        "\"call\":\"start\"," +
                                                        "\"channel\":\"" + channel + "\"" +
                                                        "}"
                                                    );
    }

    private void CallingBeep()
    {
        while (_currentCall.CallStatus == CallStatus.Calling)
        {
            Console.Beep(500, 1000);
            Thread.Sleep(500);
        }
    }
}