using Cimbalino.Phone.Toolkit.Helpers;
using Cimbalino.Phone.Toolkit.Services;
using GalaSoft.MvvmLight.Command;
using IndovinaCanzoni.Resources;
using IndovinaCanzoni.Utils;

namespace IndovinaCanzoni.ViewModel
{
    public class AboutPageViewModel : ViewModelCommon
    {
        #region Commands
        #region SendFeedback
        public RelayCommand SendFeedbackCommand { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        private void SendFeedback()
        {
            const string to = Constants.FeedbackEmailAddress;
            string subject = AppResources.FeedbackSubject;
            var body = string.Format(
                AppResources.FeedbackText,
                ApplicationManifest.App.Title,
                ApplicationManifest.App.Version);
            base.EmailComposerService.Show(to, subject, body);
        }
        #endregion

        #region Rate
        public RelayCommand RateCommand { get; private set; }

        /// <summary>
        /// Rate the app
        /// </summary>
        private void Rate()
        {
            MarketplaceReviewService.Show();
        }
        #endregion
        #endregion

        #region Constructor
        public AboutPageViewModel(IEmailComposeService emailComposerService,
            IApplicationManifestService applicationManifestService,
            IMarketplaceReviewService marketplaceReviewService)
            : base(emailComposerService, applicationManifestService, marketplaceReviewService)
        {
            SendFeedbackCommand = new RelayCommand(SendFeedback);
            RateCommand = new RelayCommand(Rate);
        }

        #endregion
    }
}
