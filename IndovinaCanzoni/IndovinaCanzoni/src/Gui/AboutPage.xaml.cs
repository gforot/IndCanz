using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Cimbalino.Phone.Toolkit.Helpers;
using Cimbalino.Phone.Toolkit.Services;
using IndovinaCanzoni.Resources;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace IndovinaCanzoni.src.Gui
{
    public partial class AboutPage : PhoneApplicationPage
    {
        /// <summary>
        /// The application manifest.
        /// </summary>
        private readonly ApplicationManifest _applicationManifest;

        /// <summary>
        /// The email compose service.
        /// </summary>
        private readonly IEmailComposeService _emailComposeService;

        public AboutPage()
        {
            InitializeComponent();
        }

        private void BuildLocalizedApplicationBar()
        {
            // Set the page's ApplicationBar to a new instance of ApplicationBar.
            ApplicationBar = new ApplicationBar();

            // Create a new button and set the text value to the localized string from AppResources.
            ApplicationBarIconButton feedbackButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/sendFeedback.png", UriKind.Relative));
            feedbackButton.Text = AppResources.AppBarFeedbackTxt;
            feedbackButton.Click += feedbackButton_Click;
            ApplicationBar.Buttons.Add(feedbackButton);
        }

        void feedbackButton_Click(object sender, EventArgs e)
        {
            const string to = "gforot@hotmail.it";
            const string subject = "My Feedback";
            var body = string.Format(
                "Application {0}\n Version: {1}",
                _applicationManifest.App.Title,
                _applicationManifest.App.Version);
            _emailComposeService.Show(to, subject, body);
        }
    }
}