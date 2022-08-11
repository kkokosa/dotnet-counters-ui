using System;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Avalonia.Threading;
using DotnetCountersUi.Counters;
using DotnetCountersUi.Extensions;
using DotnetCountersUi.Utils;
using OxyPlot;
using OxyPlot.Axes;
using ReactiveUI;
using Splat;
using LineSeries = OxyPlot.Series.LineSeries;

namespace DotnetCountersUi.ViewModels;

public class CounterGraphViewModel : ReactiveObject
{
    public ReadOnlyObservableCollection<AddedCounterViewModel> Counters { get; }

    private readonly ObservableCollection<AddedCounterViewModel> _counters = new();

    public PlotModel Model { get; }
    
    public ReactiveCommand<Unit, Unit> AddCounter { get; }
    
    public ReactiveCommand<AddedCounterViewModel, Unit> RemoveCounter { get; }

    private readonly IDataRouter _dataRouter;

    public CounterGraphViewModel(IDataRouter? router = null)
    {
        _dataRouter = router ?? Locator.Current.GetRequiredService<IDataRouter>();
        
        Counters = new ReadOnlyObservableCollection<AddedCounterViewModel>(_counters);

        Model = new PlotModel { Title = "(unnamed)" };
        
        var dateAxis = new DateTimeAxis { Position = AxisPosition.Bottom, StringFormat = "HH:mm:ss" };
        
        Model.Axes.Add(dateAxis);

        AddCounter = ReactiveCommand.CreateFromTask(AddCounterAsync);
        
        RemoveCounter = ReactiveCommand.Create((AddedCounterViewModel vm) =>
        {
            vm.Dispose();

            Model.Series.Remove(vm.Series);
            
            Model.InvalidatePlot(false);

            _counters.Remove(vm);
        });
    }

    private async Task AddCounterAsync()
    {
        var counterDescriptor = await Interactions.ShowAddCounterDialog.Handle(this);

        if (counterDescriptor == null) return;

        var (name, type) = counterDescriptor;

        var counter = (ICounter)Activator.CreateInstance(type, _dataRouter)!;

        var series = new LineSeries
        {
            Title = name,
            Color = OxyColorUtils.GetNextQualitativeColor(),
        };
            
        Model.Series.Add(series);
        
        var vm = new AddedCounterViewModel(counter, series);

        _counters.Add(vm);
    }
}

public class AddedCounterViewModel : ReactiveObject, IDisposable
{
    public ICounter Instance { get; }
    
    public LineSeries Series { get; }
    
    public float Scale
    {
        get => _scale;
        set => this.RaiseAndSetIfChanged(ref _scale, value);
    }

    private float _scale = 1;

    private readonly CompositeDisposable _disposable = new();

    private readonly ObservableCollection<DataPoint> _points = new();

    public AddedCounterViewModel(ICounter instance, LineSeries series)
    {
        Instance = instance;
        Series = series;

        Series.ItemsSource = _points;

        instance.Data
            .ObserveOn(AvaloniaScheduler.Instance)
            .Subscribe(data =>
            {
                _points.Add(new DataPoint(DateTimeAxis.ToDouble(DateTime.Now), data * Scale));

                series.PlotModel.InvalidatePlot(true);
            })
            .DisposeWith(_disposable);

        this.WhenAnyValue(vm => vm.Scale)
            .Subscribe(scale =>
            {
                series.Mapping = p =>
                {
                    var dp = (DataPoint) p;
                    return new DataPoint(dp.X, dp.Y * scale);
                };

                series.PlotModel.InvalidatePlot(false);
            })
            .DisposeWith(_disposable);
    }

    public void Dispose()
    {
        _disposable.Dispose();
    }
}