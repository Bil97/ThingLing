using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace ThingLing.Controls.InternalControls
{
    public partial class ContentControl : UserControl
    {
        TabControl DocumentWindow;
        public ContentControl()
        {
            InitializeComponent();

            DocumentWindow = new();
            //DocumentWindow = new(ThemeMethods.TabControlTheme());
            //DocumentWindow.;
            ContentPanel!.Children.Add(DocumentWindow);
        }

        #region Controls
        internal Grid? ContentPanel;
        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);

            ContentPanel = this.FindControl<Grid>(nameof(ContentPanel));
        }

#endregion  
        public void AddDocument(string header, string contentPath, Control content, Image? contentIcon = null)
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
