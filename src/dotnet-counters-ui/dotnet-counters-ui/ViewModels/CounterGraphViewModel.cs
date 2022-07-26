using System;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using Avalonia.Threading;
using DotnetCountersUi.Extensions;
using DynamicData;
using OxyPlot;
using ReactiveUI;
using Splat;

namespace DotnetCountersUi.ViewModels;

public class CounterGraphViewModel : ReactiveObject
{
    public string GraphId
    {
        get => _graphId;
        private set => this.RaiseAndSetIfChanged(ref _graphId, value);
    }

    private string _graphId;

    public ReadOnlyObservableCollection<DataPoint> Data => _data;
    private readonly ReadOnlyObservableCollection<DataPoint> _data;

    private readonly SourceList<DataPoint> _dataSource = new();

    private readonly DataPoint[] _dataBuffer;

    public string Name
    {
        get => _name;
        private set => this.RaiseAndSetIfChanged(ref _name, value);
    }

    private string _name;

    private readonly IDataRouter _router;
    private int _pointX;

    public CounterGraphViewModel(IDataRouter? router = null)
    {
        _router = router ?? Locator.Current.GetRequiredService<IDataRouter>();

        _dataSource
            .Connect()
            .ObserveOn(AvaloniaScheduler.Instance)
            .Bind(out _data)
            .Subscribe();
        
        _dataBuffer = new DataPoint[200];
        
        for (var i = 0; i < _dataBuffer.Length; i++)
        {
            _dataBuffer[i] = new DataPoint(i, 0);
        }

        _pointX = _dataBuffer.Length;
        
        _dataSource.AddRange(_dataBuffer);
    }

    public void Start(string graphId)
    {
        if (GraphId != null)
        {
            throw new InvalidOperationException("The graph has already been started");
        }
        
        Name = _router.Register(graphId, OnNewData).Name;
        GraphId = graphId;
    }

    private void OnNewData(double value)
    {
        Array.ConstrainedCopy(_dataBuffer, 1, _dataBuffer, 0, _dataBuffer.Length - 1);
        _dataBuffer[^1] = new DataPoint(++_pointX, value);

        _dataSource.Edit(state =>
        {
            state.Clear();
            state.AddRange(_dataBuffer);
        });
    }
}