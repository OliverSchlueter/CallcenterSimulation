using EmployeeClient.forms;

namespace EmployeeClient.calls
{
    public class CallHandler
    {
        private Call _currentCall;
        public Call CurrentCall => _currentCall;
        
        public void Call(string channel)
        {
            if (_currentCall != null && _currentCall.CallStatus == CallStatus.InCall)
                return;
            
            _currentCall = new Call();
            _currentCall.Channel = channel;
            CallForm callForm = new CallForm(_currentCall);
            callForm.Show();
            
            EmployeeClientApp.Instance.WebSocketClient.Send(
                                                            "{" +
                                                            "\"call\":\"pull\"," +
                                                            "\"channel\":\"" + channel + "\"" +
                                                            "}"
            );
        }
    }
}