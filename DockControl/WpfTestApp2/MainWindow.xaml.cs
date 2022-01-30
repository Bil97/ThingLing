using System.Windows;
using System.Windows.Controls;

namespace WpfTestApp2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DockControl win = new DockControl();
        public MainWindow()
        {
            InitializeComponent();

            //win.AddWindow("Text Editor", "Text", new TextBlock());
            //win.AddWindow("Text Editor", "Text2", new RichTextBox(), 50, 25);
            //win.AddWindow("Text Editor", "Text3", new RichTextBox(), 100, 50);
            //win.AddWindow("Text Editor", "Text4", null, 150, 150);
            //win.AddDocument("Text Window", "th path 2", new RichTextBox());
            //win.AddDocument("Text Window", "th path 32", new RichTextBox());
            //win.AddDocument("Text Window12", "th path 122", new RichTextBox());
            win.AddDocument("Text Window13", "th path 132", new RichTextBox());
            MainGrid.Children.Add(win);
        }

        private int _i = 0;
        private int _j = 0;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            win.AddWindow($"Text Editor {++_j}", $"Path to {_j}", new TextBlock());
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            win.AddDocument($"Text Window{++_i}", $"Path to document{_i}", new RichTextBox());
        }
    }
}
