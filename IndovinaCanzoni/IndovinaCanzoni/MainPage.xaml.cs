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
            ShellTile oTile = ShellTile.ActiveTiles.First();
            FlipTileData oFliptile = new FlipTileData();

            

            #region Front
            oFliptile.Title = AppResources.ApplicationTitle;

            oFliptile.SmallBackgroundImage = new Uri("Assets/Tiles/radio_159_159.png", UriKind.Relative);
            oFliptile.BackgroundImage = new Uri("Assets/Tiles/radio_336_336.png", UriKind.Relative);
            oFliptile.WideBackgroundImage = new Uri("Assets/Tiles/radio_691_336.png", UriKind.Relative);
            #endregion

            #region Back
            oFliptile.BackBackgroundImage = new Uri("/Assets/Tiles/radio_336_336.png", UriKind.Relative);
            oFliptile.WideBackBackgroundImage = new Uri("/Assets/Tiles/radio_691_336.png", UriKind.Relative);

            oFliptile.BackTitle = AppResources.ApplicationTitle;

            oFliptile.BackContent = AppResources.BackTileMessage;
            oFliptile.WideBackContent = AppResources.BackTileWideMessage;
            #endregion

            #region Common
            oFliptile.Count = 11;
            #endregion






            oTile.Update(oFliptile);
        }
        #endregion

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            NavigationService.Navigate(new Uri(PageAddresses.AttributionPage, UriKind.Relative));
        }
    }
}