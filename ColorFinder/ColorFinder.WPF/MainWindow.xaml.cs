using Microsoft.Win32;
using System;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace ColorFinder.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public object FileAccessMode { get; private set; }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void LoadImageButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            openFileDialog.Filter = "Image files|*.png;*.jpeg;*.jpg;*.bitmap;*.bmp;*.gif;*.ico;*.tiff;*.tif|" +
                "png (*.png)|*.png|jpeg (*.jpeg;*.jpg)|*.jpeg;*.jpg|bitmap (*.bitmap;*.bmp)" +
                "|*.bitmap;*.bmp|gif (*.gif)|*.gif|ico (*.ico)|*.ico|tiff (*.tiff;*tif)|*.tiff;*.tif";
            ;
            var inputFile = openFileDialog.ShowDialog();
            if (inputFile != true)
            {
                // User cancelled operation
                return;
            }

            var bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.UriSource = new Uri(openFileDialog.FileName);
            bitmapImage.EndInit();
            LoadedImage.Source = bitmapImage;

        }

        private void TakeScreenshotButton_Click(object sender, RoutedEventArgs e)
        {
//            LoadedImage.Source = bmp;
        }

        public void CaptureScreen(double x, double y, double width, double height)
        {

            int ix, iy, iw, ih;
            ix = Convert.ToInt32(x);
            iy = Convert.ToInt32(y);
            iw = Convert.ToInt32(width);
            ih = Convert.ToInt32(height);
            Bitmap image = new Bitmap(iw, ih,
                   System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            Graphics g = Graphics.FromImage(image);
            g.CopyFromScreen(ix, iy, ix, iy,
                     new System.Drawing.Size(iw, ih),
                     CopyPixelOperation.SourceCopy);
        }

        private void SelectFromScreenButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CopyHexToClipBoardButton_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(HexColorTextBox.Text);
        }

        private void CopyRGBToClipBoardButton_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(RGBColorTextBox.Text);
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            LoadedImage.Source = null;
            HexColorTextBox.Text = string.Empty;
            RGBColorTextBox.Text = string.Empty;
        }

        private void LoadedImage_MouseEnter(object sender, MouseEventArgs e)
        {

        }

        private void LoadedImage_MouseLeave(object sender, MouseEventArgs e)
        {

        }

        private void LoadedImage_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void LoadedImage_MouseWheel(object sender, MouseWheelEventArgs e)
        {

        }

    }
}
