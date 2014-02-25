
using GalaSoft.MvvmLight;
using IndovinaCanzoni.Utils;
namespace IndovinaCanzoni.Model
{
    public class ScoreItem : ViewModelBase
    {
        #region Properties
        #region Score
        private const string _scorePrpName = "Score";
        public int Score
        {
            get
            {
                return GuessedArtists * Constants.GuessArtistPoints +
                    GuessedTitles * Constants.GuessTitlePoints;
            }
        }
        private void RaiseScoreChanged()
        {
            RaisePropertyChanged(_scorePrpName);
        }
        #endregion
        #region GuessedArtists
        private string _guessedArtistsPrpName = "GuessedArtists";
        private int _guessedArtists;
        public int GuessedArtists
        {
            get { return _guessedArtists; }
            set
            {
                _guessedArtists = value;
                RaisePropertyChanged(_guessedArtistsPrpName);
                RaiseScoreChanged();
            }
        }

        #endregion
        #region GuessedTitles
        private string _guessedTitlesPrpName = "GuessedTitles";
        private int _guessedTitles;
        public int GuessedTitles
        {
            get { return _guessedTitles; }
            set
            {
                _guessedTitles = value;
                RaisePropertyChanged(_guessedTitlesPrpName);
                RaiseScoreChanged();
            }
        }
        #endregion
        #region User
        private string _userPrpName = "User";
        private string _user;
        public string User
        {
            get { return _user; }
            set
            {
                _user = value;
                RaisePropertyChanged(_userPrpName);
            }
        }
        #endregion
        #endregion

        public ScoreItem()
        {

        }
    }
}
