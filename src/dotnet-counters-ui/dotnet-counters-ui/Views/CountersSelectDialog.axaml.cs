using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using DotnetCountersUi.ViewModels;

namespace DotnetCountersUi.Views
{
    public partial class CountersSelectDialog : Window
    {
        public CountersSelectDialog()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            DataContext = new CountersSelectViewModel()
            {
                CloseAction = str => Close(str)
            };
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
