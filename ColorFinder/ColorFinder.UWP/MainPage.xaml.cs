using System;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Shapes;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace ColorFinder.UWP
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

        private async void LoadImageButton_Click(object sender, RoutedEventArgs e)
        {
            FileOpenPicker fileOpenPicker = new FileOpenPicker();
            fileOpenPicker.ViewMode = PickerViewMode.Thumbnail;
            fileOpenPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            fileOpenPicker.FileTypeFilter.Add(".png");
            fileOpenPicker.FileTypeFilter.Add(".jpg");
            fileOpenPicker.FileTypeFilter.Add(".jpeg");
            fileOpenPicker.FileTypeFilter.Add(".bmp");
            fileOpenPicker.FileTypeFilter.Add(".gif");
            fileOpenPicker.FileTypeFilter.Add(".ico");
            fileOpenPicker.FileTypeFilter.Add(".tif");
            fileOpenPicker.FileTypeFilter.Add(".tiff");

            var inputFile = await fileOpenPicker.PickSingleFileAsync();
            if (inputFile == null)
            {
                // User cancelled operation
                return;
            }

            SoftwareBitmap softwareBitmap;
            using (var stream = await inputFile.OpenAsync(FileAccessMode.Read))
            {
                // Create the decoder from the stream
                BitmapDecoder bitmapDecoder = await BitmapDecoder.CreateAsync(stream);
                //Get the software representation of the file
                softwareBitmap = await bitmapDecoder.GetSoftwareBitmapAsync();
            }

            if (softwareBitmap.BitmapPixelFormat != BitmapPixelFormat.Bgra8 ||
                softwareBitmap.BitmapAlphaMode == BitmapAlphaMode.Straight)
            {
                softwareBitmap = SoftwareBitmap.Convert(softwareBitmap, BitmapPixelFormat.Bgra8, BitmapAlphaMode.Premultiplied);
            }
            var source = new SoftwareBitmapSource();
            await source.SetBitmapAsync(softwareBitmap);

            LoadedImage.Source = source;
        }

        private void TakeScreenshotButton_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void SelectFromScreenButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CopyHexToClipBoardButton_Click(object sender, RoutedEventArgs e)
        {
            DataPackage data = new DataPackage();
            data.SetText(HexColorTextBox.Text);
            Clipboard.SetContent(data);
        }

        private void CopyRGBToClipBoardButton_Click(object sender, RoutedEventArgs e)
        {
            DataPackage data = new DataPackage();
            data.SetText(RGBColorTextBox.Text);
            Clipboard.SetContent(data);
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            LoadedImage.Source = null;
            HexColorTextBox.Text = string.Empty;
            RGBColorTextBox.Text = string.Empty;
        }

        private void LoadedImage_PointerEntered(object sender, PointerRoutedEventArgs e)
        {

        }

        private void LoadedImage_PointerExited(object sender, PointerRoutedEventArgs e)
        {

        }

        private void LoadedImage_PointerMoved(object sender, PointerRoutedEventArgs e)
        {

        }

        private void LoadedImage_PointerWheelChanged(object sender, PointerRoutedEventArgs e)
        {

        }
    }
}
