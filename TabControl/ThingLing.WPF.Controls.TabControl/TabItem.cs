using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ThingLing.Controls.Props;

namespace ThingLing.Controls
{
    /// <summary>
    ///  Represents a selectable item inside a ThingLing.Controls.TabControl.
    /// </summary>
    public class TabItem
    {
        private TabItemHeader _tabItemHeader = new TabItemHeader();
        private TabItemBody _tabItemBody = new TabItemBody();
        private Brush _backgroundWhenFocused;
        private Brush _backgroundWhenUnFocused;
        private Brush _foregroundWhenFocused;
        private Brush _foregroundWhenUnFocused;
        private UIElement _content;
        private Brush _tabItemBodyBackground;
        private static Brush _tabItemBodyForeground;
        private string header;
        private Image contentIcon;
        private string toolTip;
        private bool contentChanged;

        /// <summary>
        /// Holds the Title text of the TabItem
        /// </summary>
        public string Header
        {
            get => header;
            set
            {
                header = value;
                _tabItemHeader.Header.Text = value;
                _tabItemBody.TabItemHeader.Header.Text = value;
            }
        }

        /// <summary>
        /// Holds the Icon of the content displayed in the TabItem body
        /// </summary>
        public Image ContentIcon
        {
            get => contentIcon;
            set
            {
                contentIcon = value;
                _tabItemHeader.ContentIcon = value;
                _tabItemBody.TabItemHeader.ContentIcon = value;
            }
        }

        /// <summary>
        /// Holds extra information displayed as tooltip in the TabItem header
        /// </summary>
        public string ToolTip
        {
            get => toolTip;
            set
            {
                toolTip = value;
                _tabItemHeader.ToolTip = value;
                _tabItemBody.TabItemHeader.ToolTip = value;
            }
        }

        /// <summary>
        /// Holds the content displayed in the TabItem body
        /// </summary>
        public UIElement Content
        {
            get => _content;
            set
            {
                _content = value;
                _content.Focusable = true;
                if (_tabItemBody.ContentPanel.Child != null)
                    _tabItemBody.ContentPanel.Child = value;
            }
        }

        /// <summary>
        /// Indicates whether change has occurred in the TabItem body. It is show by a * in the TabItem header
        /// </summary>
        public bool ContentChanged
        {
            get => contentChanged;
            set
            {
                contentChanged = value;
                _tabItemHeader.ContentChanged.Visibility = value ? Visibility.Visible : Visibility.Collapsed;
                _tabItemBody.TabItemHeader.ContentChanged.Visibility = value ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        /// <summary>
        /// Holds the Background color of the TabItem header when it is focused
        /// </summary>
        public Brush BackgroundWhenFocused
        {
            get => _backgroundWhenFocused ??= CurrentTheme.FocusedTabItemBackground;
            set
            {
                _backgroundWhenFocused = value;
                _tabItemHeader.Background = value;
                _tabItemBody.TabItemHeader.Background = value;
            }
        }

        /// <summary>
        /// Holds the Background color of the TabItem header when it is not focused
        /// </summary>
        public Brush BackgroundWhenUnFocused
        {
            get => _backgroundWhenUnFocused ??= CurrentTheme.UnFocusedTabItemBackground;
            set
            {
                _backgroundWhenUnFocused = value;
            }
        }

        /// <summary>
        /// Holds the font color of the TabItem header when it is focused
        /// </summary>
        public Brush ForegroundWhenFocused
        {
            get => _foregroundWhenFocused ??= CurrentTheme.FocusedTabItemForeground;
            set
            {
                _foregroundWhenFocused = value;
                _tabItemHeader.Foreground = value;
                _tabItemBody.TabItemHeader.Foreground = value;
            }
        }

        /// <summary>
        /// Holds the font color of the TabItem header when it is not focused
        /// </summary>
        public Brush ForegroundWhenUnFocused
        {
            get => _foregroundWhenUnFocused ??= CurrentTheme.UnFocusedTabItemForeground;
            set => _foregroundWhenUnFocused = value;
        }

        /// <summary>
        /// Holds the default background color of the TabItem Body 
        /// </summary>
        public Brush TabItemBodyBackground
        {
            get => _tabItemBodyBackground ??= CurrentTheme.TabItemBodyBackground;
            set
            {
                _tabItemBodyBackground = value;
                _tabItemBody.Background = value;
            }
        }

        /// <summary>
        /// Holds the default font color of the TabItem Body
        /// </summary>
        public Brush TabItemBodyForeground
        {
            get => _tabItemBodyForeground ??= CurrentTheme.TabItemBodyForeground;
            set
            {
                _tabItemBodyForeground = value;
                _tabItemBody.Foreground = value;
            }
        }

        /// <summary>
        /// Holds the content displayed in the TabItem Header
        /// </summary>
        public TabItemHeader TabItemHeader()
        {
            _tabItemHeader.ContentIcon = ContentIcon;
            _tabItemHeader.Header.Text = Header;
            _tabItemHeader.Header.ToolTip = ToolTip;
            _tabItemHeader.ContentChanged.Visibility = ContentChanged ? Visibility.Visible : Visibility.Collapsed;
            _tabItemHeader.Background = BackgroundWhenFocused;
            _tabItemHeader.Foreground = ForegroundWhenFocused;
            return _tabItemHeader;
        }

        /// <summary>
        /// Holds the content displayed in the TabItem Body. 
        /// </summary>
        public TabItemBody TabItemBody()
        {
            _tabItemBody.TabItemHeader.ContentIcon = ContentIcon;
            _tabItemBody.TabItemHeader.Header.Text = Header;
            _tabItemBody.TabItemHeader.Header.ToolTip = ToolTip;
            _tabItemBody.TabItemHeader.ContentChanged.Visibility = ContentChanged ? Visibility.Visible : Visibility.Collapsed;
            _tabItemBody.TabItemHeader.Background = BackgroundWhenFocused;
            _tabItemBody.TabItemHeader.Foreground = ForegroundWhenFocused;
            _tabItemBody.Background = TabItemBodyBackground;
            _tabItemBody.Foreground = TabItemBodyForeground;
            return _tabItemBody;
        }
    }
}
