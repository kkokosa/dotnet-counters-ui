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
        get => GetValue(DeleteCommandParameterProperty);
        set => SetValue(DeleteCommandParameterProperty, value);
    }
    
    public static readonly StyledProperty<object?> DeleteCommandParameterProperty =
        AvaloniaProperty.Register<GraphToolbox, object?>(nameof(DeleteCommandParameter));

    //private object? _deleteCommandParameter;

    public static readonly DirectProperty<GraphToolbox, string> TextProperty =
        AvaloniaProperty.RegisterDirect<GraphToolbox, string>(
            nameof(Text),
            o => o.Text,
            (o, v) => o.Text = v);

    public string Text
    {
        get => _text;
        set => SetAndRaise(TextProperty, ref _text, value);
    }

    private string _text = string.Empty;

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
            Text = _renameBox.Text;
        }
        else if (e.Key != Key.Escape)
        {
            return;
        }
        
        SetRenameMode(false);
    }

    private void RenameBox_OnLostFocus(object? sender, RoutedEventArgs e)
    {
        SetRenameMode(false);
    }

    private void SetRenameMode(bool renameMode)
    {
        if (renameMode)
        {
            _renameBox.Focus();
            _renameBox.Text = Text;
            _renameBox.CaretIndex = _renameBox.Text.Length;
            _renameBox.SelectAll();
        }

        _nameBlock.IsVisible = !renameMode;
        _renameBox.IsVisible = renameMode;
    }
}