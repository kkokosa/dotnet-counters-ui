using System.Collections.ObjectModel;
using System.Reactive;
using Avalonia;
using Avalonia.Controls;
using DotnetCountersUi.Extensions;
using ReactiveUI;
using Splat;

namespace DotnetCountersUi.ViewModels;

public class MainWindowViewModel : ReactiveObject
{
    public ObservableCollection<CounterGraphViewModel> Graphs { get; }
    
    public ReactiveCommand<Unit, Unit> AddNewGraph { get; }
    
    public ReactiveCommand<CounterGraphViewModel, Unit> DeleteGraph { get; }

    public ReactiveCommand<Window, Unit> CloseWindow { get; }

    private readonly IDataRouter _dataRouter;
    
    public MainWindowViewModel(IDataRouter? dataRouter = null)
    {
        _dataRouter = dataRouter ?? Locator.Current.GetRequiredService<IDataRouter>();

        Graphs = new ObservableCollection<CounterGraphViewModel>();

        AddNewGraph = ReactiveCommand.Create(() => Graphs.Add(new CounterGraphViewModel()));

        DeleteGraph = ReactiveCommand.Create((CounterGraphViewModel vm) =>
        {
            Graphs.Remove(vm);
            vm.Dispose();
        });

        CloseWindow = ReactiveCommand.Create<Window>(window => window.Close());
    }

    public void AttachRouter(int remotePid)
    {
        _dataRouter.Start(remotePid);
    }
}