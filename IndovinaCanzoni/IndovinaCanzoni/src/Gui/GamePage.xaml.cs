using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using IndovinaCanzoni.ViewModel;

namespace IndovinaCanzoni.src.Gui
{
    public partial class GamePage : PhoneApplicationPage
    {
        private GamePageViewModel _vm;

        public GamePage()
        {
            InitializeComponent();

            _vm = new GamePageViewModel();
            _vm.PlayRequested += _vm_PlayRequested;
            DataContext = _vm;
        }

        private void _vm_PlayRequested(object sender, EventArgs e)
        {
            player.Play();
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            await _vm.Init();
        }
    }
}