public static class Logger
{
    public enum MessageType
    {
        Default,
        Done,
        Info,
        Warn,
        Err
    }

    public static void Log(MessageType type, string message)
    {
        switch (type)
        {
            case MessageType.Default:
                PrintLabel("bob", ConsoleColor.Blue);
                break;
            case MessageType.Done:
                PrintLabel("done", ConsoleColor.Green);
                break;
            case MessageType.Info:
                PrintLabel("info", ConsoleColor.White);
                break;
            case MessageType.Warn:
                PrintLabel("warn", ConsoleColor.Yellow);
                break;
            case MessageType.Err:
                PrintLabel("err", ConsoleColor.Red);
                break;
        }
        Console.Write(message);
    }

    public static void PrintLabel(string text, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.Write("["+text+"]");
        Console.ResetColor();
        Console.Write(" ");
    }
}