using System;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Avalonia.Data;
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
        set
        {
            if (value <= 0)
                throw new DataValidationException($"{nameof(Scale)} must be positive");

            this.RaiseAndSetIfChanged(ref _scale, value);
        }
    }

    private float _scale = 1;

    private readonly CompositeDisposable _disposable = new();

    private readonly ObservableCollection<MeasurePoint> _points = new();

    public AddedCounterViewModel(ICounter instance, LineSeries series)
    {
        Instance = instance;
        Series = series;

        Series.ItemsSource = _points;
        Series.TrackerFormatString = "X: {Timestamp:T}\nY: {Y}";

        instance.Data
            .ObserveOn(AvaloniaScheduler.Instance)
            .Subscribe(data =>
            {
                var point = new MeasurePoint(DateTime.Now, data, _scale);
                _points.Add(point);

                series.PlotModel.InvalidatePlot(true);
            })
            .DisposeWith(_disposable);

        this.WhenAnyValue(vm => vm.Scale)
            .Subscribe(_ =>
            {
                _points.Clear();
            })
            .DisposeWith(_disposable);
    }

    public void Dispose()
    {
        _disposable.Dispose();
    }
}

public class MeasurePoint : IDataPointProvider
{
    public MeasurePoint(DateTime timestamp, double y, float scale)
    {
        Timestamp = timestamp;
        Y = y;
        Scale = scale;
    }

    public DateTime Timestamp { get; }
    public double Y { get; }
    public float Scale { get; }

    public DataPoint GetDataPoint()
    {
        return new DataPoint(DateTimeAxis.ToDouble(Timestamp), Y * Scale);
    }
}
