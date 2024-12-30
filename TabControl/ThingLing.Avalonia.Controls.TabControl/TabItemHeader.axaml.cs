using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using System;
using System.Linq;
using Avalonia.Threading;
using System.Diagnostics;

namespace ThingLing.Controls
{
    public partial class TabItemHeader : UserControl
    {
        public TabItemHeader()
        {
            InitializeComponent();

            Initialized += TabItemHeader_Initialized;
            AttachedToVisualTree += (s, e) =>
            {
                Dispatcher.UIThread.Post(this.BringIntoView, DispatcherPriority.Background);
            };
        }

        private void TabItemHeader_Initialized(object sender, EventArgs e)
        {
            MenuButton.ContextMenu = TabControl.DockingContextMenu;
            HideButton.ContextMenu = TabControl.HideContextMenu;
        }

        private void Border_PointerEntered(object sender, PointerEventArgs e)
        {
            ((Border) sender).BorderBrush = Brushes.White;
        }

        private void Border_PointerExited(object? sender, PointerEventArgs e)
        {
            ((Border)sender).BorderBrush = Brushes.Transparent;
        }

        private void Close()
        {
            try
            {
                TabControl parent;
                TabItem tabItem;

                if ((Parent as Panel)?.Parent.GetType() == typeof(TabItemBody))
                {
                    var panelParent = ((Panel)Parent).Parent as TabItemBody;
                    parent = (TabControl)((Panel)((Panel)((TabItemBody)((Panel)Parent).Parent).Parent).Parent).Parent;
                    tabItem = parent.TabItems!.FirstOrDefault(i =>
                        i.TabItemBody().ContentPanel.Child == panelParent?.ContentPanel.Child);
                }
                else
                {
                    parent = (TabControl)((Panel)((Panel)((ScrollViewer)((Panel)Parent).Parent).Parent).Parent).Parent;
                    tabItem = parent.TabItems!.FirstOrDefault(i => Equals(i.TabItemHeader(), this));
                }

                parent.Remove(tabItem);
                parent.LayoutChanged();

                if (parent.ContentPanel.Children.Count < 1)
                {
                    parent.tabIndex = -1;
                    return;
                }

                var nextTabIndex = parent.HeaderPanel.Children.IndexOf(parent.TabItems[0].TabItemHeader());
                ((TabItemHeader)parent.HeaderPanel.Children[nextTabIndex]).Background = tabItem?.BackgroundWhenFocused;
                ((TabItemHeader)parent.HeaderPanel.Children[nextTabIndex]).Foreground = tabItem?.ForegroundWhenFocused;

                var element = (TabItemHeader)parent.HeaderPanel.Children[nextTabIndex];

                element.BringIntoView(new Rect(new Size(element.Width, element.Height)));

                parent.ContentPanel.Children[nextTabIndex].IsVisible = true;
                parent.ContentPanel.Children[nextTabIndex].Focus();

                parent.tabIndex = nextTabIndex;
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private void CloseWindow_PointerReleased(object sender, PointerReleasedEventArgs e)
        {
            Close();
            e.Handled = true;
        }

        private void ContextMenuButton_PointerReleased(object sender, PointerReleasedEventArgs e)
        {
            var contextMenu = ((Border) sender).ContextMenu;
            if (contextMenu != null) contextMenu.Open();
            // Prevent the event from bubbling up to the parent
            e.Handled = true;
        }

    }
}