using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IndovinaCanzoni.Utils;
using Nokia.Music;
using Nokia.Music.Types;

namespace IndovinaCanzoni.Model
{
    public  class MusicClientAPI
    {
        private MusicClient _musicClient;
        private CountryResolver _countryResolver;
        public bool IsServiceAvailable { get; private set; }
        public IList <Genre> Genres { get; private set; }

        #region Singleton Instance
        private static MusicClientAPI _instance;
        public static MusicClientAPI GetInstance()
        {
            return _instance ?? (_instance = new MusicClientAPI());
        }

        public static void ReleaseInstance()
        {
            _instance = null;
        }
        #endregion

        private MusicClientAPI()
        {
            _countryResolver = new CountryResolver(Constants.ClientId);
            _musicClient = new MusicClient(Constants.ClientId);
        }

        public async Task Init()
        {
            string countryCode = RegionInfo.CurrentRegion.TwoLetterISORegionName.ToLower();
            Genres = await GetListOfGenresAsync();
            //IsServiceAvailable = await _countryResolver.CheckAvailabilityAsync(countryCode);
            //if (IsServiceAvailable)
            //{
            //    Genres = await GetListOfGenresAsync();
            //}
            
        }

        public async Task<IList<Genre>> GetListOfGenresAsync()
        {
            if (Constants.IsNetworkAvailable)
            {
                return await _musicClient.GetGenresAsync();
            }
            return new List<Genre>
            {
                new Genre(){Id = "1", Name="Rock"},
                new Genre(){Id = "2", Name="Pop"},
                new Genre(){Id = "3", Name="Classica"},
                new Genre(){Id = "4", Name="Metal"}
            };
        }
    }
}
