using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace ThingLing.Controls.InternalControls
{
    public partial class DockingContextMenu : ContextMenu
    {
        public DockingContextMenu()
        {
            InitializeComponent();
        }

        #region Controls
        internal MenuItem? DockTopMenuItem;
        internal MenuItem? DockLeftMenuItem;
        internal MenuItem? DockBottomMenuItem;
        internal MenuItem? DockRightMenuItem;
        internal MenuItem? DockAsDocumentMenuItem;
        internal MenuItem? TabModeSwitch;
        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);

            DockTopMenuItem = this.FindControl<MenuItem>(nameof(DockTopMenuItem));
            DockLeftMenuItem = this.FindControl<MenuItem>(nameof(DockLeftMenuItem));
            DockBottomMenuItem = this.FindControl<MenuItem>(nameof(DockBottomMenuItem));
            DockRightMenuItem = this.FindControl<MenuItem>(nameof(DockRightMenuItem));
            DockAsDocumentMenuItem = this.FindControl<MenuItem>(nameof(DockAsDocumentMenuItem));
            TabModeSwitch = this.FindControl<MenuItem>(nameof(TabModeSwitch));
        }

        #endregion
    }
}
