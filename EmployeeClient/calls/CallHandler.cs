using EmployeeClient.forms;

namespace EmployeeClient.calls
{
    public class CallHandler
    {
        public void Call(string channel)
        {
            Call call = new Call();
            call.Channel = channel;
            CallForm callForm = new CallForm(call);
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