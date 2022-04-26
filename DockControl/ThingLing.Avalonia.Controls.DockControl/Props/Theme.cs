using Avalonia.Media;

namespace ThingLing.Controls.Props
{
    public class Theme
    {
        /// <summary>
        /// Provides a background color to the heading of the selected floating and docked windows.
        /// </summary>
        public Brush? SelectedWindowHeadingBackground { get; set; }

        /// <summary>
        /// Provides a font color to the heading of the selected floating and docked windows.
        /// </summary>
        public Brush? SelectedWindowHeadingForeground { get; set; }

        /// <summary>
        /// Provides a background color to the heading of the selected floating and docked windows.
        /// </summary>
        public Brush? UnSelectedWindowHeadingBackground { get; set; }

        /// <summary>
        /// Provides a background color to the heading of the unselected floating and docked windows.
        /// </summary>
        public Brush? UnSelectedWindowHeadingForeground { get; set; }

        /// <summary>
        /// Provides a background color to the heading of the unselected floating and docked windows.
        /// </summary>
        public Brush? WindowBackground { get; set; }

        #region TabControl theme

        /// <summary>
        /// Holds the default Background color of the TabItem header when it is focused
        /// </summary>
        public Brush? FocusedTabItemBackground { get; set; }

        /// <summary>
        /// Holds the default font color of the TabItem header when it is focused
        /// </summary>
        public Brush? FocusedTabItemForeground { get; set; }

        /// <summary>
        /// Holds the default Background color of the TabItem header when it is not focused
        /// </summary>
        public Brush? UnFocusedTabItemBackground { get; set; }

        /// <summary>
        /// Holds the default font color of the TabItem header when it is not focused
        /// </summary>
        public Brush? UnFocusedTabItemForeground { get; set; }

        /// <summary>
        /// Holds the default font color of the SeparatorBorder
        /// </summary>
        public Brush? SeparatorBorder { get; set; }

        /// <summary>
        /// Holds the default font color of the TabControl background
        /// </summary>
        public Brush? TabControlBackground { get; set; }

        /// <summary>
        /// Holds the default background color of the TabItem Body 
        /// </summary>
        public Brush? TabItemBodyBackground { get; set; }

        /// <summary>
        /// Holds the default font color of the TabItem Body
        /// </summary>
        public Brush? TabItemBodyForeground { get; set; }

        #endregion TabControl
    }
}
