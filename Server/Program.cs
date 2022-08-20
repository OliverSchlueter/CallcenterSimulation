namespace Server;

public static class Program
{
    private static void Main()
    {
        Console.Title = "CallcenterSimulation | Server";
        
        Console.WriteLine($"Starting server application now (v{ServerMain.Version})");
        ServerMain main = ServerMain.CreateOrGetInstance();
        main.Start();
    }
}
