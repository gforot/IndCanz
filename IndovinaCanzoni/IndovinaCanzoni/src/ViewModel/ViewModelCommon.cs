using Cimbalino.Phone.Toolkit.Helpers;
using Cimbalino.Phone.Toolkit.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace IndovinaCanzoni.ViewModel
{
    public abstract class ViewModelCommon : ViewModelBase
    {
        public RelayCommand ClearResultsCommand { get; private set; }
        public RelayCommand ClearResultsOfCurrentGenreCommand { get; private set; }

        protected IEmailComposeService EmailComposerService;
        protected ApplicationManifest ApplicationManifest;
        protected IMarketplaceReviewService MarketplaceReviewService;
        protected INavigationService NavigationService;

        protected ViewModelCommon(IEmailComposeService emailComposerService,
            IApplicationManifestService applicationManifestService,
            IMarketplaceReviewService marketplaceReviewService)
        {
            EmailComposerService = emailComposerService;
            ApplicationManifest = applicationManifestService.GetApplicationManifest();
            MarketplaceReviewService = marketplaceReviewService;
        }

        protected ViewModelCommon(INavigationService navigationService)
        {
            NavigationService = navigationService;
        }

        protected ViewModelCommon()
        {
            ClearResultsCommand = new RelayCommand(ClearResults);
            ClearResultsOfCurrentGenreCommand = new RelayCommand(ClearResultsOfCurrentGenre);
        }

        private void ClearResults()
        {
            Data.DataLayer.GetInstance().ClearResults();
        }

        private void ClearResultsOfCurrentGenre()
        {
            if ((App.SelectedGenre == null) || (string.IsNullOrEmpty(App.SelectedGenre.Id)))
            { 
                return; 
            }
            Data.DataLayer.GetInstance().ClearResults(App.SelectedGenre.Id);
        }

    }
}
