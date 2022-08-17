using System;
using System.IO;
using System.Threading.Tasks;

namespace DotnetCountersUi.Native;

public class LinuxCommandLineArgsProvider : ICommandLineArgsProvider
{
    public async Task<string> GetCommandLineArgs(int pid)
    {
        try
        {
            var path = $"/proc/{pid}/cmdline";

            return await File.ReadAllTextAsync(path);
        }
        catch (Exception e)
        {
            await Console.Error.WriteLineAsync(e.ToString());

            return string.Empty;
        }
    }
}