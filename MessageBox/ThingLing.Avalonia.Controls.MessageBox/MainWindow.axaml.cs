using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media.Imaging;
using Avalonia.Platform;

namespace ThingLing.Avalonia.Controls
{
    internal partial class MainWindow : Window
    {
        internal MessageBoxResult MessageBoxResult { get; private set; }
        private const double UniformThickness = 2;

        internal TextBlock TitleTextBlock;
        internal TextBlock MessageTextBlock;
        internal Button OkButton;
        internal Button YesButton;
        internal Button NoButton;
        internal Button CancelButton;
        internal Image IconImage;

        public MainWindow()
        {
            AvaloniaXamlLoader.Load(this);

            TitleTextBlock = this.FindControl<TextBlock>(nameof(TitleTextBlock));
            MessageTextBlock = this.FindControl<TextBlock>(nameof(MessageTextBlock));
            OkButton = this.FindControl<Button>(nameof(OkButton));
            YesButton = this.FindControl<Button>(nameof(YesButton));
            NoButton = this.FindControl<Button>(nameof(NoButton));
            CancelButton = this.FindControl<Button>(nameof(CancelButton));
            IconImage = this.FindControl<Image>(nameof(IconImage));
        }

        private void Window_OnKeyUp(object? sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Escape:
                    MessageBoxResult = MessageBoxResult.None;
                    Close();
                    break;
                case Key.Enter:
                    Close();
                    break;
                case Key.Left:
                    if (OkButton.IsVisible && CancelButton.IsVisible)
                    {
                        if (OkButton.IsDefault)
                        {
                            OkButton.IsDefault = false;
                            OkButton.BorderThickness = new Thickness(0);
                            CancelButton.IsDefault = true;
                            CancelButton.Focus();
                        }
                        else
                        {
                            CancelButton.IsDefault = false;
                            CancelButton.BorderThickness = new Thickness(0);
                            OkButton.IsDefault = true;
                            OkButton.Focus();
                        }
                    }
                    else
                        switch (YesButton.IsVisible)
                        {
                            case true when NoButton.IsVisible && !CancelButton.IsVisible:
                            {
                                if (YesButton.IsDefault)
                                {
                                    YesButton.IsDefault = false;
                                    YesButton.BorderThickness = new Thickness(0);
                                    NoButton.IsDefault = true;
                                    NoButton.Focus();
                                }
                                else
                                {
                                    NoButton.IsDefault = false;
                                    NoButton.BorderThickness = new Thickness(0);
                                    YesButton.IsDefault = true;
                                    YesButton.Focus();
                                }

                                break;
                            }
                            case true when NoButton.IsVisible && CancelButton.IsVisible:
                            {
                                if (YesButton.IsDefault)
                                {
                                    YesButton.IsDefault = false;
                                    YesButton.BorderThickness = new Thickness(0);
                                    CancelButton.IsDefault = true;
                                    CancelButton.Focus();
                                }
                                else if (NoButton.IsDefault)
                                {
                                    NoButton.IsDefault = false;
                                    NoButton.BorderThickness = new Thickness(0);
                                    YesButton.IsDefault = true;
                                    YesButton.Focus();
                                }
                                else
                                {
                                    CancelButton.IsDefault = false;
                                    CancelButton.BorderThickness = new Thickness(0);
                                    NoButton.IsDefault = true;
                                    NoButton.Focus();
                                }

                                break;
                            }
                        }

                    break;
                case Key.Right:
                    if (OkButton.IsVisible && CancelButton.IsVisible)
                    {
                        if (OkButton.IsDefault)
                        {
                            OkButton.IsDefault = false;
                            OkButton.BorderThickness = new Thickness(0);
                            CancelButton.IsDefault = true;
                            CancelButton.Focus();
                        }
                        else
                        {
                            CancelButton.IsDefault = false;
                            CancelButton.BorderThickness = new Thickness(0);
                            OkButton.IsDefault = true;
                            OkButton.Focus();
                        }
                    }
                    else
                        switch (YesButton.IsVisible)
                        {
                            case true when NoButton.IsVisible && !CancelButton.IsVisible:
                            {
                                if (YesButton.IsDefault)
                                {
                                    YesButton.IsDefault = false;
                                    YesButton.BorderThickness = new Thickness(0);
                                    NoButton.IsDefault = true;
                                    NoButton.Focus();
                                }
                                else
                                {
                                    NoButton.IsDefault = false;
                                    NoButton.BorderThickness = new Thickness(0);
                                    YesButton.IsDefault = true;
                                    YesButton.Focus();
                                }

                                break;
                            }
                            case true when NoButton.IsVisible && CancelButton.IsVisible:
                            {
                                if (YesButton.IsDefault)
                                {
                                    YesButton.IsDefault = false;
                                    YesButton.BorderThickness = new Thickness(0);
                                    NoButton.IsDefault = true;
                                    NoButton.Focus();
                                }
                                else if (NoButton.IsDefault)
                                {
                                    NoButton.IsDefault = false;
                                    NoButton.BorderThickness = new Thickness(0);
                                    CancelButton.IsDefault = true;
                                    CancelButton.Focus();
                                }
                                else
                                {
                                    CancelButton.IsDefault = false;
                                    CancelButton.BorderThickness = new Thickness(0);
                                    YesButton.IsDefault = true;
                                    YesButton.Focus();
                                }

                                break;
                            }
                        }

                    break;
            }
        }

        private void CloseButton_Click(object? sender, RoutedEventArgs e)
        {
            MessageBoxResult = MessageBoxResult.None;
            Close();
        }

        private void OKButton_Click(object? sender, RoutedEventArgs e)
        {
            MessageBoxResult = MessageBoxResult.Ok;
            Close();
        }

        private void YesButton_Click(object? sender, RoutedEventArgs e)
        {
            MessageBoxResult = MessageBoxResult.Yes;
            Close();
        }

        private void NoButton_Click(object? sender, RoutedEventArgs e)
        {
            MessageBoxResult = MessageBoxResult.No;
            Close();
        }

        private void CancelButton_Click(object? sender, RoutedEventArgs e)
        {
            MessageBoxResult = MessageBoxResult.Cancel;
            Close();
        }

        private void Button_OnGotFocus(object? sender, GotFocusEventArgs e)
        {
            (sender as Button).BorderThickness = new Thickness(UniformThickness);
        }

        private void HeaderPanel_OnPointerPressed(object? sender, PointerPressedEventArgs e)
        {
            BeginMoveDrag(e);
        }
    }
}