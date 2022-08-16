namespace Server;

public static class Program
{
    static void Main()
    {
        ServerMain main = ServerMain.CreateOrGetInstance();
        main.Initialize();
        main.Start();
    }
}
