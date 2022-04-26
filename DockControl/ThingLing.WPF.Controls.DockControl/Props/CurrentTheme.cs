using System.Windows.Media;

namespace ThingLing.Controls.Props
{
    internal static class CurrentTheme
    {
        /// <summary>
        /// Provides a background color to the heading of the selected floating and docked windows.
        /// </summary>
        public static Brush SelectedWindowHeadingBackground { get; set; }

        /// <summary>
        /// Provides a font color to the heading of the selected floating and docked windows.
        /// </summary>
        public static Brush SelectedWindowHeadingForeground { get; set; }

        /// <summary>
        /// Provides a background color to the heading of the selected floating and docked windows.
        /// </summary>
        public static Brush UnSelectedWindowHeadingBackground { get; set; }

        /// <summary>
        /// Provides a background color to the heading of the unselected floating and docked windows.
        /// </summary>
        public static Brush UnSelectedWindowHeadingForeground { get; set; }

        /// <summary>
        /// Provides a background color to the heading of the unselected floating and docked windows.
        /// </summary>
        public static Brush WindowBackground { get; set; }

        #region TabControl
        /// <summary>
        /// Holds the default Background color of the TabItem header when it is focused
        /// </summary>
        public static Brush FocusedTabItemBackground { get; set; }

        /// <summary>
        /// Holds the default font color of the TabItem header when it is focused
        /// </summary>
        public static Brush FocusedTabItemForeground { get; set; }

        /// <summary>
        /// Holds the default Background color of the TabItem header when it is not focused
        /// </summary>
        public static Brush UnFocusedTabItemBackground { get; set; }

        /// <summary>
        /// Holds the default font color of the TabItem header when it is not focused
        /// </summary>
        public static Brush UnFocusedTabItemForeground { get; set; }

        /// <summary>
        /// Holds the default font color of the SeparatorBorder
        /// </summary>
        public static Brush SeparatorBorder { get; set; }

        /// <summary>
        /// Holds the default font color of the TabControl background
        /// </summary>
        public static Brush TabControlBackground { get; set; }

        /// <summary>
        /// Holds the default background color of the TabItem Body 
        /// </summary>
        public static Brush TabItemBodyBackground { get; set; }

        /// <summary>
        /// Holds the default font color of the TabItem Body
        /// </summary>
        public static Brush TabItemBodyForeground { get; set; }

        #endregion TabControl
    }
}
