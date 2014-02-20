using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cimbalino.Phone.Toolkit.Services;
using GalaSoft.MvvmLight.Command;
using IndovinaCanzoni.Messages;
using IndovinaCanzoni.Model;
using Microsoft.Phone.Shell;

namespace IndovinaCanzoni.ViewModel
{
    public class ResultPageViewModel : ViewModelCommon
    {

        private ScoreItem _score;

        public int Score 
        { 
            get 
            {
                if (IsInDesignMode)
                {
                    return 5800;
                }
                return _score.Score; 
            } 
        }

        #region Commands
        public RelayCommand NewGameCommand { get; private set; }
        public RelayCommand SeeScoresCommand { get; private set; }

        private void NewGame()
        {
            ResetCurrentScore();
            MessengerInstance.Send<Message>(new Message(IndovinaCanzoni.Utils.Messages.NewGame));
            NavigationService.NavigateTo("/src/Gui/GamePage.xaml");
        }

        private void SeeScores()
        {
            ResetCurrentScore();
            NavigationService.NavigateTo("/src/Gui/ScoresPage.xaml");
        }

        #endregion

        #region Constructor

        public ResultPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            NewGameCommand = new RelayCommand(NewGame);
            SeeScoresCommand = new RelayCommand(SeeScores);

            _score = App.CurrentScore;
        }

        #endregion

        private void ResetCurrentScore()
        {
            App.CurrentScore = null;
        }
    }
}
