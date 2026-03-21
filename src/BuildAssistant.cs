using static Logger;
using static Logger.MessageType;

public class ProjectConfig
{
    public string CompilerFlags { get; set; } = "";
    public string MainFile { get; set; } = "";
    public string OutputFile { get; set; } = "";
}

public static class BuildAssistant
{
    private static readonly List<IBuilder> Builders =
    [
        new CsBuilder(),
        //new GccBuilder();
        new RustBuilder(),
        //new GoBuilder(),
        //new JavaBuilder();
    ];

    public static void Build()
    {
        var config = JsonHandler.LoadConfig();
        if (config == null || string.IsNullOrEmpty(config.MainFile)) 
        {
            Log(Err, "MainFile is missing in bob-config.json\n");
            return;
        }

        string extension = Path.GetExtension(config.MainFile).ToLower();
        var builder = Builders.FirstOrDefault(b => b.CanHandle(extension));

        if (builder != null)
            builder.Build(config);
        else
            Log(Err, $"no builder found for {extension}\n");
    }
}