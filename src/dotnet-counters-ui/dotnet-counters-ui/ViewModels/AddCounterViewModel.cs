using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reflection;
using Avalonia.Controls;
using DotnetCountersUi.Counters;
using ReactiveUI;

namespace DotnetCountersUi.ViewModels;

public class AddCounterViewModel : ReactiveObject
{
    public IEnumerable<CounterDescriptorViewModel> AvailableCounters { get; }

    public CounterDescriptorViewModel? Selected
    {
        get => _selected;
        set => this.RaiseAndSetIfChanged(ref _selected, value);
    }

    private CounterDescriptorViewModel? _selected;
    
    public ReactiveCommand<Window, Unit> CloseDialog { get; }

    public AddCounterViewModel()
    {
        AvailableCounters = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract && t.IsAssignableTo(typeof(ICounter)))
            .Select(t =>
            {
                var attribute = t.GetCustomAttribute<CounterAttribute>()
                                ?? throw new NullReferenceException(
                                    $"Every counter in the assembly must be described with {nameof(CounterAttribute)} attribute");

                return new CounterDescriptorViewModel(attribute.Name, t);
            });

        CloseDialog = ReactiveCommand.Create<Window>(window => window.Close(Selected));
    }
}

public record CounterDescriptorViewModel(string Name, Type Type);