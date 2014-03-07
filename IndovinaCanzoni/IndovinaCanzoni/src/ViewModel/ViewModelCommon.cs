using System;
using Cimbalino.Phone.Toolkit.Helpers;
using Cimbalino.Phone.Toolkit.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using IndovinaCanzoni.Utils;

namespace IndovinaCanzoni.ViewModel
{
    public abstract class ViewModelCommon : ViewModelBase
    {
        #region Commands
        public RelayCommand ClearResultsCommand { get; private set; }
        public RelayCommand ShowAttributionPageCommand { get; private set; }
        public RelayCommand AboutCommand { get; private set; }
        #endregion

        protected IEmailComposeService EmailComposerService;
        protected ApplicationManifest ApplicationManifest;
        protected IMarketplaceReviewService MarketplaceReviewService;
        protected INavigationService NavigationService;

        protected ViewModelCommon(IEmailComposeService emailComposerService,
            IApplicationManifestService applicationManifestService,
            IMarketplaceReviewService marketplaceReviewService)
            : this()
        {
            EmailComposerService = emailComposerService;
            ApplicationManifest = applicationManifestService.GetApplicationManifest();
            MarketplaceReviewService = marketplaceReviewService;
        }

        protected ViewModelCommon(INavigationService navigationService)
            : this()
        {
            NavigationService = navigationService;
        }

        protected ViewModelCommon()
        {
            ClearResultsCommand = new RelayCommand(ClearResults);
            ShowAttributionPageCommand = new RelayCommand(ShowAttributionPage);
            AboutCommand = new RelayCommand(About);
        }

        private void ClearResults()
        {
            Data.DataLayer.GetInstance().ClearResults();
            if (App.SelectedGenre != null)
            {
                IndovinaCanzoni.App.HighScores.SetHighscores(Data.DataLayer.GetInstance().LoadScoreItems(App.SelectedGenre.Id));
            }

            RaisePropertyChanged("HighScores");
        }

        private void ShowAttributionPage()
        {
            App.SaveScoreItems();
            NavigationService.NavigateTo(new Uri(PageAddresses.AttributionPage, UriKind.Relative));
        }

        private void About()
        {
            NavigationService.NavigateTo(new Uri(PageAddresses.AboutPage, UriKind.Relative));
        }

        #region SelectedGenre
        private const string _designModeSelectedGenre = "Musica leggera/Musica vecchia";
        public string SelectedGenre
        {
            get
            {
                if (IsInDesignMode)
                {
                    return _designModeSelectedGenre;
                }
                return App.SelectedGenre.Name;
            }
        }
        #endregion
    }
}
