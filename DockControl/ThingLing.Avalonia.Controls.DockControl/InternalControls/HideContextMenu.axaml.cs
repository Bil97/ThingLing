using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace ThingLing.Controls.InternalControls
{
    public partial class HideContextMenu : ContextMenu
    {
        public HideContextMenu()
        {
            InitializeComponent();
        }

        #region Controls

        internal MenuItem? AutoHideLeftMenuItem;
        internal MenuItem? AutoHideRightMenuItem;
        internal MenuItem? AutoHideBottomMenuItem;
        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);

            AutoHideLeftMenuItem = this.FindControl<MenuItem>(nameof(AutoHideLeftMenuItem));
            AutoHideRightMenuItem = this.FindControl<MenuItem>(nameof(AutoHideRightMenuItem));
            AutoHideBottomMenuItem = this.FindControl<MenuItem>(nameof(AutoHideBottomMenuItem));
        }

        #endregion
    }
}
