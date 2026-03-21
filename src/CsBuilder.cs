using static Logger;
using static Logger.MessageType;

public class CsBuilder : IBuilder
{
    public string Name => "csharp (dotnet)";
    public bool CanHandle(string ext) => ext is ".cs" or ".csproj" or ".sln";

    public void Build(ProjectConfig config)
    {
        Log(Default, $"{Name} project detected\n");
        if (CommandRunner.RunQuiet("dotnet", "--version") != 0)
        {
            Log(Err, "dotnet is not installed or not in PATH, unable to build\n");
            return;
        }
        Log(Default, "dotnet is present\n");
        string targetProject = config.MainFile;
        string extension = Path.GetExtension(targetProject).ToLower();

        if (extension == ".cs")
             Log(Warn, "single file build. for better performance and dependencies use .csproj file\n");

        string outPath = string.IsNullOrWhiteSpace(config.OutputFile) ? "publish" : config.OutputFile;
        string cmd = "dotnet";
        string args = $"publish {targetProject} {config.CompilerFlags} -o {outPath}".Trim();
        Log(Default, $"running \"{cmd} {args}\"\n");
        int result = CommandRunner.Run(cmd, args);

        if (result == 0)
            Log(Done, $"build finished successfully. output located in {outPath}\n");
        else
            Log(Err, $"project build failed. (exit code {result})\n");
    }
}