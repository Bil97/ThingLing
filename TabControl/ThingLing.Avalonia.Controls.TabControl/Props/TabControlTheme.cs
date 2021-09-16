using Avalonia.Media;

namespace ThingLing.Controls.Props
{
    public class TabControlTheme
    {
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
    }
}