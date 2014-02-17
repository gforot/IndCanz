/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:IndovinaCanzoni"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using Cimbalino.Phone.Toolkit.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;

namespace IndovinaCanzoni.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            if (!SimpleIoc.Default.IsRegistered<IMarketplaceReviewService>())
            {
                SimpleIoc.Default.Register<IMarketplaceReviewService, MarketplaceReviewService>();
            }

            if (!SimpleIoc.Default.IsRegistered<IApplicationManifestService>())
            {
                SimpleIoc.Default.Register<IApplicationManifestService, ApplicationManifestService>();
            }

            if (!SimpleIoc.Default.IsRegistered<IEmailComposeService>())
            {
                SimpleIoc.Default.Register<IEmailComposeService, EmailComposeService>();
            }

            if (!SimpleIoc.Default.IsRegistered<INavigationService>())
            {
                SimpleIoc.Default.Register<INavigationService, NavigationService>();
            }

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<GamePageViewModel>();
            SimpleIoc.Default.Register<AboutPageViewModel>();
            SimpleIoc.Default.Register<ScoresPageViewModel>();
            SimpleIoc.Default.Register<ResultPageViewModel>();
        }

        /// <summary>
        /// Gets the about view model.
        /// </summary>
        public AboutPageViewModel AboutPageViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<AboutPageViewModel>();
            }
        }

        public MainViewModel MainViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        public GamePageViewModel GamePageViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<GamePageViewModel>();
            }
        }


        public ScoresPageViewModel ScoresPageViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ScoresPageViewModel>();
            }
        }

        public ResultPageViewModel ResultPageViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ResultPageViewModel>();
            }
        }
        
        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}