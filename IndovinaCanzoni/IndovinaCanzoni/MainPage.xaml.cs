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
using IndovinaCanzoni.ViewModel;
using IndovinaCanzoni.Model;

namespace IndovinaCanzoni
{
    public partial class MainPage : PhoneApplicationPage
    {
        MainViewModel _vm;

        // Constructor
        public MainPage()
        {
            InitializeComponent();

            CreateTiles();

            FeedbackOverlay.VisibilityChanged += FeedbackOverlay_VisibilityChanged;
        }

        #region Rate My App
        private void FeedbackOverlay_VisibilityChanged(object sender, EventArgs e)
        {
            //Nascondo la ApplicationBar (se presente) quando viene visualizzato il FeedbackOverlay di RateMyApp
            if (ApplicationBar != null)
            {
                ApplicationBar.IsVisible = (FeedbackOverlay.Visibility != Visibility.Visible);
            }
        }
        #endregion

        #region Tiles
        private void CreateTiles()
        {
            if (Mangopollo.Utils.CanUseLiveTiles)
            {
                var tileId = ShellTile.ActiveTiles.FirstOrDefault();
                if (tileId != null)
                {
                    var tileData = new FlipTileData();
                    tileData.Title = AppResources.ApplicationTitle;
                    tileData.BackContent = "";
                    //tileData.BackgroundImage = new Uri("/Icons/173x173.png", UriKind.Relative);
                    tileData.BackBackgroundImage = new Uri("/Icons/173x173.png", UriKind.Relative);
                    tileData.WideBackContent = "";
                    //tileData.WideBackgroundImage = new Uri("/Icons/346x173.png", UriKind.Relative);
                    tileData.WideBackBackgroundImage = new Uri("/Icons/346x173.png", UriKind.Relative);
                    //Debug.WriteLine("Activating live tile: " + Mangopollo.Utils.CanUseLiveTiles);
                    tileId.Update(tileData);
                }
            }
        }
        #endregion

        //private async void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    //LaunchTask task = new LaunchTask();
        //    //task.Show();
        //    MusicClient client = new MusicClient(Constants.ClientId, "it",null);
        //    //MusicClient client = new MusicClient(Constants.ClientId);



        //    ListResponse<MusicItem> result = await client.SearchAsync("love");
        //    int count = -100;
        //    if (result.Result != null) 
        //    {
        //        count = result.Result.Count;
        //    }
        //    result = await client.SearchAsync("Green Day");
        //    if (result.Result != null)
        //    {
        //        count = result.Result.Count;
        //    }
        //    result = await client.SearchAsync("Ligabue");
        //    if (result.Result != null)
        //    {
        //        count = result.Result.Count;
        //    }
        //    result = await client.SearchAsync("U2");
        //    if (result.Result != null)
        //    {
        //        count = result.Result.Count;
        //    }

        //    var artists = await client.GetTopArtistsAsync();
        //}






    }
}