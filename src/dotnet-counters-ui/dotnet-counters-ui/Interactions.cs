using DotnetCountersUi.ViewModels;
using ReactiveUI;

namespace DotnetCountersUi;

public class Interactions
{
    public static Interaction<CounterGraphViewModel, CounterDescriptorViewModel?> ShowAddCounterDialog { get; } = new();
    public static Interaction<MainWindowViewModel, bool> ShowDeleteGraphDialog { get; } = new();
}