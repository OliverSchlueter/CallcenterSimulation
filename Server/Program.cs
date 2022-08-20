namespace Server;

public static class Program
{
    private static void Main()
    {
        Console.Title = "CallcenterSimulation | Server";
        
        Console.WriteLine("Starting server application now");
        ServerMain main = ServerMain.CreateOrGetInstance();
        main.Start();
    }
}
