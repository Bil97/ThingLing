using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using System;

namespace RandomGuid.AvaloniaUI
{
    public class MainWindow : Window
    {
        TextBox GuidTextBox;
        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
          

            GuidTextBox = this.FindControl<TextBox>(nameof(GuidTextBox));
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void NewGuidButton_Click(object sender, RoutedEventArgs e)
        {
            GuidTextBox.Text = Guid.NewGuid().ToString();
        }

        private void AppendGuidButton_Click(object sender, RoutedEventArgs e)
        {
            GuidTextBox.Text += Guid.NewGuid().ToString();
        }

        private void CopyGuidButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Clipboard.SetTextAsync(GuidTextBox.Text);
        }

        private void ClearGuidTextBoxButton_Click(object sender, RoutedEventArgs e)
        {
            GuidTextBox.Text = string.Empty;
        }
    }
}
