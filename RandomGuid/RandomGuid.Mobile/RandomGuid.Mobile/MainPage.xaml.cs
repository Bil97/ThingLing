using System;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace RandomGuid.Mobile
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void NewGuidButton_Clicked(object sender, EventArgs e)
        {
            GuidEditor.Text = Guid.NewGuid().ToString();
        }

        private void AppendGuidButton_Clicked(object sender, EventArgs e)
        {
            GuidEditor.Text += Guid.NewGuid().ToString();
        }

        private void CopyGuidButton_Clicked(object sender, EventArgs e)
        {
            Clipboard.SetTextAsync(GuidEditor.Text);
        }

        private void ClearGuidEditorButton_Clicked(object sender, EventArgs e)
        {
            GuidEditor.Text = string.Empty;
        }
    }
}
