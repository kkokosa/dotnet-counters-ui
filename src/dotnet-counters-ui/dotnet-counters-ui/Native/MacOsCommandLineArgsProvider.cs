using System.Diagnostics;
using System.Threading.Tasks;

namespace DotnetCountersUi.Native;

public class MacOsCommandLineArgsProvider : ICommandLineArgsProvider
{
    public async Task<string> GetCommandLineArgs(int pid)
    {
        var startInfo = new ProcessStartInfo
        {
            FileName = "/bin/bash",
            Arguments = $"-c \"/bin/ps -p {pid} -o args=\"",
            RedirectStandardOutput = true
        };

        using var process = Process.Start(startInfo);
        await process!.WaitForExitAsync();

        return (await process.StandardOutput.ReadToEndAsync()).TrimEnd();
    }
}