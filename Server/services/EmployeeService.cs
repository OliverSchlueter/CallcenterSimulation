using Server.Clients;
using Server.Utils;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace Server.Services;

public class EmployeeService : WebSocketBehavior
{
    protected override void OnOpen()
    {
        ServerMain.Instance.Logger.Info($"[+] Employee - {ID}");

        ServerMain.Instance.UnknownClientCache.Put(ID, new Client(ID, null, "/employee"));
    }

    protected override void OnMessage(MessageEventArgs e)
    {
        ServerMain.Instance.Logger.Info($"[MSG] Employee {ID} - {e.Data}");
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

        if (client != null && json.ContainsKey("call") && json.ContainsKey("channel"))
        {
            CallStatus callStatus = CallStatus.None;
            
            switch (json["call"])
            {
                case "pull":
                    callStatus = CallStatus.Pull;
                    break;
                case "end":
                    callStatus = CallStatus.HangUp;
                    break;
            }
            
            string channel = json["channel"];
            
            client.CallAction(callStatus, channel);
        }
        
        if (client != null && json.ContainsKey("call_message"))
        {
            if (!ServerMain.Instance.CurrentCalls.ContainsKey(client))
                return;

            Call call = ServerMain.Instance.CurrentCalls[client];
            call.Send(client, json["call_message"]);
        }
    }

    protected override void OnClose(CloseEventArgs e)
    {
        ServerMain.Instance.Logger.Info($"[-] Employee - {ID}");
        
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
}