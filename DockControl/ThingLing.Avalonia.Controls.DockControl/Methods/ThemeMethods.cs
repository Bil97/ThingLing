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
                CurrentTheme.SelectedWindowHeadingBackground = new SolidColorBrush(Colors.MidnightBlue);
                CurrentTheme.SelectedWindowHeadingForeground = new SolidColorBrush(Colors.Silver);
                CurrentTheme.UnSelectedWindowHeadingBackground = new SolidColorBrush(Colors.DarkSlateGray);
                CurrentTheme.UnSelectedWindowHeadingForeground = new SolidColorBrush(Colors.White);
                CurrentTheme.WindowBackground = new SolidColorBrush(Colors.Azure);

                //TabControl
                CurrentTheme.FocusedTabItemBackground = new SolidColorBrush(Colors.Teal);
                CurrentTheme.FocusedTabItemForeground = new SolidColorBrush(Colors.Tan);
                CurrentTheme.UnFocusedTabItemBackground = new SolidColorBrush(Colors.CadetBlue);
                CurrentTheme.UnFocusedTabItemForeground = new SolidColorBrush(Colors.BurlyWood);
                CurrentTheme.SeparatorBorder = new SolidColorBrush(Colors.Teal);
                CurrentTheme.TabItemBodyBackground = new SolidColorBrush(Colors.SeaGreen);
                CurrentTheme.TabItemBodyForeground = new SolidColorBrush(Colors.PeachPuff);
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
