using Microsoft.Phone.Controls;

namespace IndovinaCanzoni.src.Gui
{
    public partial class ResultPage : PhoneApplicationPage
    {
        public ResultPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            NavigationService.RemoveBackEntry();
        }
    }
}