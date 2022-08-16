using WebSocketSharp;
using WebSocketSharp.Server;
using ErrorEventArgs = WebSocketSharp.ErrorEventArgs;

namespace Server.Services;

public class IncomingCallService : WebSocketBehavior
{
    protected override void OnOpen()
    {
        base.OnOpen();
    }

    protected override void OnClose(CloseEventArgs e)
    {
        base.OnClose(e);
    }

    protected override void OnMessage(MessageEventArgs e)
    {
        base.OnMessage(e);
    }

    protected override void OnError(ErrorEventArgs e)
    {
        base.OnError(e);
    }
}