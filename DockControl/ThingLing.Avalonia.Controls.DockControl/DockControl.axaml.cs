using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using System;
using System.Collections.Generic;
using ThingLing.Controls.InternalControls;
using ThingLing.Controls.Methods;
using ThingLing.Controls.Props;
using ContentControl = ThingLing.Controls.InternalControls.ContentControl;

namespace ThingLing.Controls
{
    public partial class DockControl : UserControl
    {
        internal readonly List<string> FloatingWindows = new();
        internal int currentZIndex = 1;
        internal static Theme? Theme { get; set; }

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

        #region Controls

        internal Canvas? MainPanel;
        internal ContentControl? ContentControl;
        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);

            MainPanel = this.FindControl<Canvas>(nameof(MainPanel));
            ContentControl = this.FindControl<ContentControl>(nameof(ContentControl));
        }

        #endregion

        private void UserControl_Initialized(object sender, EventArgs e)
        {
            MainPanel.Background = CurrentTheme.WindowBackground;
        }

        /// <summary>
        /// Adds a floating window to ThingLing.WPF.Controls.DockControl
        /// </summary>
        /// <param name="header">The title of the content. It is displayed at the top of the window</param>
        /// <param name="contentPath">The path to the content displayed</param>
        /// <param name="content">The content displayed</param>
        /// <param name="left">The left location to display the window in the ThingLing.WPF.Controls.DockControl</param>
        /// <param name="top">The top location to display the window in the ThingLing.WPF.Controls.DockControl</param>
        /// <param name="contentIcon">The icon of the content displayed</param>
        public void AddWindow(string header, string contentPath, Control content, double left = 0, double top = 0, Image? contentIcon = null)
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
        /// Adds a document tab to ThingLing.WPF.Controls.TabControl
        /// </summary>
        /// <param name="header">The title of the content. It is displayed at the header of the ThingLing.WPF.Controls.TabControl TabItem</param>
        /// <param name="contentPath">The path to the content displayed</param>
        /// <param name="content">The content displayed</param>
        /// <param name="contentIcon">The icon of the content displayed</param>
        public void AddDocument(string header, string contentPath, Control content, Image contentIcon = null)
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
                win.ZIndex = index;
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

        private void MainPanel_PointerReleased(object sender, PointerReleasedEventArgs e)
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

        private void MainPanel_PointerMove(object sender, PointerEventArgs e)
        {
            //------------< MouseMove() >------------ 
            if (IsSizing) Set_Sizing(e);
            //-------------</ MouseMove() >------------ 
        }

        private void Set_Sizing(PointerEventArgs e)
        {
            //-------------< sizing_Cols_And_Rows() >------------- 
            if (IsSizing == false) return;
            //< check > 
            if (SizingEdgeType < 0) return;
            if (SizingPanel == null) return;
            //</ check > 
            if (e.LeftButton != MouseButtonState.Pressed)
            if (e.Pointer.IsPrimary && e.PointerPre)
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
                var posX = -position.Value.X;
                var posY = -position.Value.Y;
                //--</ get Position >-- 

                ////--</ get Offset >-- 
                var diffX = (posX - mouseX);
                var diffY = (posY - mouseY);
                if (SizingEdgeType == (int)EdgeTypes.HeaderMove)
                {
                    //----< sizing Move >---- 
                    var newLeft = mouseX - SizingOffsetX;
                    var excessRight = MainPanel.Width - SizingPanel.Width;
                    if (newLeft > excessRight && excessRight > 0) newLeft = excessRight;
                    var newTop = mouseY - SizingOffsetY;
                    var excessBottom = MainPanel.Height - SizingPanel.Height;
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
                                var newWidth = (SizingPanel.Width) + diffX;
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
                                var newHeight = (SizingPanel.Height) + diffY;
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
