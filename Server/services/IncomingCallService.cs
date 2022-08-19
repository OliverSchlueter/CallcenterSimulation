using Server.Clients;
using Server.Utils;
using WebSocketSharp;
using WebSocketSharp.Server;
using ErrorEventArgs = WebSocketSharp.ErrorEventArgs;

namespace Server.Services;

public class IncomingCallService : WebSocketBehavior
{
    protected override void OnOpen()
    {
        ServerMain.Instance.Logger.Info("New connection with ID: " + ID);
        
        ServerMain.Instance.UnknownClientCache.Put(ID, new Client(ID, null));
    }

    protected override void OnClose(CloseEventArgs e)
    {
        ServerMain.Instance.Logger.Info("Connection quit with ID: " + ID);
    
        if (ServerMain.Instance.ClientCache.Contains(ID))
        {
            //TODO: check if client was in a active call
        }

        if (ServerMain.Instance.ClientIdSessionId.ContainsKey(ID))
        {
            ServerMain.Instance.ClientIdSessionId.Remove(ID);
        }
    }

    protected override void OnMessage(MessageEventArgs e)
    {
        ServerMain.Instance.Logger.Info("New message from " + ID + ": " + e.Data);
        Dictionary<string, string>? json = JsonUtils.Deserialize(e.Data);
        if (json == null)
            return;

        Client? client = null;

        if (ServerMain.Instance.UnknownClientCache.Contains(ID))
            client = ServerMain.Instance.UnknownClientCache.Get(ID);
        else if (ServerMain.Instance.ClientIdSessionId.ContainsKey(ID))
            client = ServerMain.Instance.ClientCache.Get(ServerMain.Instance.ClientIdSessionId[ID]);

        if (json.ContainsKey("client_id") && ServerMain.Instance.UnknownClientCache.Contains(ID))
        {
            string clientId = json["client_id"];
            client = ServerMain.Instance.UnknownClientCache.Get(ID);
            client.ClientId = clientId;

            if (ServerMain.Instance.ClientCache.Contains(clientId))
            {
                //TODO: client already exists in cache
                return;
            }
            
            ServerMain.Instance.ClientCache.Put(clientId, client);
            ServerMain.Instance.ClientIdSessionId.Add(ID, clientId);
            ServerMain.Instance.UnknownClientCache.Remove(ID);
        }
        
        if (client != null && json.ContainsKey("role"))
        {
            string roleName = json["role"];
            Role.TryParse(roleName, out Role role);
            client.Role = role;
        }

        if (client != null && json.ContainsKey("call"))
        {
            CallStatus callStatus = CallStatus.None;
            
            switch (json["call"])
            {
                case "start":
                    callStatus = CallStatus.Calling;
                    break;
                case "end":
                    callStatus = CallStatus.HangUp;
                    break;
            }

            //TODO: channel handling
            string? channel = null;

            if (json.ContainsKey("channel"))
                channel = json["channel"];

            client.CallAction(callStatus, channel);
        }
    }

    protected override void OnError(ErrorEventArgs e)
    {
        
    }
}