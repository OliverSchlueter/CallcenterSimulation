namespace Server.Utils;

public class Logger
{
    private static string _format = "[%level%] [%time%]: %message%";
    public bool IsDebug { get; set; }

    public Logger(bool isDebug = false)
    {
        IsDebug = isDebug;
    }

    public void Log(string level, string message)
    {
        string msg = _format
            .Replace("%level%", level)
            .Replace("%message%", message)
            .Replace("%time%", DateTime.Now.ToLongTimeString());

        Console.WriteLine(msg);
    }
    
    public void Info(string message)
    {
        ConsoleColor colorBefore = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Green;
        Log("INFO", message);
        Console.ForegroundColor = colorBefore;
    }

    public void Debug(string message)
    {
        if (!IsDebug) return;
        
        ConsoleColor colorBefore = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Cyan;
        Log("DEBUG", message);
        Console.ForegroundColor = colorBefore;
    }

    public void Warning(string message)
    {
        ConsoleColor colorBefore = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Yellow;
        Log("WARNING", message);
        Console.ForegroundColor = colorBefore;
    }

    public void Error(string message)
    {
        ConsoleColor colorBefore = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Red;
        Log("ERROR", message);
        Console.ForegroundColor = colorBefore;
    }
}