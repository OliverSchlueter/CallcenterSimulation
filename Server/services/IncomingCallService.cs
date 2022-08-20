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
        ServerMain.Instance.Logger.Info($"[+] Customer - {ID}");
        
        ServerMain.Instance.UnknownClientCache.Put(ID, new Client(ID, null));
    }

    protected override void OnClose(CloseEventArgs e)
    {
        ServerMain.Instance.Logger.Info($"[-] Customer - {ID}");

        string clientId = "";

        if (ServerMain.Instance.SessionIdClientId.ContainsKey(ID))
        {
            clientId = ServerMain.Instance.SessionIdClientId[ID];
            ServerMain.Instance.SessionIdClientId.Remove(ID);
        }
        
        if (clientId.Length != 0 && ServerMain.Instance.ClientCache.Contains(clientId))
        {
            ServerMain.Instance.ClientCache.AutoRemove(clientId);
            
            Client client = ServerMain.Instance.ClientCache.Get(clientId);
            client.CallAction(CallStatus.HangUp, null);
            client.SessionId = "";
        }
    }

    protected override void OnMessage(MessageEventArgs e)
    {
        ServerMain.Instance.Logger.Info($"[MSG] Customer {ID} - {e.Data}");
        Dictionary<string, string>? json = JsonUtils.Deserialize(e.Data);
        if (json == null)
            return;

        Client? client = null;

        if (ServerMain.Instance.UnknownClientCache.Contains(ID))
            client = ServerMain.Instance.UnknownClientCache.Get(ID);
        else if (ServerMain.Instance.SessionIdClientId.ContainsKey(ID))
            client = ServerMain.Instance.ClientCache.Get(ServerMain.Instance.SessionIdClientId[ID]);

        if (json.ContainsKey("client_id") && ServerMain.Instance.UnknownClientCache.Contains(ID))
        {
            string clientId = json["client_id"];
            client = ServerMain.Instance.UnknownClientCache.Get(ID);

            if (ServerMain.Instance.ClientCache.Contains(clientId))
            {
                client = ServerMain.Instance.ClientCache.Get(clientId);
                client.SessionId = ID;
            }
            else
            {
                client.ClientId = clientId;
                ServerMain.Instance.ClientCache.Put(clientId, client);
            }

            client.CallStatus = CallStatus.None;
            ServerMain.Instance.SessionIdClientId.Add(ID, clientId);
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
}