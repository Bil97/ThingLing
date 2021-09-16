using Avalonia.Controls;
using Avalonia.Media;
using ThingLing.Controls.Props;
using Tool_Tip = Avalonia.Controls.ToolTip;

namespace ThingLing.Controls
{
    /// <summary>
    ///  Represents a selectable item inside a ThingLing.Controls.TabControl.
    /// </summary>
    public class TabItem
    {
        private readonly TabItemHeader _tabItemHeader = new();
        private readonly TabItemBody _tabItemBody = new();
        private Brush _backgroundWhenFocused;
        private Brush _backgroundWhenUnFocused;
        private Brush _foregroundWhenFocused;
        private Brush _foregroundWhenUnFocused;
        private Control _content;
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
                Tool_Tip.SetTip(_tabItemHeader, value);
                Tool_Tip.SetTip(_tabItemBody.TabItemHeader, value);
            }
        }

        /// <summary>
        /// Holds the content displayed in the TabItem body
        /// </summary>
        public Control Content
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
        /// Indicates whether change has occured in the TabItem body. It is show by a * in the TabItem header
        /// </summary>
        public bool ContentChanged
        {
            get => contentChanged;
            set
            {
                contentChanged = value;
                _tabItemHeader.ContentChanged.IsVisible = value;
                _tabItemBody.TabItemHeader.ContentChanged.IsVisible = value;
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
            set => _backgroundWhenUnFocused = value;
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
            Tool_Tip.SetTip(_tabItemHeader, ToolTip);
            _tabItemHeader.ContentChanged.IsVisible = ContentChanged;
            _tabItemHeader.Background = BackgroundWhenFocused;
            _tabItemHeader.Foreground = ForegroundWhenFocused;
            return _tabItemHeader;
        }

        /// <summary>
        /// Holds the content displayed in the TabItem Body
        /// </summary>
        public TabItemBody TabItemBody()
        {
            _tabItemBody.TabItemHeader.ContentIcon = ContentIcon;
            _tabItemBody.TabItemHeader.Header.Text = Header;
            Tool_Tip.SetTip(_tabItemBody, ToolTip);
            _tabItemBody.TabItemHeader.ContentChanged.IsVisible = ContentChanged;
            _tabItemBody.TabItemHeader.Background = BackgroundWhenFocused;
            _tabItemBody.TabItemHeader.Foreground = ForegroundWhenFocused;
            _tabItemBody.Background = TabItemBodyBackground;
            _tabItemBody.Foreground = TabItemBodyForeground;
            return _tabItemBody;
        }
    }
}
