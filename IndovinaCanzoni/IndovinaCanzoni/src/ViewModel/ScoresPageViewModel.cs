using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using IndovinaCanzoni.Model;
using System;
using Cimbalino.Phone.Toolkit.Services;

namespace IndovinaCanzoni.ViewModel
{
    public class ScoresPageViewModel : ViewModelBase
    {
        private INavigationService _navigationService;

        #region Properties

        public ObservableCollection<ScoreItem> ScoreItems { get; private set; }

        #endregion

        #region Command
        public RelayCommand PlayCommand { get; private set; }

        private bool CanPlay()
        {
            return true;
        }

        private void Play()
        {
            _navigationService.NavigateTo(new Uri("/src/Gui/GamePage.xaml", UriKind.Relative));
        }

        public RelayCommand AboutCommand { get; private set; }

        private void About()
        {
            _navigationService.NavigateTo(new Uri("/src/Gui/AboutPage.xaml", UriKind.Relative));
        }
        #endregion

        #region Constructor

        public ScoresPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            ScoreItems = Data.DataLayer.GetInstance().LoadScoreItems(App.SelectedGenre.Id);
            PlayCommand = new RelayCommand(Play, CanPlay);
            AboutCommand = new RelayCommand(About);
        }
        #endregion
    }
}
