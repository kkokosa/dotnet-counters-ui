using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace DotnetCountersUi.Views;

public partial class GraphToolbox : UserControl
{
    public ICommand? DeleteCommand
    {
        get => _deleteCommand;
        set => SetAndRaise(DeleteCommandProperty, ref _deleteCommand, value);
    }
    
    public static readonly DirectProperty<GraphToolbox, ICommand?> DeleteCommandProperty =
        AvaloniaProperty.RegisterDirect<GraphToolbox, ICommand?>(nameof(DeleteCommand),
            control => control.DeleteCommand, (button, command) => button.DeleteCommand = command, enableDataValidation: true);

    private ICommand? _deleteCommand;

    public object DeleteCommandParameter
    {
        get => _deleteCommandParameter;
        set => SetAndRaise(DeleteCommandParameterProperty, ref _deleteCommandParameter, value);
    }
    
    public static readonly StyledProperty<object> DeleteCommandParameterProperty =
        AvaloniaProperty.Register<GraphToolbox, object>(nameof(DeleteCommandParameter));

    private object _deleteCommandParameter;

    public GraphToolbox()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}