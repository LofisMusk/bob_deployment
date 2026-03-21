using static Logger;
using static Logger.MessageType;
using System.Diagnostics;

public static class CommandRunner
{
    public static int Run(string fileName, string arguments)
    {
        var startInfo = new ProcessStartInfo
        {
            FileName = fileName,
            Arguments = arguments,
            RedirectStandardOutput = false,
            RedirectStandardError = false,
            UseShellExecute = false,
            CreateNoWindow = false
        };

        try
        {
            using var process = Process.Start(startInfo);
            
            if (process == null) return -1;

            process.WaitForExit();
            return process.ExitCode;
        }
        catch (Exception ex)
        {
            Log(Err, $"could not start process: {ex.Message}\n");
            return -1;
        }
    }
}
