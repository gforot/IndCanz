using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using IndovinaCanzoni.Model;

namespace IndovinaCanzoni.ViewModel
{
    public class ScoresPageViewModel : ViewModelBase
    {
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

        }
        #endregion

        #region Constructor

        public ScoresPageViewModel()
        {
            Data.DataLayer.GetInstance().SaveScoreItems(App.SelectedGenre.Id, new ObservableCollection<ScoreItem>()
            {
                new ScoreItem()
                {
                    GuessedTitles = 2, GuessedBoth=1, GuessedAuthors = 3
                },
                new ScoreItem()
                {
                    GuessedTitles = 4, GuessedBoth=2, GuessedAuthors = 1
                }
            });

            //ScoreItems = new ObservableCollection<ScoreItem>();
            ScoreItems = Data.DataLayer.GetInstance().LoadScoreItems(App.SelectedGenre.Id);
            PlayCommand = new RelayCommand(Play, CanPlay);
        }
        #endregion
    }
}
