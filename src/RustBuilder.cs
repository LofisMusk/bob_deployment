using static Logger;
using static Logger.MessageType;

public class RustBuilder : IBuilder
{
    public string Name => "rust (cargo)";
    public bool CanHandle(string extension) => extension is ".rs" or ".toml";

    public void Build(ProjectConfig config)
    {
        Log(Default, $"{Name} project detected\n");
        if (CommandRunner.RunQuiet("cargo", "--version") != 0)
        {
            Log(Err, "cargo is not installed or not in PATH, unable to build\n");
            return;
        }
        Log(Default, "cargo is present\n");
        string targetProject = config.MainFile;
        string extension = Path.GetExtension(targetProject).ToLower();
        string cmd;
        string args;
        string outPath;

        if (extension == ".toml")
        {
            cmd = "cargo";
            string outputFlag = string.IsNullOrWhiteSpace(config.OutputFile) 
                ? "" 
                : $"--target-dir {config.OutputFile}";
            args = $"build {outputFlag} {config.CompilerFlags}".Trim();
            
            if (!string.IsNullOrWhiteSpace(config.OutputFile))
                outPath = config.OutputFile;
            else
            {
                bool isRelease = config.CompilerFlags.Contains("-r") || config.CompilerFlags.Contains("--release");
                outPath = isRelease ? "target/release" : "target/debug";
            }
        }
        else
        {
            cmd = "rustc";
            string binaryName = string.IsNullOrWhiteSpace(config.OutputFile) ? "app.out" : config.OutputFile;
            args = $"{targetProject} -o {binaryName} {config.CompilerFlags}";
            outPath = binaryName;
            Log(Warn, "single file build. for better performance and dependencies use Cargo.toml\n");
        }

        Log(Default, $"running \"{cmd} {args}\"\n");
        int result = CommandRunner.Run(cmd, args);

        if (result == 0)
            Log(Done, $"build finished successfully. output located in {outPath}\n");
        else
            Log(Err, $"project build failed. (exit code {result})\n");
    }
}