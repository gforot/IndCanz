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
            ScoreItems = new ObservableCollection<ScoreItem>();

            PlayCommand = new RelayCommand(Play, CanPlay);
        }
        #endregion
    }
}
