using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Layout;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using ThingLing.Controls.Methods;
using ThingLing.Controls.Props;

namespace ThingLing.Controls
{
    /// <summary>
    ///  Represents a control that contains multiple items that share the same space on the screen.
    /// </summary>
    public partial class TabControl : UserControl
    {
        #region Properties

        /// <summary>
        /// Determines whether the TabControl arranges TabItems as windows or as documents
        /// </summary>
        public TabMode TabMode { get; set; }

        /// <summary>
        /// Determines the location of the TabStrip or TabItem headers; whether Top, Left, Bottom or Right
        /// </summary>
        public TabStripPlacement? TabStripPlacementSide { get; set; }

        /// <summary>
        /// Determines the Angle of rotation of a TabIte
        /// </summary>
        public double? TabItemRotationAngle { get; set; }

        /// <summary>
        /// Shows the number of TabItems
        /// </summary>
        public int TabItemsCount { get; internal set; }

        /// <summary>
        /// The currently selected TabItem 
        /// </summary>
        public TabItem SelectedTabItem
        {
            get => _selectedTabItem;
            set
            {
                _selectedTabItem = value;
                FocusThisTabItem(value);
            }
        }

        /// <summary>
        /// Provides context menu to the Menu button
        /// </summary>
        public static ContextMenu DockingContextMenu { get; set; }

        /// <summary>
        /// Panel from which this TabControl's TabControlParent is removed when the close button is clicked from the specified parent
        /// </summary>

        public Panel ParentPanel { get; set; }

        /// <summary>
        /// The parent to this TabControl that is removed when the close button is clicked from the specified parent
        /// </summary>

        public Control TabControlParent { get; set; }

        /// <summary>
        /// Provides context menu to the Hide button
        /// </summary>
        /// <summary>
        /// Provides the colors used to decorate the this TabControl
        /// </summary>

        public TabControlTheme Theme
        {
            get => _theme;
            set
            {
                _theme = value;
                _ = new LoadTheme(value);
            }
        }

        /// <summary>
        /// Provides context menu to the Hide button
        /// </summary>
        public static ContextMenu HideContextMenu { get; set; }

        internal readonly List<TabItem> TabItems = new();

        /// <summary>
        /// Holds the index of the current focused TabItem in this TabControl
        /// </summary>
        internal int tabIndex = -1;

        private TabItem _selectedTabItem;
        private TabControlTheme _theme;

        /// <summary>
        /// Determines whether to collapse this TabControl's visibility when it has no TabItem
        /// and also whether keep showing the NewTabButton Button and the OpenTabs button when this TabControl has one or no child
        /// </summary>
        public bool AlwaysVisible { get; set; } = true;

        /// <summary>
        /// Determines whether to collapse the AddItem Button
        /// </summary>
        public bool HideNewTabButton { get; set; }

        /// <summary>
        /// Determines whether to collapse the OpenTabs button
        /// </summary>
        public bool HideOpenTabsButton { get; set; }

        #endregion Properties

        public TabControl()
        {
            InitializeComponent();
            Initialized += TabControl_Initialized;
            if (Theme == null) _ = new LoadTheme();
        }

        #region Controls

        internal DockPanel TabStrip;
        internal Image DocMenu;
        internal Image NewTabItem;
        internal StackPanel HeaderPanel;
        internal Border SeparatorBorder;
        internal Grid ContentPanel;

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);

            TabStrip = this.FindControl<DockPanel>(nameof(TabStrip));
            DocMenu = this.FindControl<Image>(nameof(DocMenu));
            NewTabItem = this.FindControl<Image>(nameof(NewTabItem));
            HeaderPanel = this.FindControl<StackPanel>(nameof(HeaderPanel));
            SeparatorBorder = this.FindControl<Border>(nameof(SeparatorBorder));
            ContentPanel = this.FindControl<Grid>(nameof(ContentPanel));
        }

        #endregion Controls

        private void TabControl_Initialized(object sender, EventArgs e)
        {
            SeparatorBorder.BorderBrush = CurrentTheme.SeparatorBorderBrush;
            this.Background = CurrentTheme.TabControlBackground;
            NewTabItem.IsVisible = !HideNewTabButton;
            DocMenu.IsVisible = !HideOpenTabsButton;
        }

        private void TabStrip_Placement()
        {
            switch (TabStripPlacementSide)
            {
                case TabStripPlacement.Top:
                    TabItemRotationAngle ??= 0;
                    DockPanel.SetDock(TabStrip, Dock.Top);
                    DockPanel.SetDock(SeparatorBorder, Dock.Top);
                    HeaderPanel.Orientation = Orientation.Horizontal;
                    DockPanel.SetDock(DocMenu, Dock.Right);
                    break;
                case TabStripPlacement.Left:
                    TabItemRotationAngle ??= -90;
                    DockPanel.SetDock(TabStrip, Dock.Left);
                    DockPanel.SetDock(SeparatorBorder, Dock.Left);
                    HeaderPanel.Orientation = Orientation.Vertical;
                    DockPanel.SetDock(DocMenu, Dock.Bottom);
                    break;
                case TabStripPlacement.Bottom:
                    TabItemRotationAngle ??= 0;
                    DockPanel.SetDock(TabStrip, Dock.Bottom);
                    DockPanel.SetDock(SeparatorBorder, Dock.Bottom);
                    HeaderPanel.Orientation = Orientation.Horizontal;
                    DockPanel.SetDock(DocMenu, Dock.Right);
                    break;
                case TabStripPlacement.Right:
                    TabItemRotationAngle ??= 90;
                    DockPanel.SetDock(TabStrip, Dock.Right);
                    DockPanel.SetDock(SeparatorBorder, Dock.Right);
                    HeaderPanel.Orientation = Orientation.Vertical;
                    DockPanel.SetDock(DocMenu, Dock.Bottom);
                    break;
            }
        }

        /// <summary>
        /// This event is fired when a TabItem is added
        /// </summary>
        public event Action<TabItem> TabItemAdded;

        /// <summary>
        /// Adds a new TabItem to the target InpossibleTabControl
        /// </summary>
        /// <param name="tabItem">The TabItem to be added</param>
        public void Add(TabItem tabItem)
        {
            tabItem.Content ??= ErrorMessage.EmptyContent;

            tabItem.TabItemHeader().PointerPressed += TabItem_MouseDown;
            tabItem.TabItemBody().PointerPressed += TabItem_MouseDown;

            void TabItem_MouseDown(object sender, PointerPressedEventArgs e)
            {
                Click();
            }

            ++tabIndex;
            switch (TabMode)
            {
                case TabMode.Document:
                    // Do not mess this order
                    TabStripPlacementSide ??= TabStripPlacement.Top;
                    TabStrip_Placement();
                    tabItem.TabItemHeader().RenderTransform = new RotateTransform((double)TabItemRotationAngle);
                    HeaderPanel.Children.Insert(tabIndex, tabItem.TabItemHeader());
                    ContentPanel.Children.Insert(tabIndex, tabItem.Content);
                    break;
                case TabMode.Window:
                    // Do not mess this order
                    TabStripPlacementSide ??= TabStripPlacement.Bottom;
                    TabStrip_Placement();
                    tabItem.TabItemHeader().RenderTransform = new RotateTransform((double)TabItemRotationAngle);
                    tabItem.TabItemHeader().CloseButton.IsVisible = false;
                    ;
                    HeaderPanel.Children.Insert(tabIndex, tabItem.TabItemHeader());
                    tabItem.TabItemBody().ContentPanel.Child = tabItem.Content;
                    ContentPanel.Children.Insert(tabIndex, tabItem.TabItemBody());
                    break;
            }

            void Click()
            {
                // Select a TabItem
                this.SelectedTabItem = tabItem;
                // Set this as the first TabItem in the OpenTabItems list
                TabItems.Remove(tabItem);
                TabItems.Insert(0, tabItem);
            }

            tabItem.Content.GotFocus += (sender, e) => { Click(); };

            // Select a TabItem
            SelectedTabItem = tabItem;

            TabItems.Insert(0, tabItem);

            ContentPanel.Children[tabIndex].Focus();

            LayoutChanged();
            TabItemAdded?.Invoke(tabItem);
        }

        /// <summary>
        /// Sets focus to the selected TabItem
        /// </summary>
        /// <param name="tabItem">The TabItem to set focus on</param>
        private void FocusThisTabItem(TabItem tabItem)
        {
            foreach (var child in HeaderPanel.Children)
            {
                ((TabItemHeader)child).Background = tabItem.BackgroundWhenUnFocused;
                ((TabItemHeader)child).Foreground = tabItem.ForegroundWhenUnFocused;
            }

            foreach (Control child in ContentPanel.Children)
            {
                if (child != null)
                    child.IsVisible = false;
            }

            tabIndex = HeaderPanel.Children.IndexOf(tabItem.TabItemHeader());

            var element = (TabItemHeader)HeaderPanel.Children[tabIndex];
            element.Background = tabItem.BackgroundWhenFocused;
            element.Foreground = tabItem.ForegroundWhenFocused;
            ContentPanel.Children[tabIndex].IsVisible = true;
            element.BringIntoView(new Rect(new Size(element.Bounds.Width, element.Bounds.Height)));
        }

        /// <summary>
        /// This event is fired when a TabItem is removed
        /// </summary>
        public event Action<TabItem> TabItemRemoved;

        /// <summary>
        /// Removes a specified TabItem from this TabControl
        /// </summary>
        /// <param name="tabItem">The TabItem to be removed</param>
        public void Remove(TabItem tabItem)
        {
            var _tabItem = TabItems.FirstOrDefault(i => i == tabItem);

            if (_tabItem == null) return;
            HeaderPanel.Children.Remove(_tabItem.TabItemHeader());
            ContentPanel.Children.Remove(TabMode == TabMode.Document ? _tabItem.Content : _tabItem.TabItemBody());
            TabItems.Remove(tabItem);
            LayoutChanged();
            TabItemRemoved?.Invoke(tabItem);
        }

        /// <summary>
        /// Removes a TabItem from this TabControl at a specified index
        /// </summary>
        /// <param name="tabIndex">The index of the TabItem to be removed</param>
        public void RemoveAt(int tabIndex)
        {
            var _tabIndex = Math.Abs(tabIndex);
            if (TabItems.Count < _tabIndex) return;
            ContentPanel.Children.RemoveAt(_tabIndex);
            HeaderPanel.Children.RemoveAt(_tabIndex);
            var tabItem = TabItems[_tabIndex];
            TabItemRemoved?.Invoke(tabItem);
            TabItems.RemoveAt(_tabIndex);
            LayoutChanged();
        }

        /// <summary>
        /// Removes all TabItems from this TabControl
        /// </summary>
        public void RemoveAll()
        {
            ContentPanel.Children.Clear();
            HeaderPanel.Children.Clear();
            foreach (var tabItem in TabItems)
            {
                TabItemRemoved?.Invoke(tabItem);
            }

            TabItems.Clear();
            LayoutChanged();
        }

        private void OpenTabs_PointerReleased(object sender, PointerReleasedEventArgs e)
        {
            if (e.InitialPressMouseButton != MouseButton.Left) return;
            var menuItems = new List<MenuItem>();
            foreach (var tabItem in TabItems)
            {
                var menuItem = new MenuItem
                {
                    Header = tabItem.Header,
                    Icon = tabItem.ContentIcon
                };
                menuItem.Click += (sender1, e1) =>
                {
                    foreach (var child in HeaderPanel.Children)
                    {
                        ((TabItemHeader)child).Background = tabItem.BackgroundWhenUnFocused;
                        ((TabItemHeader)child).Foreground = tabItem.ForegroundWhenUnFocused;
                    }

                    foreach (var child in ContentPanel.Children)
                    {
                        child.IsVisible = false;
                    }

                    tabIndex = HeaderPanel.Children.IndexOf(tabItem.TabItemHeader());
                    ((TabItemHeader)HeaderPanel.Children[tabIndex]).Background = tabItem.BackgroundWhenFocused;
                    ((TabItemHeader)HeaderPanel.Children[tabIndex]).Foreground = tabItem.ForegroundWhenFocused;
                    var element = (TabItemHeader)HeaderPanel.Children[tabIndex];

                    element.BringIntoView(new Rect(new Size(element.Bounds.Width, element.Bounds.Height)));

                    ContentPanel.Children[tabIndex].IsVisible = true;
                    ContentPanel.Children[tabIndex].Focus();
                    // Set this as the first TabItem in the OpenTabItems list
                    TabItems.Remove(tabItem);
                    TabItems.Insert(0, tabItem);
                };
                menuItems.Add(menuItem);
            }

            var contextMenu = new ContextMenu { Items = menuItems };
            ((Image)sender).ContextMenu = contextMenu;

            contextMenu.Open();
            e.Handled = true;
        }

        /// <summary>
        /// Keeps watch of addition and removal of TabItems to this TabControl
        /// </summary>
        internal void LayoutChanged()
        {
            TabItemsCount = ContentPanel.Children.Count;
            switch (TabItemsCount)
            {
                case 0:
                    IsVisible = AlwaysVisible;

                    if (ParentPanel != null && TabControlParent != null)
                        ParentPanel.Children.Remove(TabControlParent);
                    break;
                case 1:
                    IsVisible = true;
                    if (TabMode == TabMode.Window)
                    {
                        if (AlwaysVisible)
                        {
                            SeparatorBorder.IsVisible = true;
                            TabStrip.IsVisible = true;
                        }
                        else
                        {
                            SeparatorBorder.IsVisible = false;
                            TabStrip.IsVisible = false;
                        }
                    }

                    break;
                default:
                    if (ContentPanel.Children.Count > 1)
                    {
                        IsVisible = true;
                        SeparatorBorder.IsVisible = true;
                        TabStrip.IsVisible = true;
                    }

                    break;
            }

            TabItemsCount = ContentPanel.Children.Count;
        }

        /// <summary>
        /// This event is fired when the NewTabItemButton is clicked
        /// </summary>
        public event Action NewTabItemButtonClicked;

        private void NewTabItem_PointerReleased(object sender, PointerReleasedEventArgs e)
        {
            NewTabItemButtonClicked?.Invoke();
        }
    }
}