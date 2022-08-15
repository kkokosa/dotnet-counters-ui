using System;

namespace DotnetCountersUi.Native;

public class DummyCommandLineArgsProvider : ICommandLineArgsProvider
{
    public bool TryGetCommandLineArgs(int pid, out string[] args)
    {
        args = Array.Empty<string>();
        return true;
    }
}