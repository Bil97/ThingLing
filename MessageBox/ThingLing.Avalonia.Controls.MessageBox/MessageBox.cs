using Avalonia;
using Avalonia.Controls;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ThingLing.Avalonia.Controls
{
    /// <summary>
    /// Displays a message box.
    /// </summary>
    public class MessageBox
    {
        private const double UniformThickness = 2;
        private static MainWindow? _window;

        private static void MessageBoxItems(string message, string title = "")
        {
            _window = new MainWindow {MessageTextBlock = {Text = message}, TitleTextBlock = {Text = title}};
        }

        private static IBitmap LoadBitmap(string uri)
        {
            var assets = AvaloniaLocator.Current.GetService<IAssetLoader>();
            return new Bitmap(assets.Open(new Uri(uri)));
        }

        /// <summary>
        ///  Displays a message box that has a message and that returns a result.
        /// </summary>
        /// <param name="owner">The main window that hosts this message box</param>
        /// <param name="message">A System.String that specifies the text to display.</param>
        /// <returns>A ThingLing.Avalonia.Controls.MessageBoxResult value that specifies which message box button is clicked by the user.</returns>
        public static async Task<MessageBoxResult> ShowAsync(Window owner, string message)
        {
            MessageBoxItems(message);
            await _window.ShowDialog(owner);
            return _window.MessageBoxResult;
        }

        /// <summary>
        /// Displays a message box that has a message and title bar caption; and that returns a result.
        /// </summary>
        /// <param name="owner">The main window that hosts this message box</param>
        /// <param name="message">A System.String that specifies the text to display.</param>
        /// <param name="title">A System.String that specifies the title bar caption to display.</param>
        /// <returns>A ThingLing.Avalonia.Controls.MessageBoxResult value that specifies which message box button is clicked by the user.</returns>
        public static async Task<MessageBoxResult> ShowAsync(Window owner, string message, string title)
        {
            MessageBoxItems(message, title);
            await _window.ShowDialog(owner);
            return _window.MessageBoxResult;
        }

        private static void MessageBoxButtonMethod(MessageBoxButton button)
        {
            switch (button)
            {
                case MessageBoxButton.Ok:
                    _window.OkButton.IsVisible = true;
                    break;
                case MessageBoxButton.YesNo:
                    _window.YesButton.IsVisible = true;
                    _window.NoButton.IsVisible = true;
                    break;
                case MessageBoxButton.YesNoCancel:
                    _window.YesButton.IsVisible = true;
                    _window.NoButton.IsVisible = true;
                    _window.CancelButton.IsVisible = true;
                    break;
                case MessageBoxButton.OkCancel:
                    _window.CancelButton.IsVisible = true;
                    _window.OkButton.IsVisible = true;
                    break;
                default:
                    _window.OkButton.IsVisible = true;
                    break;
            }
        }

        private static void MessageBoxImageMethod(MessageBoxImage icon)
        {
            switch (icon)
            {
                case MessageBoxImage.None:
                    _window.IconImage.IsVisible = false;
                    break;
                case MessageBoxImage.Error:
                    _window.IconImage.Source =
                        LoadBitmap("avares://ThingLing.Avalonia.Controls.MessageBox/Images/delete.png");
                    break;
                case MessageBoxImage.Stop:
                    _window.IconImage.Source =
                        LoadBitmap("avares://ThingLing.Avalonia.Controls.MessageBox/Images/No-entry.png");
                    break;
                case MessageBoxImage.Warning:
                    _window.IconImage.Source =
                        LoadBitmap("avares://ThingLing.Avalonia.Controls.MessageBox/Images/Warning.png");
                    break;
                case MessageBoxImage.Information:
                    _window.IconImage.Source =
                        LoadBitmap("avares://ThingLing.Avalonia.Controls.MessageBox/Images/Info.png");
                    break;
            }
        }

        private static void MessageBoxResultMethod(MessageBoxResult defaultResult)
        {
            switch (defaultResult)
            {
                case MessageBoxResult.None:
                    break;
                case MessageBoxResult.Ok:
                    _window.OkButton.IsDefault = true;
                    _window.OkButton.BorderThickness = new Thickness(UniformThickness);
                    break;
                case MessageBoxResult.Cancel:
                    _window.CancelButton.IsDefault = true;
                    _window.CancelButton.BorderThickness = new Thickness(UniformThickness);
                    break;
                case MessageBoxResult.Yes:
                    _window.YesButton.IsDefault = true;
                    _window.YesButton.BorderThickness = new Thickness(UniformThickness);
                    break;
                case MessageBoxResult.No:
                    _window.NoButton.IsDefault = true;
                    _window.NoButton.BorderThickness = new Thickness(UniformThickness);
                    break;
                default:
                    _window.OkButton.IsDefault = true;
                    _window.OkButton.BorderThickness = new Thickness(UniformThickness);
                    break;
            }
        }

        /// <summary>
        /// Displays a message box that has a message and title bar caption; and that returns a result.
        /// </summary>
        /// <param name="owner">The main window that hosts this message box</param>
        /// <param name="message">A System.String that specifies the text to display.</param>
        /// <param name="title">A System.String that specifies the title bar caption to display.</param>
        /// <param name="button"> A ThingLing.Avalonia.Controls.MessageBoxButton value that specifies which button or buttons to display</param>
        /// <returns>A ThingLing.Avalonia.Controls.MessageBoxResult value that specifies which message box button is clicked by the user.</returns>
        public static async Task<MessageBoxResult> ShowAsync(Window owner, string message, string title,
            MessageBoxButton button)
        {
            MessageBoxItems(message, title);

            _window.OkButton.IsVisible = false;
            MessageBoxButtonMethod(button);

            await _window.ShowDialog(owner);
            return _window.MessageBoxResult;
        }

        /// <summary>
        /// Displays a message box that has a message and title bar caption; and that returns a result.
        /// </summary>
        /// <param name="owner">The main window that hosts this message box</param>
        /// <param name="message">A System.String that specifies the text to display.</param>
        /// <param name="title">A System.String that specifies the title bar caption to display.</param>
        /// <param name="button"> A ThingLing.Avalonia.Controls.MessageBoxButton value that specifies which button or buttons to display</param>
        /// <param name="icon"> A ThingLing.Avalonia.Controls.MessageBoxImage value that specifies the icon to display.</param>
        /// <returns>A ThingLing.Avalonia.Controls.MessageBoxResult value that specifies which message box button is clicked by the user.</returns>
        public static async Task<MessageBoxResult> ShowAsync(Window owner, string message, string title,
            MessageBoxButton button, MessageBoxImage icon)
        {
            MessageBoxItems(message, title);

            _window.OkButton.IsVisible = false;
            MessageBoxButtonMethod(button);
            MessageBoxImageMethod(icon);

            await _window.ShowDialog(owner);
            return _window.MessageBoxResult;
        }

        /// <summary>
        /// Displays a message box that has a message and title bar caption; and that returns a result.
        /// </summary>
        /// <param name="owner">The main window that hosts this message box</param>
        /// <param name="message">A System.String that specifies the text to display.</param>
        /// <param name="title">A System.String that specifies the title bar caption to display.</param>
        /// <param name="button"> A ThingLing.Avalonia.Controls.MessageBoxButton value that specifies which button or buttons to display</param>
        /// <param name="icon"> A ThingLing.Avalonia.Controls.MessageBoxImage value that specifies the icon to display.</param>
        /// <param name="defaultResult"> A ThingLing.Avalonia.Controls.MessageBoxResult value that specifies the default result of the message box.</param>
        /// <returns>A ThingLing.Avalonia.Controls.MessageBoxResult value that specifies which message box button is clicked by the user.</returns>
        public static async Task<MessageBoxResult> ShowAsync(Window owner, string message, string title,
            MessageBoxButton button, MessageBoxImage icon, MessageBoxResult defaultResult)
        {
            MessageBoxItems(message, title);

            _window.OkButton.IsVisible = false;
            MessageBoxButtonMethod(button);
            MessageBoxImageMethod(icon);

            _window.OkButton.IsDefault = false;

            MessageBoxResultMethod(defaultResult);

            await _window.ShowDialog(owner);
            return _window.MessageBoxResult;
        }
    }
}