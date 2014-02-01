using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using IndovinaCanzoni.Resources;
using Nokia.Music.Tasks;
using Nokia.Music;
using IndovinaCanzoni.Utils;
using Nokia.Music.Types;
using System.Globalization;

namespace IndovinaCanzoni
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            //LaunchTask task = new LaunchTask();
            //task.Show();
            //MusicClient client = new MusicClient(Constants.ClientId, "it","it");
            MusicClient client = new MusicClient(Constants.ClientId);
            ListResponse<MusicItem> result = await client.SearchAsync("love");
            int count = -100;
            if (result.Result != null) 
            {
                count = result.Result.Count;
            }
            result = await client.SearchAsync("Green Day");
            if (result.Result != null)
            {
                count = result.Result.Count;
            }
            result = await client.SearchAsync("Ligabue");
            if (result.Result != null)
            {
                count = result.Result.Count;
            }
            result = await client.SearchAsync("U2");
            if (result.Result != null)
            {
                count = result.Result.Count;
            }

            var artists = await client.GetTopArtistsAsync();
        }

        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
    }
}