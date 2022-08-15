using System;
using System.IO;

namespace DotnetCountersUi.Native;

public class LinuxCommandLineArgsProvider : ICommandLineArgsProvider
{
    public bool TryGetCommandLineArgs(int pid, out string[] args)
    {
        args = Array.Empty<string>();

        var path = $"/proc/{pid}/cmdline";

        if (!File.Exists(path)) return false;

        var input = File.ReadAllText(path);

        args = input.Split('\x00');

        return true;
    }
}