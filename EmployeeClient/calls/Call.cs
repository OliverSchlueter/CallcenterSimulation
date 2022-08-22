using EmployeeClient.forms;

namespace EmployeeClient.calls
{
    public enum CallStatus
    {
        Pulling,
        InCall,
        HangUp,
        None
    }

    public class Call
    {
        private string _channel;
        public string Channel { get => _channel; set => _channel = value; }

        private CallStatus _callStatus;
        public CallStatus CallStatus { get => _callStatus; set => _callStatus = value; }

        private string _partnerId;
        public string PartnerId => _partnerId;

        private CallForm _callForm;
        public CallForm CallForm { get => _callForm; set => _callForm = value; }
        
        public Call()
        {
            _channel = "";
            _callStatus = CallStatus.Pulling;
        }

        public void Pulled(string partnerId)
        {
            _callStatus = CallStatus.InCall;
            _partnerId = partnerId;
            _callForm.lbl_status.Text = "Status: " + _callStatus.ToString();
            _callForm.lbl_customer.Text = "Customer: " + partnerId;
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
            
            _callForm.Close();
        }
    }
}