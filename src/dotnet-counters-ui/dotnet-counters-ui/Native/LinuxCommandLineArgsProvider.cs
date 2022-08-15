using System;
using System.IO;
using System.Threading.Tasks;

namespace DotnetCountersUi.Native;

public class LinuxCommandLineArgsProvider : ICommandLineArgsProvider
{
    public async Task<string> GetCommandLineArgs(int pid)
    {
        var path = $"/proc/{pid}/cmdline";

        return await File.ReadAllTextAsync(path);
    }
}