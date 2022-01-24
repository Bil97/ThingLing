using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using System;
using System.Linq;
using Avalonia.Threading;

namespace ThingLing.Controls
{
    public class TabItemHeader : UserControl
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

        #region Controls

        internal Image ContentIcon;
        internal TextBlock ContentChanged;
        internal Border CloseButton;
        public Border HideButton;
        public Border MenuButton;
        public TextBlock Header;

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            ContentIcon = this.FindControl<Image>(nameof(ContentIcon));
            ContentChanged = this.FindControl<TextBlock>(nameof(ContentChanged));
            CloseButton = this.FindControl<Border>(nameof(CloseButton));
            HideButton = this.FindControl<Border>(nameof(HideButton));
            MenuButton = this.FindControl<Border>(nameof(MenuButton));
            Header = this.FindControl<TextBlock>(nameof(Header));
        }

        #endregion

        private void TabItemHeader_Initialized(object sender, EventArgs e)
        {
            MenuButton.ContextMenu = TabControl.DockingContextMenu;
            HideButton.ContextMenu = TabControl.HideContextMenu;
        }

        private void Border_PointerEnter(object sender, PointerEventArgs e)
        {
            ((Border) sender).BorderBrush = Brushes.White;
        }

        private void Border_PointerLeave(object sender, PointerEventArgs e)
        {
            ((Border) sender).BorderBrush = Brushes.Transparent;
        }

        private void Close()
        {
            TabControl parent;
            TabItem tabItem;

            if ((Parent as Panel)?.Parent.GetType() == typeof(TabItemBody))
            {
                var panelParent = ((Panel) Parent).Parent as TabItemBody;
                parent = (TabControl) ((Panel) ((Panel) ((TabItemBody) ((Panel) Parent).Parent).Parent).Parent).Parent;
                tabItem = parent.TabItems!.FirstOrDefault(i =>
                    i.TabItemBody().ContentPanel.Child == panelParent?.ContentPanel.Child);
            }
            else
            {
                parent = (TabControl) ((Panel) ((Panel) ((ScrollViewer) ((Panel) Parent).Parent).Parent).Parent).Parent;
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

            ((TabItemHeader) parent.HeaderPanel.Children[nextTabIndex]).Background = tabItem?.BackgroundWhenFocused;
            ((TabItemHeader) parent.HeaderPanel.Children[nextTabIndex]).Foreground = tabItem?.ForegroundWhenFocused;

            var element = (TabItemHeader) parent.HeaderPanel.Children[nextTabIndex];

            element.BringIntoView(new Rect(new Size(element.Width, element.Height)));

            parent.ContentPanel.Children[nextTabIndex].IsVisible = true;
            parent.ContentPanel.Children[nextTabIndex].Focus();

            parent.tabIndex = nextTabIndex;
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