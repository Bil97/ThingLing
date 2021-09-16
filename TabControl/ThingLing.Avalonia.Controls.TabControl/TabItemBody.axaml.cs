using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace ThingLing.Controls
{
    public class TabItemBody : UserControl
    {
        public TabItemHeader TabItemHeader;
        public Decorator ContentPanel;

        public TabItemBody()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);

            TabItemHeader = this.FindControl<TabItemHeader>(nameof(TabItemHeader));
            ContentPanel = this.FindControl<Decorator>(nameof(ContentPanel));
        }
    }
}
