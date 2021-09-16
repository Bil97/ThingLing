using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media.Imaging;
using System;
using System.Collections.Generic;
using System.IO;

namespace ColorFinder.AvaloniaUI
{
    public class MainWindow : Window
    {
        internal Button? LoadImageButton;
        internal Button? TakeScreenshotButton;
        internal Button? SelectFromScreenButton;
        internal Rectangle? ColorPreviewRectangle;
        internal Button? CopyHexToClipBoardButton;
        internal Button? CopyRGBToClipBoardButton;
        internal Button? ClearButton;
        internal TextBox? HexColorTextBox;
        internal TextBox? RGBColorTextBox;
        internal Image? LoadedImage;

        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);

            LoadImageButton = this.FindControl<Button>(nameof(LoadImageButton));
            TakeScreenshotButton = this.FindControl<Button>(nameof(TakeScreenshotButton));
            SelectFromScreenButton = this.FindControl<Button>(nameof(SelectFromScreenButton));
            CopyHexToClipBoardButton = this.FindControl<Button>(nameof(CopyHexToClipBoardButton));
            CopyRGBToClipBoardButton = this.FindControl<Button>(nameof(CopyRGBToClipBoardButton));
            ClearButton = this.FindControl<Button>(nameof(ClearButton));
            ColorPreviewRectangle = this.FindControl<Rectangle>(nameof(ColorPreviewRectangle));
            HexColorTextBox = this.FindControl<TextBox>(nameof(HexColorTextBox));
            RGBColorTextBox = this.FindControl<TextBox>(nameof(RGBColorTextBox));
            LoadedImage = this.FindControl<Image>(nameof(LoadedImage));
        }

        private async void LoadImageButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new();
            openFileDialog.Directory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            openFileDialog.Filters = new List<FileDialogFilter>
            {
                new FileDialogFilter
                {
                    Name="Image files",
                    Extensions=new List<string> { "png","jpeg","jpg","bitmap","bmp","gif","ico","tiff","tif"}
                },
                new FileDialogFilter
                {
                    Name="png",
                    Extensions=new List<string> { "png" }
                },
                new FileDialogFilter
                {
                    Name="jgeg",
                    Extensions=new List<string> { "jpeg","jpg" }
                },
                new FileDialogFilter
                {
                    Name="bitmap",
                    Extensions=new List<string> { "bitmap","bmp" }
                },
                new FileDialogFilter
                {
                    Name="gif",
                    Extensions=new List<string> { "gif" }
                },
                new FileDialogFilter
                {
                    Name="ico",
                    Extensions=new List<string> { "ico" }
                },
                new FileDialogFilter
                {
                    Name="tiff",
                    Extensions=new List<string> { "tiff","tif" }
                }
            };

            var inputFile = await openFileDialog.ShowAsync(this);
            if (inputFile.Length > 0)
            {
                var bitmap = new Bitmap(inputFile[0]);
                LoadedImage.Source = bitmap;
            }
            // User cancelled operation
            return;
        }

        private void TakeScreenshotButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SelectFromScreenButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CopyHexToClipBoardButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Clipboard.SetTextAsync(HexColorTextBox.Text);
        }

        private void CopyRGBToClipBoardButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Clipboard.SetTextAsync(RGBColorTextBox.Text);
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            LoadedImage.Source = null;
            HexColorTextBox.Text = string.Empty;
            RGBColorTextBox.Text = string.Empty;
        }

        private void LoadedImage_PointerEnter(object sender, PointerEventArgs e)
        {

        }

        private void LoadedImage_PointerLeave(object sender, PointerEventArgs e)
        {

        }

        private void LoadedImage_PointerMoved(object sender, PointerEventArgs e)
        {

        }

        private void LoadedImage_PointerWheelChanged(object sender, PointerWheelEventArgs e)
        {

        }
    }
}
