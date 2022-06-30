using System.Collections.Generic;
using System.Linq;
using OxyPlot;
using ReactiveUI;
using Splat;

namespace DotnetCountersUi.ViewModels;

public class CounterGraphViewModel : ReactiveObject
{
  private readonly IDataRouter _router;
  private readonly List<DataPoint> _points;
  private double _pointX;

  private List<DataPoint>? _p;

  public List<DataPoint>? Points
  {
    get => _p;
    set => this.RaiseAndSetIfChanged(ref _p, value);
  }

  public CounterGraphViewModel()
  {
    _router = Locator.Current.GetService<IDataRouter>()!
      ;
    _points = Enumerable
      .Range(0, 200)
      .Select((_, i) => new DataPoint(i, 0))
      .ToList();

    _pointX = _points.Count;
  }

  public void Register(string name)
  {
    _router.Register(name, OnNewData);
  }

  private void OnNewData(double value)
  {
    _points.RemoveAt(0);
    _points.Add(new DataPoint(_pointX++, value));

    Points = null!;
    Points = _points;
  }
}