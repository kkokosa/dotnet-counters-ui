using System;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Avalonia.Threading;
using DotnetCountersUi.Counters;
using DotnetCountersUi.Utils;
using OxyPlot;
using OxyPlot.Axes;
using ReactiveUI;
using LineSeries = OxyPlot.Series.LineSeries;

namespace DotnetCountersUi.ViewModels;

public class CounterGraphViewModel : ReactiveObject
{
    public string GraphId
    {
        get => _graphId;
        private set => this.RaiseAndSetIfChanged(ref _graphId, value);
    }

    private string _graphId;

    public string Name
    {
        get => _name;
        private set => this.RaiseAndSetIfChanged(ref _name, value);
    }

    private string _name;

    public ReadOnlyObservableCollection<AddedCounterViewModel> Counters { get; }

    private readonly ObservableCollection<AddedCounterViewModel> _counters = new();

    public PlotModel Model { get; }
    
    public ReactiveCommand<Unit, Unit> AddCounter { get; }
    
    public ReactiveCommand<AddedCounterViewModel, Unit> RemoveCounter { get; }

    public CounterGraphViewModel(IDataRouter? router = null)
    {
        Counters = new ReadOnlyObservableCollection<AddedCounterViewModel>(_counters);

        Model = new PlotModel { Title = "My new graph" };
        
        var dateAxis = new DateTimeAxis { Position = AxisPosition.Bottom, StringFormat = "HH:mm:ss" };
        
        Model.Axes.Add(dateAxis);

        AddCounter = ReactiveCommand.CreateFromTask(AddCounterAsync);
        
        RemoveCounter = ReactiveCommand.Create((AddedCounterViewModel vm) =>
        {
            vm.Subscription.Dispose();

            Model.Series.Remove(vm.Series);
            
            _counters.Remove(vm);
        });
    }

    private async Task AddCounterAsync()
    {
        var (name, type) = await Interactions.ShowAddCounterDialog.Handle(this);

        var counter = (ICounter)Activator.CreateInstance(type)!;

        var series = new LineSeries
        {
            Title = name,
            Color = OxyColorUtils.GetRandomOxyColor(),
        };
            
        Model.Series.Add(series);
        
        var subscription = counter.Data
            .ObserveOn(AvaloniaScheduler.Instance)
            .Subscribe(data =>
            {
                series.Points.Add(new DataPoint(DateTimeAxis.ToDouble(DateTime.Now), data));
                
                Model.InvalidatePlot(true);
            });
        
        var vm = new AddedCounterViewModel(counter, series, subscription);

        _counters.Add(vm);
    }
}

public class AddedCounterViewModel : ReactiveObject
{
    public ICounter Instance { get; }
    
    public LineSeries Series { get; }
    
    public IDisposable Subscription { get; }

    public AddedCounterViewModel(ICounter instance, LineSeries series, IDisposable subscription)
    {
        Instance = instance;
        Series = series;
        Subscription = subscription;
    }
}