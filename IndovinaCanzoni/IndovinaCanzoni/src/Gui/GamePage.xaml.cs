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
using IndovinaCanzoni.Resources;

namespace IndovinaCanzoni.src.Gui
{
    public partial class GamePage : PhoneApplicationPage
    {
        private GamePageViewModel _vm;

        public GamePage()
        {
            InitializeComponent();

            //creazione viewModel
            _vm = new GamePageViewModel();
            _vm.PlayRequested += _vm_PlayRequested;
            _vm.StopRequested += _vm_StopRequested;
            
            //imposto il DataContext
            DataContext = _vm;

            BuildLocalizedApplicationBar();
        }

        private void BuildLocalizedApplicationBar()
        {
            // Set the page's ApplicationBar to a new instance of ApplicationBar.
            ApplicationBar = new ApplicationBar();

            // Create a new button and set the text value to the localized string from AppResources.
            ApplicationBarIconButton aboutButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/questionmark.png", UriKind.Relative));
            aboutButton.Text = AppResources.AppBarAboutButtonTxt;
            aboutButton.Click += aboutButton_Click;
            ApplicationBar.Buttons.Add(aboutButton);

            //// Create a new menu item with the localized string from AppResources.
            //ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
            //ApplicationBar.MenuItems.Add(appBarMenuItem);
        }


        private void _vm_StopRequested(object sender, EventArgs e)
        {
            player.Stop();
        }

        private void _vm_PlayRequested(object sender, EventArgs e)
        {
            player.Play();
        }

        private void aboutButton_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/src/Gui/AboutPage.xaml", UriKind.Relative));
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            await _vm.Init();
        }
    }
}