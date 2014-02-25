using System;
using System.Collections.ObjectModel;
using Cimbalino.Phone.Toolkit.Services;
using GalaSoft.MvvmLight.Command;
using IndovinaCanzoni.Model;
using IndovinaCanzoni.Utils;

namespace IndovinaCanzoni.ViewModel
{
    public class ScoresPageViewModel : ViewModelCommon
    {
        #region Properties
        public static ObservableCollection<ScoreItem> HighScores { get; private set; }
        #endregion

        #region Command
        public RelayCommand PlayCommand { get; private set; }

        private bool CanPlay()
        {
            return true;
        }

        private void Play()
        {
            NavigationService.NavigateTo(new Uri(PageAddresses.GamePage, UriKind.Relative));
        }

        public RelayCommand AboutCommand { get; private set; }

        private void About()
        {
            NavigationService.NavigateTo(new Uri(PageAddresses.AboutPage, UriKind.Relative));
        }
        #endregion

        #region Constructor

        public ScoresPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            PlayCommand = new RelayCommand(Play, CanPlay);
            AboutCommand = new RelayCommand(About);

            HighScores = IndovinaCanzoni.App.HighScores;
        }
        #endregion
    }
}
