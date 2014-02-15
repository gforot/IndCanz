
using Cimbalino.Phone.Toolkit.Helpers;
using Cimbalino.Phone.Toolkit.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using IndovinaCanzoni.Utils;

namespace IndovinaCanzoni.ViewModel
{
    public class AboutPageViewModel : ViewModelBase
    {
        public RelayCommand SendFeedbackCommand { get; private set; }

        /// <summary>
        /// The application manifest.
        /// </summary>
        private readonly ApplicationManifest _applicationManifest;

        /// <summary>
        /// The marketplace review service.
        /// </summary>
        private readonly IMarketplaceReviewService _marketplaceReviewService;

        /// <summary>
        /// The email compose service.
        /// </summary>
        private readonly IEmailComposeService _emailComposeService;

        public AboutPageViewModel(IEmailComposeService emailComposerService,
            IApplicationManifestService applicationManifestService,
            IMarketplaceReviewService marketplaceReviewService)
        {
            SendFeedbackCommand = new RelayCommand(SendFeedback);

            _emailComposeService = emailComposerService;
            _applicationManifest = applicationManifestService.GetApplicationManifest();
            _marketplaceReviewService = marketplaceReviewService;
        }

        private void SendFeedback()
        {
            const string to = Constants.FeedbackEmailAddress;
            const string subject = "My Feedback";
            var body = string.Format(
                "Application {0}\n Version: {1}",
                _applicationManifest.App.Title,
                _applicationManifest.App.Version);
            _emailComposeService.Show(to,subject, body);
        }

        /// <summary>
        /// The rate.
        /// </summary>
        private void Rate()
        {
            _marketplaceReviewService.Show();
        }
        
    }
}
