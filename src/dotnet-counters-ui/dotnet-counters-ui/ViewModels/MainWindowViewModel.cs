using System.Collections.ObjectModel;
using DotnetCountersUi.Extensions;
using ReactiveUI;
using Splat;

namespace DotnetCountersUi.ViewModels;

public class MainWindowViewModel : ReactiveObject
{
    public ObservableCollection<CounterGraphViewModel> Counters { get; }

    private readonly IDataRouter _dataRouter;
    
    public MainWindowViewModel(IDataRouter? dataRouter = null)
    {
        _dataRouter = dataRouter ?? Locator.Current.GetRequiredService<IDataRouter>();

        Counters = new ObservableCollection<CounterGraphViewModel>();
    }

    public void AddAndStartGraph(string graphId)
    {
        var vm = new CounterGraphViewModel(_dataRouter);
        Counters.Add(vm);
    }

    public void AttachRouter(int remotePid)
    {
        _dataRouter.Start(remotePid);
    }
}