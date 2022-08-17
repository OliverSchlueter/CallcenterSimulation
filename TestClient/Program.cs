using WebSocketSharp;

WebSocket client = new WebSocket("ws://127.0.0.1:1337/call");

client.OnMessage += ClientOnMessage;

client.Connect();

while (true)
{
    client.Send(Console.ReadLine());
}

static void ClientOnMessage(object? sender, MessageEventArgs e)
{
    Console.WriteLine("Message: " + e.Data);
}