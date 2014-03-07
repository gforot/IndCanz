using System;
using System.Linq;
using System.Windows;
using Cimbalino.Phone.Toolkit.Services;
using IndovinaCanzoni.Resources;
using IndovinaCanzoni.Utils;
using IndovinaCanzoni.ViewModel;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace IndovinaCanzoni
{
    public partial class MainPage : PhoneApplicationPage
    {
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

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            NavigationService.Navigate(new Uri(PageAddresses.AttributionPage, UriKind.Relative));
        }
    }
}