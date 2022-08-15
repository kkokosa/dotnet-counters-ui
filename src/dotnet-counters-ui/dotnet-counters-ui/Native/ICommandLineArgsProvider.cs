using System.Threading.Tasks;

namespace DotnetCountersUi.Native;

public interface ICommandLineArgsProvider
{
    Task<string> GetCommandLineArgs(int pid);
}