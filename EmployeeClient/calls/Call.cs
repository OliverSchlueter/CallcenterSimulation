using EmployeeClient.forms;

namespace EmployeeClient.calls
{
    public enum CallStatus
    {
        Pulling,
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
            _callStatus = CallStatus.Pulling;
        }

        public void Pulled()
        {
            _callStatus = CallStatus.InCall;
            _callForm.lbl_status.Text = _callStatus.ToString();
        }
        
        public void HangUp()
        {
            if (_callStatus == CallStatus.None)
                return;
        
            EmployeeClientApp.Instance.WebSocketClient.Send(
                "{" +
                "\"call\":\"end\"," +
                "\"channel\":\"" + _channel + "\"" +
                "}"
            );
        
            _channel = "";
            _callStatus = CallStatus.None;
        }
    }
}