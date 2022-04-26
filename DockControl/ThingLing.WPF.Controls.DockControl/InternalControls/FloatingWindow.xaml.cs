using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ThingLing.Controls.Methods;
using ThingLing.Controls.Props;

namespace ThingLing.Controls.InternalControls
{
    /// <summary>
    /// Interaction logic for FloatingWindow.xaml
    /// </summary>
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

            TabControl = new TabControl()
            {
                TabMode = TabMode.Window,
                TabControlParent = this,
                ParentPanel = _dockControl.MainPanel,
                Theme = ThemeMethods.TabControlTheme(),
                HideNewTabButton = true,
            };

            TabControl.DockingContextMenu = dockingContextMenu;
            TabControl.HideContextMenu = hideContextMenu;

            TabControl.MouseDown += Window_MouseDown;
            TabControlPanel.Children.Add(TabControl);

            this.Unloaded += FloatingWindow_Unloaded;
        }

        private void FloatingWindow_Unloaded(object sender, RoutedEventArgs e)
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
                MessageBox.Show(ex.Message, "Error");
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
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

        public void Add(string header, string contentPath, UIElement content, Image contentIcon)
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

            tabItem.TabItemHeader().MouseDown += Header_MouseDown;
            tabItem.TabItemHeader().MouseEnter += Header_MouseEnter;
            tabItem.TabItemBody().TabItemHeader.MenuButton.Visibility = Visibility.Visible;
            tabItem.TabItemBody().TabItemHeader.HideButton.Visibility = Visibility.Visible;
            tabItem.TabItemBody().TabItemHeader.MouseDown += Header_MouseDown;
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

        private void LeftBorder_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _edgeType = (int)EdgeTypes.Left;
            Set_Sizing();
        }

        private void TopBorder_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _edgeType = (int)EdgeTypes.Top;
            Set_Sizing();
        }

        private void RightBorder_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _edgeType = (int)EdgeTypes.Right;
            Set_Sizing();
        }

        private void BottomBorder_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _edgeType = (int)EdgeTypes.Bottom;
            Set_Sizing();
        }

        private void Header_MouseDown(object sender, MouseButtonEventArgs e)
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
        private void LeftBorder_MouseEnter(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.SizeWE;
        }

        private void TopBorder_MouseEnter(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.SizeNS;
        }

        private void RightBorder_MouseEnter(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.SizeWE;
        }

        private void BottomBorder_MouseEnter(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.SizeNS;
        }

        private void Header_MouseEnter(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Arrow;
        }

        private void Header_MouseLeave(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Arrow;
        }
        //----</ ENTER >---- 

        #endregion cursor: enter leave

        #endregion Window drag and resize

    }
}
