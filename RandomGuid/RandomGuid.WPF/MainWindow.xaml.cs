using System;
using System.Windows;

namespace RandomGuid.WPF
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
            Clipboard.SetText(GuidTextBox.Text);
        }

        private void ClearGuidTextBoxButton_Click(object sender, RoutedEventArgs e)
        {
            GuidTextBox.Text = string.Empty;
        }
    }
}
