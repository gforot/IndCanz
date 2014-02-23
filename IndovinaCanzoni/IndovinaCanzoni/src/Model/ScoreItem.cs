
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
                return GuessedAuthors * Constants.GuessAuthorPoints +
                    GuessedTitles * Constants.GuessTitlePoints;
            }
        }
        private void RaiseScoreChanged()
        {
            RaisePropertyChanged(_scorePrpName);
        }
        #endregion
        #region GuessedAuthors
        private string _guessedAuthorsPrpName = "GuessedAuthors";
        private int _guessedAuthors;
        public int GuessedAuthors
        {
            get { return _guessedAuthors; }
            set
            {
                _guessedAuthors = value;
                RaisePropertyChanged(_guessedAuthorsPrpName);
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
        #endregion

        public ScoreItem()
        {

        }
    }
}
