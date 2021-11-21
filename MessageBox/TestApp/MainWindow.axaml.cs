using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using ThingLing.Controls;

namespace TestApp
{
    public class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private async void Button_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            var mb = await MessageBox.ShowAsync("Hello world, this message box is working fine", "Hello title", MessageBoxButton.Ok, MessageBoxImage.Warning, MessageBoxResult.Ok);
            this.FindControl<TextBlock>("result").Text = mb.ToString();
        }
    }
}