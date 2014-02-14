using IndovinaCanzoni.ViewModel;
using Microsoft.Phone.Controls;
using System;
using Microsoft.Phone.Shell;
using IndovinaCanzoni.Resources;

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

        void _vm_PlayRequested(object sender, System.EventArgs e)
        {
            NavigationService.Navigate(new Uri("/src/Gui/GamePage.xaml", UriKind.Relative));
        }

        private void aboutButton_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/src/Gui/AboutPage.xaml", UriKind.Relative));
        }
    }
}