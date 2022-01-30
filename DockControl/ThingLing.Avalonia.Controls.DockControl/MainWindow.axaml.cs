using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace ThingLing.Controls
{
    public partial class MainWindow : Window
    {
        DockControl win = new DockControl() /*{ Width = 900 }*/;

        public MainWindow()
        {
            InitializeComponent();

            win.AddDocument("Text Window13", "th path 132", new TextBox());
            MainPanel.Children.Add(win);

#if DEBUG
            this.AttachDevTools();
#endif
        }

        #region Controls

        Panel? MainPanel;
        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);

            MainPanel = this.FindControl<Panel>(nameof(MainPanel));
        }
        #endregion

        private int _i = 0;
        private int _j = 0;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            win.AddWindow($"Text Editor {++_j}", $"Path to {_j}", new TextBlock());
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            win.AddDocument($"Text Window{++_i}", $"Path to document{_i}", new TextBox());
        }
    }
}
