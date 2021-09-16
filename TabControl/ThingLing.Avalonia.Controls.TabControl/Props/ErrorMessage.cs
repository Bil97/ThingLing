using Avalonia.Controls;
using Avalonia.Media;

namespace ThingLing.Controls.Props
{
    internal static class ErrorMessage
    {
        public static Control EmptyContent => new TextBlock
        {
            Text = "There is no content to display for this window",
            TextWrapping = TextWrapping.Wrap
        };
    }
}