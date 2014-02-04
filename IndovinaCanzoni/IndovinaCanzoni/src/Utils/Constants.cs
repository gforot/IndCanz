using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndovinaCanzoni.Utils
{
    class Constants
    {
        public const bool IsNetworkAvailable = false;

        #region Applications Ids
        public const string ApplicationId = "IndovinaCanzoni";
        public const string ClientId = "27b11ebf882bc5888af6a279a75b59a0";
        public const string ClientSecret = "1JKaDB3sRtNgk32Am/xXMIxSSWfQJ6Uz2Ugr+DPUnjHBeD10GXw5v0rbYlZ6MUKd";
        #endregion

        #region Punteggio

        public const int GuessSongPoints = 1000;
        public const int GuessAuthorPoints = 500;
        public const int HintDecreasePoints = 100;

        #endregion
    }
}
