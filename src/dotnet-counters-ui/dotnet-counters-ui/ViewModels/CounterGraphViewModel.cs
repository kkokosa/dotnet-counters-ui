using System;
using System.Linq;
using OxyPlot;
using ReactiveUI;
using Splat;

namespace DotnetCountersUi.ViewModels;

public class CounterGraphViewModel : ReactiveObject
{
  private readonly IDataRouter _router;
  private readonly DataPoint[] _points;
  private double _pointX;

  private DataPoint[]? _p;

  public DataPoint[]? Points
  {
    get => _p;
    set => this.RaiseAndSetIfChanged(ref _p, value);
  }

  public CounterGraphViewModel()
  {
    _router = Locator.Current.GetService<IDataRouter>()!;

    _points = Enumerable
      .Range(0, 200)
      .Select((_, i) => new DataPoint(i, 0))
      .ToArray();

    _pointX = _points.Length;
  }

  public void Register(string name)
  {
    _router.Register(name, OnNewData);
  }

  private void OnNewData(double value)
  {
    Array.ConstrainedCopy(_points, 1, _points, 0, _points.Length - 1);
    _points[^1] = new DataPoint(_pointX++, value);

    Points = null!;
    Points = _points;
  }
}