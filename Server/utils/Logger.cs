namespace Server.Utils;

public class Logger
{
    private static string Format = "[%level%] [%time%]: %message%";
    public bool IsDebug { get; set; }

    public Logger(bool isDebug = false)
    {
        IsDebug = isDebug;
    }

    public void Log(string level, string message)
    {
        string msg = Format
            .Replace("%level%", level)
            .Replace("%message%", message)
            .Replace("%time%", DateTime.Now.ToLongTimeString());

        Console.WriteLine(msg);
    }
    
    public void Info(string message)
    {
        Log("INFO", message);
    }

    public void Debug(string message)
    {
        if (!IsDebug) return;
        
        Log("DEBUG", message);
    }

    public void Warning(string message)
    {
        Log("WARNING", message);
    }

    public void Error(string message)
    {
        Log("ERROR", message);
    }
}