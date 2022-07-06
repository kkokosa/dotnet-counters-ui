using System;
using System.Collections.Generic;

namespace DotnetCountersUi;

public class SeriesMetadata
{
  public string Id { get; init; }
  public string Name { get; init; }
  public string? StackGroup { get; init; }
  public List<object> X { get; init; } = new List<object>();
  public List<object> Y { get; init; } = new List<object>();
  public Func<IDictionary<string, object>, bool> Filter { get; init; }
  public Func<IDictionary<string,object>, double> Selector { get; init; }
}