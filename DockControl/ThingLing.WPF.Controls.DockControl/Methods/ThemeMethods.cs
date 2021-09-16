using System.Windows.Media;
using ThingLing.Controls.Props;

namespace ThingLing.Controls.Methods
{
    public class ThemeMethods
    {
        public void LoadTheme(Theme theme = null)
        {
            if (theme == null)
            {
                CurrentTheme.SelectedWindowHeadingBackground = Brushes.MidnightBlue;
                CurrentTheme.SelectedWindowHeadingForeground = Brushes.Silver;
                CurrentTheme.UnSelectedWindowHeadingBackground = Brushes.DarkSlateGray;
                CurrentTheme.UnSelectedWindowHeadingForeground = Brushes.White;
                CurrentTheme.WindowBackground = Brushes.Azure;

                //TabControl
                CurrentTheme.FocusedTabItemBackground = Brushes.Teal;
                CurrentTheme.FocusedTabItemForeground = Brushes.Tan;
                CurrentTheme.UnFocusedTabItemBackground = Brushes.CadetBlue;
                CurrentTheme.UnFocusedTabItemForeground = Brushes.BurlyWood;
                CurrentTheme.SeparatorBorder = Brushes.Teal;
                CurrentTheme.TabItemBodyBackground = Brushes.SeaGreen;
                CurrentTheme.TabItemBodyForeground = Brushes.PeachPuff;
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
