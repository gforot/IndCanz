using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cimbalino.Phone.Toolkit.Services;
using GalaSoft.MvvmLight.Command;
using IndovinaCanzoni.Messages;
using IndovinaCanzoni.Model;
using IndovinaCanzoni.Utils;
using Microsoft.Phone.Shell;

namespace IndovinaCanzoni.ViewModel
{
    public class ResultPageViewModel : ViewModelCommon
    {
        public int Score 
        { 
            get 
            {
                if (IsInDesignMode)
                {
                    return 400;
                }
                return App.CurrentScore == null ? 0 : App.CurrentScore.Score;
            } 
        }

        public int GuessedTitles
        {
            get
            {
                if (IsInDesignMode)
                {
                    return 4;
                }
                return App.CurrentScore == null ? 0 : App.CurrentScore.GuessedTitles;
            }
        }

        public int GuessedArtists
        {
            get
            {
                if (IsInDesignMode)
                {
                    return 4;
                }
                return App.CurrentScore == null ? 0 : App.CurrentScore.GuessedArtists;
            }
        }

        #region Commands
        public RelayCommand NewGameCommand { get; private set; }
        public RelayCommand SeeScoresCommand { get; private set; }

        private void NewGame()
        {
            ResetCurrentScore();
            MessengerInstance.Send<Message>(new Message(IndovinaCanzoni.Utils.Messages.NewGame));
            NavigationService.NavigateTo(PageAddresses.GamePage);
        }

        private void SeeScores()
        {
            ResetCurrentScore();
            NavigationService.NavigateTo(PageAddresses.ScoresPage);
        }

        #endregion

        #region Constructor

        public ResultPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            NewGameCommand = new RelayCommand(NewGame);
            SeeScoresCommand = new RelayCommand(SeeScores);
        }

        #endregion

        private void ResetCurrentScore()
        {
            App.CurrentScore = null;
        }
    }
}
