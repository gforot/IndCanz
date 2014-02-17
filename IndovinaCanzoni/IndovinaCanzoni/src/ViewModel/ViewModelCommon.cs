using Cimbalino.Phone.Toolkit.Helpers;
using Cimbalino.Phone.Toolkit.Services;
using GalaSoft.MvvmLight;

namespace IndovinaCanzoni.ViewModel
{
    public abstract class ViewModelCommon : ViewModelBase
    {
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

    }
}
