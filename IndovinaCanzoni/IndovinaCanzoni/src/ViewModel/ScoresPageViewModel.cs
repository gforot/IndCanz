using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Cimbalino.Phone.Toolkit.Services;
using GalaSoft.MvvmLight.Command;
using IndovinaCanzoni.Model;
using IndovinaCanzoni.Utils;
using System.Windows;

namespace IndovinaCanzoni.ViewModel
{
    public class ScoresPageViewModel : ViewModelCommon
    {
        #region Properties
        public static List<ScoreItem> HighScores
        {
            get 
            { 
                return App.HighScores.Scores; 
            }
        }
        #endregion

        #region Commands
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


        public RelayCommand ExitCommand { get; private set; }

        private void Exit()
        {
            App.SaveScoreItems();

            NavigationService.NavigateTo(new Uri(PageAddresses.AttributionPage, UriKind.Relative));
            MessageBox.Show("Mettere qui la attribution");
            
        }
        #endregion

        #region Constructor

        public ScoresPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            PlayCommand = new RelayCommand(Play, CanPlay);
            AboutCommand = new RelayCommand(About);
            ExitCommand = new RelayCommand(Exit);
        }
        #endregion
    }
}
