using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media;

namespace ThingLing.Controls
{
    public class MainWindow : Window
    {
        private TabControl tab;
        public static MainWindow Window { get; set; }
        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            Window = this;

            tab = this.FindControl<TabControl>("tab");
            tab.NewTabItemButtonClicked+= NewTabItemButton_Clicked;
        }

        private void NewTabItemButton_Clicked()
        {
            Click();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private int _i;
       private void Button_Click(object sender, RoutedEventArgs e)
        {
         Click();   
        }

       void Click()
       {
           var tabItem = new TabItem
           {
               Header = $"Hello TextBox {++_i}",
               Content = new TextBox { Text = $"Hello {_i}", TextWrapping = TextWrapping.Wrap },
               ToolTip = $"RichTextBox {_i}"
           };
           tab.Add(tabItem);
       }

    }
}
