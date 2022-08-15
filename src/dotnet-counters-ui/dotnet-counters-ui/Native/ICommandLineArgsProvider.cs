namespace DotnetCountersUi.Native;

public interface ICommandLineArgsProvider
{
    bool TryGetCommandLineArgs(int pid, out string[] args);
}