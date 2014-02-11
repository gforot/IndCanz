using IndovinaCanzoni.ViewModel;
using Microsoft.Phone.Controls;
using System;

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
            _vm.PlayRequested += _vm_PlayRequested;
            DataContext = _vm;
        }

        void _vm_PlayRequested(object sender, System.EventArgs e)
        {
            NavigationService.Navigate(new Uri("/src/Gui/GamePage.xaml", UriKind.Relative));
        }
    }
}