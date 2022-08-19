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
    }

    protected override void OnMessage(MessageEventArgs e)
    {
        ServerMain.Instance.Logger.Info("New message from " + ID + ": " + e.Data);
        Dictionary<string, string>? json = JsonUtils.Deserialize(e.Data);
        if (json == null)
            return;

        if (json.ContainsKey("client_id") && ServerMain.Instance.UnknownClientCache.Contains(ID))
        {
            string clientId = json["client_id"];
            Client client = ServerMain.Instance.UnknownClientCache.Get(ID);
            client.ClientId = clientId;

            if (ServerMain.Instance.ClientCache.Contains(clientId))
            {
                //TODO: client already exists in cache
                return;
            }
            
            ServerMain.Instance.ClientCache.Put(clientId, client);
        }
    }

    protected override void OnError(ErrorEventArgs e)
    {
        
    }
}