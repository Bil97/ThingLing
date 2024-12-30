using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media;

namespace ThingLing.Controls
{
    public partial class MainWindow : Window
    {
        //public static MainWindow Window { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            // Window = this;
            tab.NewTabItemButtonClicked+= NewTabItemButton_Clicked;
        }

        private void NewTabItemButton_Clicked()
        {
            Click();
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
