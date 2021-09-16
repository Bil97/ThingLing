using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using ThingLing.Avalonia.Controls;

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

        private async void Button_Click(object? sender, global::Avalonia.Interactivity.RoutedEventArgs e)
        {
            var mb = await MessageBox.ShowAsync(this, "Hello world, this message box is working fine", "Hello title", MessageBoxButton.Ok, MessageBoxImage.Warning, MessageBoxResult.Ok);
            this.FindControl<TextBlock>("result").Text = mb.ToString();
        }
    }
}