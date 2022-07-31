using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Avalonia.Threading;
using DotnetCountersUi.Counters;
using DotnetCountersUi.Extensions;
using DynamicData;
using OxyPlot;
using OxyPlot.Axes;
using ReactiveUI;
using Splat;
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

    public CounterGraphViewModel(IDataRouter? router = null)
    {
        Counters = new ReadOnlyObservableCollection<AddedCounterViewModel>(_counters);

        Model = new PlotModel { Title = "My new graph" };
        
        var dateAxis = new DateTimeAxis { Position = AxisPosition.Bottom, StringFormat = "HH:mm:ss" };
        
        Model.Axes.Add(dateAxis);

        AddCounter = ReactiveCommand.CreateFromTask(AddCounterAsync);
    }

    private async Task AddCounterAsync()
    {
        var (name, type) = await Interactions.ShowAddCounterDialog.Handle(this);

        var counter = (ICounter)Activator.CreateInstance(type)!;

        var vm = new AddedCounterViewModel(name, counter, true);

        var series = new LineSeries
        {
            Title = name
        };
            
        Model.Series.Add(series);

        IDisposable? subscription = null;

        vm.WhenAnyValue(v => v.Enabled)
            .Subscribe(val =>
            {
                if (!val)
                {
                    subscription?.Dispose();
                }
                else
                {
                    subscription = counter.Data
                        .ObserveOn(AvaloniaScheduler.Instance)
                        .Subscribe(data =>
                        {
                            series.Points.Add(new DataPoint(DateTimeAxis.ToDouble(DateTime.Now), data));
                
                            Model.InvalidatePlot(true);
                        });
                }
            });

        _counters.Add(vm);
    }
}

public class AddedCounterViewModel : ReactiveObject
{
    public AddedCounterViewModel(string name, ICounter instance, bool enabled)
    {
        Name = name;
        Instance = instance;
        Enabled = enabled;
    }

    public string Name { get; }
    public ICounter Instance { get; }

    public bool Enabled
    {
        get => _enabled;
        set => this.RaiseAndSetIfChanged(ref _enabled, value);
    }

    private bool _enabled;
}