using System;
using Windows.ApplicationModel.DataTransfer;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace RandomGuid.UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
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
            DataPackage content = new DataPackage();
            content.SetText(GuidTextBox.Text);
            Clipboard.SetContent(content);
        }

        private void ClearGuidTextBoxButton_Click(object sender, RoutedEventArgs e)
        {
            GuidTextBox.Text = string.Empty;
        }
    }
}
