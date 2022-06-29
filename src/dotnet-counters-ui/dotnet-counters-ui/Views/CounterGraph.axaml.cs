using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using DotnetCountersUi.ViewModels;


namespace DotnetCountersUi.Views;

public partial class CounterGraph : ReactiveUserControl<UserControl>
{
  public string GraphId { get; set; } = null!;

  public CounterGraph()
  {
    InitializeComponent();

  }


  private void InitializeComponent()
  {
    AvaloniaXamlLoader.Load(this);
  }
    
  protected override void OnInitialized()
  {
    var dc = (CounterGraphViewModel?)DataContext;
    dc!.Register(GraphId);
    var label = this.Find<TextBlock>("Label");
    label.Text = GraphId;
  }
}