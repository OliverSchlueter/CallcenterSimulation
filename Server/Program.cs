namespace Server;

public static class Program
{
    static void Main()
    {
        Console.WriteLine("Starting server application now");
        ServerMain main = ServerMain.CreateOrGetInstance();
        main.Initialize();
        main.Start();
    }
}
