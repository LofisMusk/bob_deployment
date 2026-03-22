using static Logger;
using static Logger.MessageType;

public class Program
{
    public static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            DisplayHelp();
            return;
        }

        string command = args[0].ToLower();

        switch (command)
        {
            case "build":
                BuildAssistant.Build();
                break;
            case "fetch":
                Log(Default, "to do\n");
                break;
            case "config":
                JsonHandler.CreateTemplate();
                break;
            case "info":
                DisplayInfo();
                break;
            default:
                Log(Err, $"unknown command: {command}\n");
                break;
        }
    }

    private static void DisplayHelp()
    {
        Console.WriteLine("usage: bob [command]");
        Console.WriteLine("available commands:");
        Console.WriteLine("build   : read bob-config.json and compile project to executable");
        Console.WriteLine("fetch   : clone a repository and automatically build the project (if bobconfig.json exists)");
        Console.WriteLine("config  : create an empty bobconfig.json template");
        Console.WriteLine("info    : display information about bob");
    }

    private static void DisplayInfo()
    {
        Console.WriteLine("bob (build orchestrator binary) version 0.1-alpha");
        Console.WriteLine("copyright (c) 2026 Michael Zenith");
        Console.WriteLine("licensed under GPL license");
    }
}