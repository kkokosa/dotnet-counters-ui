using System.Diagnostics;
using ReactiveUI;

namespace DotnetCountersUi.ViewModels;

public class CountersProcessViewModel : ReactiveObject
{
    public int Pid { get; }
    public string Name { get; }

    public CountersProcessViewModel(Process p)
    {
        Pid = p.Id;
        Name = p.ProcessName;
    }
}