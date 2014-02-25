using IndovinaCanzoni.Model;
using IndovinaCanzoni.Resources;
using System.Collections.Generic;
namespace IndovinaCanzoni.Utils
{
    class HighScores
    {
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

        public HighScores()
        {

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
