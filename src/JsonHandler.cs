using System.Text.Json;
using static Logger;
using static Logger.MessageType;

public static class JsonHandler
{
    private const string FileName = "bob-config.json";

    public static void CreateTemplate()
    {
        if (!File.Exists(FileName))
        {
            var template = new
            {
                CompilerFlags = "",
                MainFile = "",
                OutputFile = ""
            };

            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(template, options);
            File.WriteAllText(FileName, jsonString);
            Log(Default, "created empty bob-config.json template\n");
        }
        else
            Log(Err, "bob-config.json already exists\n");
    }

    public static ProjectConfig? LoadConfig()
    {
        if (!File.Exists(FileName))
        {
            Log(Err, "could not find bob-config.json. make sure to run 'bob make-json' and specify the data.\n");
            throw new FileNotFoundException();
        }
        try 
        {
            string jsonString = File.ReadAllText(FileName);
            return JsonSerializer.Deserialize<ProjectConfig>(jsonString);
        }
        catch (Exception ex)
        {
            Log(Err, ex.Message+'\n');
            return null;
        }
    }
}