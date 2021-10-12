using System.Collections;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace DotnetCountersUi
{
    public partial class CounterGraph : UserControl
    {
        public static readonly DirectProperty<ItemsControl, IEnumerable> ItemsProperty =
            AvaloniaProperty.RegisterDirect<ItemsControl, IEnumerable>(
                nameof(Items),
                o => o.Items,
                (o, v) => o.Items = v);

        private IEnumerable _items = new AvaloniaList<object>();

        public IEnumerable Items
        {
            get { return _items; }
            set { SetAndRaise(ItemsProperty, ref _items, value); }
        }

        public CounterGraph()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
