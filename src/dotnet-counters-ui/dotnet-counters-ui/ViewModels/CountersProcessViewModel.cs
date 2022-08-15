using System.Diagnostics;
using ReactiveUI;

namespace DotnetCountersUi.ViewModels;

public class CountersProcessViewModel : ReactiveObject
{
    public int Pid { get; }
    public string Name { get; }
    public string Arguments { get; }

    public CountersProcessViewModel(Process p, string args)
    {
        Pid = p.Id;
        Name = p.ProcessName;
        Arguments = args;
    }
}