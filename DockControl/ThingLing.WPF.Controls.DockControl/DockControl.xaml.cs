using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ThingLing.Controls.InternalControls;
using ThingLing.Controls.Methods;
using ThingLing.Controls.Props;

namespace ThingLing.Controls
{
    /// <summary>
    /// Interaction logic for DockControl.xaml
    /// </summary>
    public partial class DockControl : UserControl
    {
        internal readonly List<string> FloatingWindows = new();
        internal int currentZIndex = 1;
        internal static Theme Theme { get; set; }

        public DockControl()
        {
            InitializeComponent();
            var themeMethods = new ThemeMethods();
            themeMethods.LoadTheme();
        }

        public DockControl(Theme theme)
        {
            InitializeComponent();
            var themeMethods = new ThemeMethods();
            themeMethods.LoadTheme(theme);

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            MainPanel.Background = CurrentTheme.WindowBackground;
        }

        /// <summary>
        /// Adds a floating window to ThingLing.Controls.DockControl
        /// </summary>
        /// <param name="header">The title of the content. It is displayed at the top of the window</param>
        /// <param name="contentPath">The path to the content displayed</param>
        /// <param name="content">The content displayed</param>
        /// <param name="left">The left location to display the window in the ThingLing.Controls.DockControl</param>
        /// <param name="top">The top location to display the window in the ThingLing.Controls.DockControl</param>
        /// <param name="contentIcon">The icon of the content displayed</param>
        public void AddWindow(string header, string contentPath, UIElement content, double left = 0, double top = 0, Image contentIcon = null)
        {
            var window = header + contentPath;
            if (FloatingWindows.Contains(window))
            {
                FloatingWindows.Remove(window);
            }
            else
            {
                var floatingWindow = new FloatingWindow(this)
                {
                    WindowName = window
                };
                Canvas.SetLeft(floatingWindow, left);
                Canvas.SetTop(floatingWindow, top);
                floatingWindow.Add(header, contentPath, content, contentIcon);
                MainPanel.Children.Add(floatingWindow);
            }

            FloatingWindows.Add(window);
            ArrangeWindows();
        }

        /// <summary>
        /// Adds a document tab to ThingLing.Controls.TabControl
        /// </summary>
        /// <param name="header">The title of the content. It is displayed at the header of the ThingLing.Controls.TabControl TabItem</param>
        /// <param name="contentPath">The path to the content displayed</param>
        /// <param name="content">The content displayed</param>
        /// <param name="contentIcon">The icon of the content displayed</param>
        public void AddDocument(string header, string contentPath, UIElement content, Image contentIcon = null)
        {
            ContentControl.AddDocument(header, contentPath, content, contentIcon);
        }

        internal void ArrangeWindows()
        {
            foreach (var child in MainPanel.Children)
            {
                if (child.GetType() != typeof(FloatingWindow))
                {
                    continue;
                }

                if (child is not FloatingWindow win) continue;
                //var childName = win.Header.Text + win.Header.ToolTip;
                var childName = win.WindowName;
                var index = FloatingWindows.IndexOf(childName);
                Panel.SetZIndex(win, index);
            }
        }

        #region mouse dragging

        //-------------< MouseMove() >------------- 

        internal bool IsSizing;
        internal int SizingEdgeType;
        internal double SizingOffsetX;
        internal double SizingOffsetY;
        internal FloatingWindow SizingPanel;

        private enum EdgeTypes
        {
            HeaderMove = 0,
            Left = 1,
            Top = 2,
            Right = 3,
            Bottom = 4
        }

        private void MainPanel_MouseUp(object sender, MouseButtonEventArgs e)
        {
            //-------------< MouseUp() >------------- 
            if (!IsSizing) return;
            //--< reset >-- 
            IsSizing = false;
            SizingEdgeType = -1;
            SizingOffsetX = 0;
            SizingOffsetY = 0;
            SizingPanel = null;
            //--</ reset >-- 
            //-------------</ MouseUp() >------------- 
        }

        private void MainPanel_MouseMove(object sender, MouseEventArgs e)
        {
            //------------< MouseMove() >------------ 
            if (IsSizing) Set_Sizing(e);
            //-------------</ MouseMove() >------------ 
        }

        private void Set_Sizing(MouseEventArgs e)
        {
            //-------------< sizing_Cols_And_Rows() >------------- 
            if (IsSizing == false) return;
            //< check > 
            if (SizingEdgeType < 0) return;
            if (SizingPanel == null) return;
            //</ check > 
            if (e.LeftButton != MouseButtonState.Pressed)
            {
                IsSizing = false;
            }
            else
            {
                //------< MouseButtonState.Pressed >------ 
                //< mouse position > 
                var mousePoint = e.GetPosition(this);
                var mouseX = mousePoint.X;
                var mouseY = mousePoint.Y;
                //</ mouse position > 
                //--< get Position >-- 
                var position = this.TranslatePoint(new Point(0, 0), SizingPanel);
                var posX = -position.X;
                var posY = -position.Y;
                //--</ get Position >-- 

                ////--</ get Offset >-- 
                var diffX = (posX - mouseX);
                var diffY = (posY - mouseY);
                if (SizingEdgeType == (int)EdgeTypes.HeaderMove)
                {
                    //----< sizing Move >---- 
                    var newLeft = mouseX - SizingOffsetX;
                    var excessRight = MainPanel.ActualWidth - SizingPanel.ActualWidth;
                    if (newLeft > excessRight && excessRight > 0) newLeft = excessRight;
                    var newTop = mouseY - SizingOffsetY;
                    var excessBottom = MainPanel.ActualHeight - SizingPanel.ActualHeight;
                    if (newTop > excessBottom && excessBottom > 0) newTop = excessBottom;
                    //< set > 
                    if (newLeft > 0) Canvas.SetLeft(SizingPanel, newLeft);
                    if (newTop > 0) Canvas.SetTop(SizingPanel, newTop);

                    //</ set > 
                    //----</ sizing Move >---- 
                }
                else
                {
                    //-------------< sizing >-------------- 
                    switch (SizingEdgeType)
                    {
                        case (int)EdgeTypes.Left:
                            {
                                //----< sizing Left >---- 
                                var newLeft = mouseX;
                                var newWidth = (SizingPanel.ActualWidth) + diffX;
                                //< set Left > 
                                if (newLeft > 0) Canvas.SetLeft(SizingPanel, newLeft);
                                //</ set Left > 
                                //< set width > 
                                if (newWidth > 0) SizingPanel.Width = newWidth;
                                //</ set width > 
                                //----</ sizing Left >---- 
                                break;
                            }
                        case (int)EdgeTypes.Top:
                            {
                                //----< sizing Top >---- 
                                var newTop = mouseY;
                                var newHeight = (SizingPanel.ActualHeight) + diffY;
                                //< set Left > 
                                if (newTop > 0) Canvas.SetTop(SizingPanel, newTop);
                                //</ set Left > 
                                //< set width > 
                                if (newHeight > 0) SizingPanel.Height = newHeight;
                                //</ set width > 
                                //----</ sizing Top >---- 
                                break;
                            }
                        case (int)EdgeTypes.Right:
                            {
                                //----< sizing Right >---- 
                                var newWidth = mouseX - posX;
                                //< set width > 
                                if (newWidth > 0) SizingPanel.Width = newWidth;
                                //</ set width > 
                                //----</ sizing Right >---- 
                                break;
                            }
                        case (int)EdgeTypes.Bottom:
                            {
                                //----< sizing Bottom >---- 
                                var newHeight = mouseY - posY;
                                //< set width > 
                                if (newHeight > 0) SizingPanel.Height = newHeight;
                                //</ set width > 
                                //----</ sizing Bottom >---- 
                                break;
                            }
                    }

                    //-------------</ sizing >-------------- 
                }

                //------</ MouseButtonState.Pressed >------ 
            }

            //-------------</ sizing_Cols_And_Rows() >------------- 
        }

        #endregion mouse dragging
    }
}
