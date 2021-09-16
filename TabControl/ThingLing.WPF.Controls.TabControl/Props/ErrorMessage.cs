using System.Windows;
using System.Windows.Controls;

namespace ThingLing.Controls.Props
{
    internal static class ErrorMessage
    {
        public static UIElement EmptyContent => new TextBlock
        {
            Text = "There is no content to display for this window",
            TextWrapping = TextWrapping.Wrap
        };
    }
}