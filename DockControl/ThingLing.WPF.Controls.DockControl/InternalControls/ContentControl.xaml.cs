using System.Windows;
using System.Windows.Controls;
using ThingLing.Controls.Methods;
using TabItem = ThingLing.Controls;
namespace ThingLing.Controls.InternalControls
{
    /// <summary>
    /// Interaction logic for ContentPanel.xaml
    /// </summary>
    internal partial class ContentControl : UserControl
    {
        TabControl DocumentWindow;
        public ContentControl()
        {
            InitializeComponent();

            DocumentWindow = new();
            //DocumentWindow.Theme = ThemeMethods.TabControlTheme();
            //DocumentWindow.;
            ContentPanel.Children.Add(DocumentWindow);
        }

        public void AddDocument(string header, string contentPath, UIElement content, Image contentIcon = null)
        {
            var tabItem = new TabItem
            {
                Header = header,
                ToolTip = contentPath,
                Content = content,
                ContentIcon = contentIcon
            };

            DocumentWindow.Add(tabItem);
        }
    }
}
