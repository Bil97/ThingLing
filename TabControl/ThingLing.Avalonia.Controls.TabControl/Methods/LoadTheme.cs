using Avalonia.Media;
using ThingLing.Controls.Props;

namespace ThingLing.Controls.Methods
{
    internal class LoadTheme
    {
        public LoadTheme(TabControlTheme theme = null)
        {
            if (theme == null)
            {
                CurrentTheme.FocusedTabItemBackground = new SolidColorBrush(Colors.Teal);
                CurrentTheme.FocusedTabItemForeground = new SolidColorBrush(Colors.Tan);
                CurrentTheme.UnFocusedTabItemBackground = new SolidColorBrush(Colors.CadetBlue);
                CurrentTheme.UnFocusedTabItemForeground = new SolidColorBrush(Colors.BurlyWood);
                CurrentTheme.SeparatorBorderBrush = new SolidColorBrush(Colors.Teal);
                CurrentTheme.TabControlBackground = new SolidColorBrush(Colors.LightBlue);
                CurrentTheme.TabItemBodyBackground = new SolidColorBrush(Colors.SeaGreen);
                CurrentTheme.TabItemBodyForeground = new SolidColorBrush(Colors.PeachPuff);
            }
            else
            {
                CurrentTheme.FocusedTabItemBackground = theme.FocusedTabItemBackground;
                CurrentTheme.FocusedTabItemForeground = theme.FocusedTabItemForeground;
                CurrentTheme.UnFocusedTabItemBackground = theme.UnFocusedTabItemBackground;
                CurrentTheme.UnFocusedTabItemForeground = theme.UnFocusedTabItemForeground;
                CurrentTheme.SeparatorBorderBrush = theme.SeparatorBorder;
                CurrentTheme.TabControlBackground = theme.TabControlBackground;
                CurrentTheme.TabItemBodyBackground = theme.TabItemBodyBackground;
                CurrentTheme.TabItemBodyForeground = theme.TabItemBodyForeground;
            }
        }
    }
}
