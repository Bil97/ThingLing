using System;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ColorFinder.Mobile
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void LoadImageButton_Clicked(object sender, EventArgs e)
        {
            /////
        }

        private void TakeScreenshotButton_Clicked(object sender, EventArgs e)
        {

        }

        private void SelectFromScreenButton_Clicked(object sender, EventArgs e)
        {

        }

        private void CopyHexToClipBoardButton_Clicked(object sender, EventArgs e)
        {
            Clipboard.SetTextAsync(HexColorEntry.Text);
        }

        private void CopyRGBToClipBoardButton_Clicked(object sender, EventArgs e)
        {
            Clipboard.SetTextAsync(HexColorEntry.Text);

        }

        private void ClearButton_Clicked(object sender, EventArgs e)
        {
            LoadedImage.Source = null;
            HexColorEntry.Text = string.Empty;
            RGBColorEntry.Text = string.Empty;
        }
    }
}
