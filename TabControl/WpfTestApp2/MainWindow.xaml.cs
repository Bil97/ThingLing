using System.Windows;
using System.Windows.Controls;

using TabItem = ThingLing.Controls.TabItem;

namespace WpfTestApp2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private int i = 0;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var tabItem = new TabItem
            {
                Header = $"Hello RichTextBox {++i}",
                Content = new TextBox { Text = $"Helloo {i}", TextWrapping = TextWrapping.Wrap },
                ToolTip = $"RichTextBox {i}"
            };
            _tabControl.Add(tabItem);
        }
    }
}
