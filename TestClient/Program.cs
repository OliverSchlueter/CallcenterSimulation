using WebSocketSharp;

static void oneClient()
{
    WebSocket client = new WebSocket("ws://127.0.0.1:1337/call");

    client.OnMessage += (sender, eventArgs) => Console.WriteLine("Message: " + eventArgs.Data);

    client.Connect();

    while (true)
    {
        client.Send(Console.ReadLine());
    }
}

static void stressTest()
{
    List<WebSocket> webSockets = new List<WebSocket>();

    new Thread(() =>
    {
        while(true)
        {
            WebSocket webSocket = new WebSocket("ws://127.0.0.1:1337/call");
            webSockets.Add(webSocket);
            webSocket.Connect();
            Console.WriteLine("Connected clients: " + webSockets.Count);
            Thread.Sleep(100);
            webSocket.Send(
                "{\"client_id\": \"%id%\", \"call\": \"start\", \"channel\": \"support\"}"
                    .Replace("%id%", Guid.NewGuid().ToString()));

            Thread.Sleep(100);
        }
    }).Start();
}

Console.Title = "CallcenterSimulation | TestClient";
stressTest();
stressTest();
stressTest();