using System;
using System.Linq;
using System.Windows;
using Cimbalino.Phone.Toolkit.Services;
using IndovinaCanzoni.Resources;
using IndovinaCanzoni.Tiles;
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
            if (Constants.IsFirstUsageOfApp)
            {
                TilesManager.UpdateTiles();
            }
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

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            NavigationService.Navigate(new Uri(PageAddresses.AttributionPage, UriKind.Relative));
        }
    }
}