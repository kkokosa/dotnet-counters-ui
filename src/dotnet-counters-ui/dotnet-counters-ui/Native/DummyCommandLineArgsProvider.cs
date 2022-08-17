using System.Threading.Tasks;

namespace DotnetCountersUi.Native;

public class DummyCommandLineArgsProvider : ICommandLineArgsProvider
{
    public Task<string> GetCommandLineArgs(int pid)
    {
        return Task.FromResult(string.Empty);
    }
}