using IndovinaCanzoni.Model;
using IndovinaCanzoni.Resources;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System;
using System.Windows.Data;
using IndovinaCanzoni.Tiles;

namespace IndovinaCanzoni.Utils
{
    public class HighScores
    {
        #region constants
        private const int NumberOfScoresInHighscore = 5;

        private const int GuessedTitle1 = 2;
        private const int GuessedArtist1 = 2;
        private const int GuessedTitle2 = 2;
        private const int GuessedArtist2 = 1;
        private const int GuessedTitle3 = 1;
        private const int GuessedArtist3 = 2;
        private const int GuessedTitle4 = 1;
        private const int GuessedArtist4 = 1;
        private const int GuessedTitle5 = 1;
        private const int GuessedArtist5 = 0;
        #endregion

        private ObservableCollection<ScoreItem> _highScores = new ObservableCollection<ScoreItem>();


        public ObservableCollection<ScoreItem> Scores
        {
            get { return _highScores; }
        }

        public HighScores()
        {
            
        }

        public void SetHighscores(IEnumerable<ScoreItem> highscores)
        {
            _highScores.Clear();
            foreach (ScoreItem item in highscores)
            {
                _highScores.Add(item);
            }
        }

        private void TruncHighscores()
        {
            SortHighscores();

            while(_highScores.Count>NumberOfScoresInHighscore)
            {
                _highScores.RemoveAt(NumberOfScoresInHighscore);
            }
        }

        private void SortHighscores()
        {
            _highScores = new ObservableCollection<ScoreItem>(_highScores.OrderByDescending(x => x.Score));
        }

        private bool IsHighscore(ScoreItem si)
        {
            foreach (ScoreItem sitem in _highScores)
            {
                if (sitem.Score < si.Score)
                {
                    return true;
                }
            }
            return false;
        }

        public void AddScore(ScoreItem si)
        {
            if (!IsHighscore(si))
            {
                return;
            }

            //aggiorno tile
            TilesManager.UpdateTiles(si);

            _highScores.Add(si);
            TruncHighscores();   
        }

        public static List<ScoreItem> CreateDefaultHighScores()
        {
            return new List<ScoreItem>()
            {
                new ScoreItem(){
                    GuessedArtists = GuessedArtist1,
                    GuessedTitles = GuessedTitle1,
                    User = AppResources.HighScoreUser
                },
                                new ScoreItem(){
                    GuessedArtists = GuessedArtist2,
                    GuessedTitles = GuessedTitle2,
                    User = AppResources.HighScoreUser
                },
                                new ScoreItem(){
                    GuessedArtists = GuessedArtist3,
                    GuessedTitles = GuessedTitle3,
                    User = AppResources.HighScoreUser
                },
                                new ScoreItem(){
                    GuessedArtists = GuessedArtist4,
                    GuessedTitles = GuessedTitle4,
                    User = AppResources.HighScoreUser
                },
                                new ScoreItem(){
                    GuessedArtists = GuessedArtist5,
                    GuessedTitles = GuessedTitle5,
                    User = AppResources.HighScoreUser
                },
            };
        }
    }
}
