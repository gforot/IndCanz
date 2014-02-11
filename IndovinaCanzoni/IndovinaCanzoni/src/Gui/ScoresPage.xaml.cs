using IndovinaCanzoni.ViewModel;
using Microsoft.Phone.Controls;

namespace IndovinaCanzoni.src.Gui
{
    public partial class ScoresPage : PhoneApplicationPage
    {
        private ScoresPageViewModel _vm;

        public ScoresPage()
        {
            InitializeComponent();

            //creo il viewModel
            _vm = new ScoresPageViewModel();
            DataContext = _vm;
        }
    }
}