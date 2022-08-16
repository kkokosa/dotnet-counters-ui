using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace DotnetCountersUi.Native;

public class WindowsCommandLineArgsProvider : ICommandLineArgsProvider
{
    public async Task<string> GetCommandLineArgs(int pid)
    {
        var startInfo = new ProcessStartInfo
        {
            FileName = "wmic",
            Arguments = $"process where \"processid='{pid}'\" get commandline",
            RedirectStandardOutput = true
        };

        using var process = Process.Start(startInfo);
        await process!.WaitForExitAsync();

        var output = await process.StandardOutput.ReadToEndAsync();

        return output.Split(Environment.NewLine)[1];
    }
}