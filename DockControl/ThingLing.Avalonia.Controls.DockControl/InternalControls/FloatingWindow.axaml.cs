using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using System;
using System.Diagnostics;
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

            TabControl = new TabControl(ThemeMethods.TabControlTheme())
            {
                TabMode = TabMode.Window,
                TabControlParent = this,
                ParentPanel = _dockControl.MainPanel
            };

            TabControl.DockingContextMenu = dockingContextMenu;
            TabControl.HideContextMenu = hideContextMenu;

            TabControl.PointerPressed += Window_PointerPressed;
            TabControlPanel.Children.Add(TabControl);
            TabControl.TabItemRemoved += TabControl_TabItemRemoved;
        }

        private void TabControl_TabItemRemoved(TabItem obj)
        {
            //if (TabControl.TabItemsCount == 0)
            //{
            //    FloatingWindow_Unloaded();
            //}

            fghj
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

        private void FloatingWindow_Unloaded()
        {
            this.MainPanel.Children.Clear();
        }

        private void MakeActive()
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
                Console.WriteLine(ex.Message, "Error");
                Debug.WriteLine(ex.Message);
            }
        }

        private void Window_PointerPressed(object sender, PointerPressedEventArgs e)
        {
            MakeActive();
        }

        private void Window_GotFocus(object sender, RoutedEventArgs e)
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

        private void LeftBorder_PointerPressed(object sender, PointerPressedEventArgs e)
        {
            _edgeType = (int)EdgeTypes.Left;
            Set_Sizing();
        }

        private void TopBorder_PointerPressed(object sender, PointerPressedEventArgs e)
        {
            _edgeType = (int)EdgeTypes.Top;
            Set_Sizing();
        }

        private void RightBorder_PointerPressed(object sender, PointerPressedEventArgs e)
        {
            _edgeType = (int)EdgeTypes.Right;
            Set_Sizing();
        }

        private void BottomBorder_PointerPressed(object sender, PointerPressedEventArgs e)
        {
            _edgeType = (int)EdgeTypes.Bottom;
            Set_Sizing();
        }

        private void Header_PointerPressed(object sender, PointerPressedEventArgs e)
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
        private void LeftBorder_MouseEnter(object sender, PointerEventArgs e)
        {
            Cursor = Cursors.SizeWE;
        }

        private void TopBorder_MouseEnter(object sender, PointerEventArgs e)
        {
            Cursor = Cursors.SizeNS;
        }

        private void RightBorder_MouseEnter(object sender, PointerEventArgs e)
        {
            Cursor = Cursors.SizeWE;
        }

        private void BottomBorder_PointerEnter(object sender, PointerEventArgs e)
        {
            Cursor = Cursors.SizeNS;
        }

        private void Header_PointerEnter(object sender, PointerEventArgs e)
        {
            Cursor = Cursors.Arrow;
        }

        private void Header_PointerLeave(object sender, PointerEventArgs e)
        {
            Cursor = Cursors.Arrow;
        }
        //----</ ENTER >---- 

        #endregion cursor: enter leave

        #endregion Window drag and resize


    }
}
