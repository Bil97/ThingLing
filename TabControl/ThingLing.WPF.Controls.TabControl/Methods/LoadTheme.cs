using System.Windows.Media;
using ThingLing.Controls.Props;

namespace ThingLing.Controls.Methods
{
    internal class LoadTheme
    {
        public LoadTheme(TabControlTheme theme = null)
        {
            if (theme == null)
            {
                CurrentTheme.FocusedTabItemBackground = Brushes.Teal;
                CurrentTheme.FocusedTabItemForeground = Brushes.Tan;
                CurrentTheme.UnFocusedTabItemBackground = Brushes.CadetBlue;
                CurrentTheme.UnFocusedTabItemForeground = Brushes.BurlyWood;
                CurrentTheme.SeparatorBorderBrush = Brushes.Teal;
                CurrentTheme.TabControlBackground = Brushes.LightBlue;
                CurrentTheme.TabItemBodyBackground = Brushes.SeaGreen;
                CurrentTheme.TabItemBodyForeground = Brushes.PeachPuff;
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
