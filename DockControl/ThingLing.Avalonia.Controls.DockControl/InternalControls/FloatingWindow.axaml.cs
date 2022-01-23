using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using System;
using ThingLing.Controls.Methods;
using ThingLing.Controls.Props;

namespace ThingLing.Controls.InternalControls
{
    internal partial class FloatingWindow : UserControl
    {
        public string WindowName { get; set; }
        private DockControl _dockControl;
        DockingContextMenu dockingContextMenu = new();
        HideContextMenu hideContextMenu = new();
        TabControl TabControl;

        public FloatingWindow()
        {
            InitializeComponent();
        }

        public FloatingWindow(DockControl dockControl)
        {
            InitializeComponent();

            _dockControl = dockControl;

            dockingContextMenu.DockTopMenuItem.Click += DockTop_Click;
            dockingContextMenu.DockLeftMenuItem.Click += DockLeft_Click;
            dockingContextMenu.DockBottomMenuItem.Click += DockBottom_Click;
            dockingContextMenu.DockRightMenuItem.Click += DockRight_Click;
            dockingContextMenu.DockAsDocumentMenuItem.Click += DockDocument_Click;
            dockingContextMenu.TabModeSwitch.Click += TabModeSwitch_Click;
            hideContextMenu.AutoHideLeftMenuItem.Click += HideLeft_Click;
            hideContextMenu.AutoHideBottomMenuItem.Click += HideBottom_Click;
            hideContextMenu.AutoHideRightMenuItem.Click += HideRight_Click;

            TabControl = new TabControl()
            {
                TabMode = TabMode.Window,
                TabControlParent = this,
                ParentPanel = _dockControl.MainPanel,
                Theme = ThemeMethods.TabControlTheme()
            };

            TabControl.DockingContextMenu = dockingContextMenu;
            TabControl.HideContextMenu = hideContextMenu;

            TabControl.PointerPressed += Window_PointerPressed;
            TabControlPanel.Children.Add(TabControl);
        }

        #region Controls

        internal Grid MainPanel;
        internal Border? HeaderBorder;
        internal Border? LeftBorder;
        internal Border? TopBorder;
        internal Border? RightBorder;
        internal Border? BottomBorder;
        internal Grid? TabControlPanel;
        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);

            MainPanel = this.FindControl<Grid>(nameof(MainPanel));
            HeaderBorder = this.FindControl<Border>(nameof(HeaderBorder));
            LeftBorder = this.FindControl<Border>(nameof(LeftBorder));
            TopBorder = this.FindControl<Border>(nameof(TopBorder));
            BottomBorder = this.FindControl<Border>(nameof(BottomBorder));
            RightBorder = this.FindControl<Border>(nameof(RightBorder));
            TabControlPanel = this.FindControl<Grid>(nameof(TabControlPanel));
        }

        #endregion

        private void FloatingWindow_Unloaded(object sender, RoutedEventArgs e)
        {
            this.MainPanel.Children.Clear();
        }

        private async void MakeActive()
        {
            TabControl.SelectedTabItem.TabItemBody().TabItemHeader.Background = CurrentTheme.SelectedWindowHeadingBackground;
            TabControl.SelectedTabItem.TabItemBody().TabItemHeader.Header.Foreground = CurrentTheme.SelectedWindowHeadingForeground;

            try
            {
                var window = this.WindowName;
                _dockControl.FloatingWindows.Remove(window);
                _dockControl.FloatingWindows.Add(window);

                _dockControl.ArrangeWindows();
            }
            catch (NullReferenceException ex)
            {
                var desktop = (IClassicDesktopStyleApplicationLifetime)Application.Current.ApplicationLifetime;
                await MessageBox.ShowAsync(desktop.MainWindow, ex.Message, "Error");
            }
        }

        private void Window_PointerPressed(object? sender, PointerPressedEventArgs e)
        {
            MakeActive();
        }

        private void Window_GotFocus(object sender, GotFocusEventArgs e)
        {
            MakeActive();
        }

        private void Window_LostFocus(object sender, RoutedEventArgs e)
        {
            TabControl.SelectedTabItem.TabItemBody().TabItemHeader.Background = CurrentTheme.UnSelectedWindowHeadingBackground;
            TabControl.SelectedTabItem.TabItemBody().TabItemHeader.Header.Foreground = CurrentTheme.UnSelectedWindowHeadingForeground;
        }

        public void Add(string header, string contentPath, Control content, Image contentIcon)
        {
            content ??= new TextBlock
            {
                Text = "There is no content to display for this window",
                TextWrapping = TextWrapping.Wrap,
                Margin = new Thickness(3),
                HorizontalAlignment = HorizontalAlignment.Center
            };
            var tabItem = new TabItem
            {
                Header = header,
                ToolTip = contentPath,
                Content = content,
                ContentIcon = contentIcon
            };

            tabItem.TabItemHeader().PointerPressed += Header_PointerPressed;
            tabItem.TabItemHeader().PointerEnter += Header_PointerEnter;
            tabItem.TabItemBody().TabItemHeader.MenuButton.IsVisible = true;
            tabItem.TabItemBody().TabItemHeader.HideButton.IsVisible = true;
            tabItem.TabItemBody().TabItemHeader.PointerPressed += Header_PointerPressed;
            this.LostFocus += (sender, e) =>
            {
                tabItem.TabItemHeader().Background = CurrentTheme.UnSelectedWindowHeadingBackground;
                tabItem.TabItemBody().TabItemHeader.Header.Foreground = CurrentTheme.UnSelectedWindowHeadingForeground;
            };

            TabControl.Add(tabItem);
            //ContentPanel.Children.Add(content);

            content.Focus();
        }

        private void DockTop_Click(object sender, RoutedEventArgs e)
        {
            //
        }

        private void DockRight_Click(object sender, RoutedEventArgs e)
        {
            //
        }

        private void DockLeft_Click(object sender, RoutedEventArgs e)
        {
            //
        }

        private void DockBottom_Click(object sender, RoutedEventArgs e)
        {
            //
        }

        private void DockDocument_Click(object sender, RoutedEventArgs e)
        {
            //
        }

        private void HideLeft_Click(object sender, RoutedEventArgs e)
        {

        }
        private void HideRight_Click(object sender, RoutedEventArgs e)
        {

        }
        private void HideBottom_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TabModeSwitch_Click(object sender, RoutedEventArgs e)
        {
            if (TabControl.TabMode == TabMode.Document) TabControl.TabMode = TabMode.Window;
            else TabControl.TabMode = TabMode.Document;
            dockingContextMenu.TabModeSwitch.Header = $"Switch to {TabControl.TabMode} Mode";
        }

        #region Window drag and resize

        private int _edgeType;

        private enum EdgeTypes
        {
            Left = 1,
            Top = 2,
            Right = 3,
            Bottom = 4
        }

        private void LeftBorder_PointerPressed(object? sender, PointerPressedEventArgs e)
        {
            _edgeType = (int)EdgeTypes.Left;
            Set_Sizing();
        }

        private void TopBorder_PointerPressed(object? sender, PointerPressedEventArgs e)
        {
            _edgeType = (int)EdgeTypes.Top;
            Set_Sizing();
        }

        private void RightBorder_PointerPressed(object? sender, PointerPressedEventArgs e)
        {
            _edgeType = (int)EdgeTypes.Right;
            Set_Sizing();
        }

        private void BottomBorder_PointerPressed(object? sender, PointerPressedEventArgs e)
        {
            _edgeType = (int)EdgeTypes.Bottom;
            Set_Sizing();
        }

        private void Header_PointerPressed(object? sender, PointerPressedEventArgs e)
        {
            //-------------< set_Sizing() >------------- 
            var offset = e.GetPosition(HeaderBorder);
            var offsetX = offset.X;
            var offsetY = offset.Y;
            //< set main > 
            if (Parent is not Canvas canvasMain) return;
            if (canvasMain.Parent is not DockControl main) return;
            main.IsSizing = true;
            main.SizingEdgeType = 0;
            main.SizingPanel = this;
            main.SizingOffsetX = offsetX;
            main.SizingOffsetY = offsetY;
            //</ set main > 
            //-------------<// set_Sizing() >------------- 
        }

        private void Set_Sizing()
        {
            //-------------< set_Sizing() >------------- 
            //< set main > 
            if (Parent is not Canvas canvasMain) return;
            if (canvasMain.Parent is not DockControl main) return;
            main.IsSizing = true;
            main.SizingEdgeType = _edgeType;
            main.SizingPanel = this;
            //</ set main > 
            //-------------<// set_Sizing() >------------- 
        }

        #region Region: Cursor enter leave

        //----< ENTER >---- 
        private void LeftBorder_PointerEnter(object? sender, PointerEventArgs e)
        {
            Cursor = new Cursor(StandardCursorType.SizeWestEast);
        }

        private void TopBorder_PointerEnter(object? sender, PointerEventArgs e)
        {
            Cursor = new Cursor(StandardCursorType.SizeNorthSouth);
        }

        private void RightBorder_PointerEnter(object? sender, PointerEventArgs e)
        {
            Cursor = new Cursor(StandardCursorType.SizeWestEast);
        }

        private void BottomBorder_PointerEnter(object? sender, PointerEventArgs e)
        {
            Cursor = new Cursor(StandardCursorType.SizeNorthSouth);
        }

        private void Header_PointerEnter(object? sender, PointerEventArgs e)
        {
            Cursor = new Cursor(StandardCursorType.Arrow);
        }

        private void Header_PointerLeave(object? sender, PointerEventArgs e)
        {
            Cursor = new Cursor(StandardCursorType.Arrow);
        }
        //----</ ENTER >---- 

        #endregion cursor: enter leave

        #endregion Window drag and resize

    }
}
