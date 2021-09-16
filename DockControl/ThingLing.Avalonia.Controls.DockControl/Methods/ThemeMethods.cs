using Avalonia.Media;
using ThingLing.Controls.Props;

namespace ThingLing.Controls.Methods
{
    public class ThemeMethods
    {
        public void LoadTheme(Theme? theme = null)
        {
            if (theme == null)
            {
                CurrentTheme.SelectedWindowHeadingBackground = (Brush?)Brushes.MidnightBlue;
                CurrentTheme.SelectedWindowHeadingForeground = (Brush?)Brushes.Silver;
                CurrentTheme.UnSelectedWindowHeadingBackground = (Brush?)Brushes.DarkSlateGray;
                CurrentTheme.UnSelectedWindowHeadingForeground = (Brush?)Brushes.White;
                CurrentTheme.WindowBackground = (Brush?)Brushes.Azure;

                //TabControl
                CurrentTheme.FocusedTabItemBackground = (Brush?)Brushes.Teal;
                CurrentTheme.FocusedTabItemForeground = (Brush?)Brushes.Tan;
                CurrentTheme.UnFocusedTabItemBackground = (Brush?)Brushes.CadetBlue;
                CurrentTheme.UnFocusedTabItemForeground = (Brush?)Brushes.BurlyWood;
                CurrentTheme.SeparatorBorder = (Brush?)Brushes.Teal;
                CurrentTheme.TabItemBodyBackground = (Brush?)Brushes.SeaGreen;
                CurrentTheme.TabItemBodyForeground = (Brush?)Brushes.PeachPuff;
            }
            else
            {
                CurrentTheme.SelectedWindowHeadingBackground = theme.SelectedWindowHeadingBackground;
                CurrentTheme.SelectedWindowHeadingForeground = theme.SelectedWindowHeadingForeground;
                CurrentTheme.UnSelectedWindowHeadingBackground = theme.UnSelectedWindowHeadingBackground;
                CurrentTheme.UnSelectedWindowHeadingForeground = theme.UnSelectedWindowHeadingForeground;
                CurrentTheme.WindowBackground = theme.WindowBackground;

                //TabControl
                CurrentTheme.FocusedTabItemBackground = theme.FocusedTabItemBackground;
                CurrentTheme.FocusedTabItemForeground = theme.FocusedTabItemForeground;
                CurrentTheme.UnFocusedTabItemBackground = theme.UnFocusedTabItemBackground;
                CurrentTheme.UnFocusedTabItemForeground = theme.UnFocusedTabItemForeground;
                CurrentTheme.SeparatorBorder = theme.SeparatorBorder;
                CurrentTheme.TabItemBodyBackground = theme.TabItemBodyBackground;
                CurrentTheme.TabItemBodyForeground = theme.TabItemBodyForeground;
            }
        }

        public static TabControlTheme TabControlTheme()
        {
            var tabControlTheme = new TabControlTheme()
            {
                SeparatorBorder = CurrentTheme.SeparatorBorder,
                TabItemBodyBackground = CurrentTheme.TabItemBodyBackground,
                TabItemBodyForeground = CurrentTheme.TabItemBodyForeground,
                FocusedTabItemBackground = CurrentTheme.FocusedTabItemBackground,
                FocusedTabItemForeground = CurrentTheme.FocusedTabItemForeground,
                UnFocusedTabItemBackground = CurrentTheme.UnFocusedTabItemBackground,
                UnFocusedTabItemForeground = CurrentTheme.UnFocusedTabItemForeground
            };
            return tabControlTheme;
        }


    }
}
