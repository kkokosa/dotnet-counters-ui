using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
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

    public object? DeleteCommandParameter
    {
        get => _deleteCommandParameter;
        set => SetAndRaise(DeleteCommandParameterProperty, ref _deleteCommandParameter, value);
    }
    
    public static readonly StyledProperty<object?> DeleteCommandParameterProperty =
        AvaloniaProperty.Register<GraphToolbox, object?>(nameof(DeleteCommandParameter));

    private object? _deleteCommandParameter;

    public static readonly DirectProperty<GraphToolbox, string?> TextProperty =
        AvaloniaProperty.RegisterDirect<GraphToolbox, string?>(
            nameof(Text),
            o => o.Text,
            (o, v) => o.Text = v);

    public string? Text
    {
        get => _text;
        set => SetAndRaise(TextProperty, ref _text, value);
    }

    private string? _text;

    private readonly TextBox _renameBox;
    private readonly TextBlock _nameBlock;

    public GraphToolbox()
    {
        InitializeComponent();

        _renameBox = this.FindControl<TextBox>("RenameBox");
        _nameBlock = this.FindControl<TextBlock>("NameBlock");
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private void RenameButton_OnClick(object? sender, RoutedEventArgs e)
    {
        SetRenameMode(true);
    }

    private void RenameBox_OnKeyUp(object? sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter)
        {
           SetRenameMode(false);
        }
    }

    private void RenameBox_OnLostFocus(object? sender, RoutedEventArgs e)
    {
        SetRenameMode(false);
    }

    private void SetRenameMode(bool renameMode)
    {
        _nameBlock.IsVisible = !renameMode;
        _renameBox.IsVisible = renameMode;
    }
}