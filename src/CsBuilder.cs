using static Logger;
using static Logger.MessageType;
using System.Diagnostics;

public class CsBuilder : IBuilder
{
    public string Name => "csharp (dotnet)";
    public bool CanHandle(string ext) => ext is ".cs" or ".csproj" or ".sln";

    public void Build(ProjectConfig config)
    {
        Log(Default, $"{Name} project detected\n");
        Stopwatch timer = Stopwatch.StartNew();
        string targetProject = config.MainFile;
        string extension = Path.GetExtension(targetProject).ToLower();

        if (extension == ".cs")
             Log(Warn, "single file build. for better performance and dependencies use .csproj file\n");

        string outPath = string.IsNullOrWhiteSpace(config.OutputFile) ? "publish" : config.OutputFile;
        string cmd = "dotnet";
        string args = $"publish {targetProject} {config.CompilerFlags} -o {outPath}".Trim();
        Log(Default, $"running \"{cmd} {args}\"\n");
        int result = CommandRunner.Run(cmd, args);
        timer.Stop();
        string elapsed = timer.Elapsed.TotalSeconds.ToString("0.0");

        if (result == 0)
            Log(Done, $"build finished successfully in {elapsed}s. output located in {outPath}\n");
        else
            Log(Err, $"project build failed. (exit code {result})\n");
    }
}