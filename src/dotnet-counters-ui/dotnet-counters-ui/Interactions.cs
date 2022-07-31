using DotnetCountersUi.ViewModels;
using ReactiveUI;

namespace DotnetCountersUi;

public class Interactions
{
    public static Interaction<CounterGraphViewModel, CounterDescriptorViewModel> ShowAddCounterDialog { get; } = new();
}