﻿namespace IndovinaCanzoni.Utils
{
    public class Constants
    {
        public static bool IsFirstUsageOfApp = true;

        /// <summary>
        /// True se si vuole testare la effettiva connessione con MixRadio
        /// Altrimenti false.
        /// </summary>
        public const bool IsNetworkAvailable = true;

        public const string FeedbackEmailAddress = "gforot@hotmail.it";

        #region Applications Ids
        public const string ApplicationId = "IndovinaCanzoni";
        public const string ClientId = "27b11ebf882bc5888af6a279a75b59a0";
        public const string ClientSecret = "1JKaDB3sRtNgk32Am/xXMIxSSWfQJ6Uz2Ugr+DPUnjHBeD10GXw5v0rbYlZ6MUKd";
        #endregion

        #region Punteggio
        public const int GuessTitlePoints = 60;
        public const int GuessArtistPoints = 40;
        public const int HintDecreasePoints = 10;
        #endregion



        public const string ResultsFolderName = "results";

        public const int NumberOfSongsPerMatch = 10;
    }
}
