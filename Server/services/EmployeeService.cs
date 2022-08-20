using WebSocketSharp;
using WebSocketSharp.Server;
using ErrorEventArgs = WebSocketSharp.ErrorEventArgs;

namespace Server.Services;

public class EmployeeService : WebSocketBehavior
{
    protected override void OnOpen()
    {
        ServerMain.Instance.Logger.Info($"[+] Employee - {ID}");
    }

    protected override void OnMessage(MessageEventArgs e)
    {
        ServerMain.Instance.Logger.Info($"[MSG] Employee {ID} - {e.Data}");
    }

    protected override void OnClose(CloseEventArgs e)
    {
        ServerMain.Instance.Logger.Info($"[-] Employee - {ID}");
    }

    protected override void OnError(ErrorEventArgs e)
    {
        
    }
}