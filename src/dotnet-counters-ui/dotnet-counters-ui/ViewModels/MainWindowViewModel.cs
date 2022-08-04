using System.Collections.ObjectModel;
using System.Reactive;
using DotnetCountersUi.Extensions;
using ReactiveUI;
using Splat;

namespace DotnetCountersUi.ViewModels;

public class MainWindowViewModel : ReactiveObject
{
    public ObservableCollection<CounterGraphViewModel> Graphs { get; }
    
    public ReactiveCommand<Unit, Unit> AddNewGraph { get; }

    private readonly IDataRouter _dataRouter;
    
    public MainWindowViewModel(IDataRouter? dataRouter = null)
    {
        _dataRouter = dataRouter ?? Locator.Current.GetRequiredService<IDataRouter>();

        Graphs = new ObservableCollection<CounterGraphViewModel>();

        AddNewGraph = ReactiveCommand.Create(() => Graphs.Add(new CounterGraphViewModel()));
    }

    public void AttachRouter(int remotePid)
    {
        _dataRouter.Start(remotePid);
    }
}