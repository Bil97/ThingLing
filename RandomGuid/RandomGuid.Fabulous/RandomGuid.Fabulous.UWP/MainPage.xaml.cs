using Windows.Foundation;
using Windows.UI.ViewManagement;

namespace RandomGuid.Fabulous.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();
            LoadApplication(new RandomGuid.Fabulous.App());
        }
    }
}
