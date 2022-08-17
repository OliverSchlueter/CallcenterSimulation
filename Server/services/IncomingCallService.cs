using WebSocketSharp;
using WebSocketSharp.Server;
using ErrorEventArgs = WebSocketSharp.ErrorEventArgs;

namespace Server.Services;

public class IncomingCallService : WebSocketBehavior
{
    protected override void OnOpen()
    {
        ServerMain.Instance.Logger.Info("New connection with ID: " + ID);
    }

    protected override void OnClose(CloseEventArgs e)
    {
        base.OnClose(e);
    }

    protected override void OnMessage(MessageEventArgs e)
    {
        ServerMain.Instance.Logger.Info("New message from " + ID + ": " + e.Data);
    }

    protected override void OnError(ErrorEventArgs e)
    {
        base.OnError(e);
    }
}