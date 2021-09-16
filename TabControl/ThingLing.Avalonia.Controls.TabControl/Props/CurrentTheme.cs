using Avalonia.Media;

namespace ThingLing.Controls.Props
{
    internal static class CurrentTheme
    {
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
        public static Brush SeparatorBorderBrush { get; set; }

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
    }
}