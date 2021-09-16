using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ThingLing.Controls
{
    public partial class TabItemHeader : UserControl
    {
        public TabItemHeader()
        {
            InitializeComponent();
            this.Loaded += TabItemHeader_Loaded;
        }

        private void TabItemHeader_Loaded(object sender, RoutedEventArgs e)
        {
            MenuButton.ContextMenu = TabControl.DockingContextMenu;
            HideButton.ContextMenu = TabControl.HideContextMenu;
            this.BringIntoView(new Rect(new Size(this.ActualWidth, this.ActualHeight)));
        }

        private void Border_MouseEnter(object sender, MouseEventArgs e)
        {
            ((Border)sender).BorderBrush = Brushes.White;
        }

        private void Border_MouseLeave(object sender, MouseEventArgs e)
        {
            ((Border)sender).BorderBrush = Brushes.Transparent;
        }

        private void Close()
        {
            TabControl parent;
            TabItem tabItem;

            if ((Parent as Panel)?.Parent.GetType() == typeof(TabItemBody))
            {
                var panelParent = ((Panel)Parent).Parent as TabItemBody;
                parent = (TabControl)((Panel)((Panel)((TabItemBody)((Panel)Parent).Parent).Parent).Parent).Parent;
                tabItem = parent.TabItems!.FirstOrDefault(i => i.TabItemBody().ContentPanel.Child == panelParent?.ContentPanel.Child);
            }
            else
            {
                parent = (TabControl)((Panel)((Panel)((ScrollViewer)((Panel)Parent).Parent).Parent).Parent).Parent;
                tabItem = parent.TabItems!.FirstOrDefault(i => i.TabItemHeader() == this);
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

            element.BringIntoView(new Rect(new Size(element.ActualWidth, element.ActualHeight)));

            parent.ContentPanel.Children[nextTabIndex].Visibility = Visibility.Visible;
            parent.ContentPanel.Children[nextTabIndex].Focus();

            parent.tabIndex = nextTabIndex;
        }

        private void CloseWindow_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Close();
            e.Handled = true;
        }

        private void ContextMenuButton_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var contextMenu = ((Border)sender).ContextMenu;
            if (contextMenu != null) contextMenu.IsOpen = true;
            // Prevent the event from bubbling up to the parent
            e.Handled = true;
        }

    }
}