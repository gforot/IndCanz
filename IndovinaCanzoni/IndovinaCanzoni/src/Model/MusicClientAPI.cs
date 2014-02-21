using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IndovinaCanzoni.Utils;
using Nokia.Music;
using Nokia.Music.Types;

namespace IndovinaCanzoni.Model
{
    public class MusicClientAPI
    {
        private MusicClient _musicClient;
        private CountryResolver _countryResolver;

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
            if (Constants.IsNetworkAvailable)
            {
                _countryResolver = new CountryResolver(Constants.ClientId);
                _musicClient = new MusicClient(Constants.ClientId);
            }
        }

        /// <summary>
        /// Get the List of available Genres
        /// </summary>
        /// <returns></returns>
        public async Task<List<Genre>> GetListOfGenresAsync()
        {
            if (_musicClient != null)
            {
                return new List<Genre>(await _musicClient.GetGenresAsync());
            }
            return new List<Genre>
            {
                new Genre(){Id = "1", Name="Rock"},
                new Genre(){Id = "2", Name="Pop"},
                new Genre(){Id = "3", Name="Classica"},
                new Genre(){Id = "4", Name="Metal"}
            };
        }

        public async Task<List<Product>> GetListOfTracksByGenreAsync(Genre genre)
        {
            if (_musicClient != null)
            {
                return new List<Product>(await _musicClient.GetTopProductsForGenreAsync(genre, Category.Track, 0, 100));
            }
            else
            {
                List<Product> prs = new List<Product>()
                {
                    new Product 
                    { 
                        Id = "La vita è adesso", 
                        Performers = new Artist[]{new Artist(){Name = "Baglioni"}}
                    },
                    new Product 
                    { 
                        Id = "Una vita da Mediano", 
                        Performers = new Artist[]{new Artist(){Name = "Ligabue"}}
                    },
                    new Product
                    { 
                        Id = "Aint it fun", 
                        Performers = new Artist[]{new Artist(){Name = "Guns'n'roses"}}
                    },
                    new Product
                    { 
                        Id = "A te", 
                        Performers = new Artist[]{new Artist(){Name = "Jovanotti"}}
                    },
                    new Product
                    { 
                        Id = "November Rain", 
                        Performers = new Artist[]{new Artist(){Name = "Guns'n'roses"}}
                    },
                    new Product
                    { 
                        Id = "Ten years gone", 
                        Performers = new Artist[]{new Artist(){Name = "Led Zeppelin"}}
                    },
                    new Product
                    { 
                        Id = "Zia Luisa", 
                        Performers = new Artist[]{new Artist(){Name = "Van de sfroos"}}
                    },
                    new Product
                    { 
                        Id = "Please please me", 
                        Performers = new Artist[]{new Artist(){Name = "Beatles"}}
                    },
                    new Product
                    { 
                        Id = "I don't want to miss a thing", 
                        Performers = new Artist[]{new Artist(){Name = "Armageddon"}}
                    },
                    new Product
                    { 
                        Id = "Set fire to the rain", 
                        Performers = new Artist[]{new Artist(){Name = "Adele"}}
                    },
                    new Product
                    { 
                        Id = "Back in black", 
                        Performers = new Artist[]{new Artist(){Name = "ACDC"}}
                    },
                    new Product
                    { 
                        Id = "Through the never", 
                        Performers = new Artist[]{new Artist(){Name = "Metallica"}}
                    },
                    new Product
                    { 
                        Id = "Smells like teen spirits", 
                        Performers = new Artist[]{new Artist(){Name = "Nirvana"}}
                    },
                    new Product
                    { 
                        Id = "Lightning bolt", 
                        Performers = new Artist[]{new Artist(){Name = "Pearl Jam"}}
                    },
                    new Product
                    { 
                        Id = "Brain damage", 
                        Performers = new Artist[]{new Artist(){Name = "Pink Floyd"}}
                    },
                    new Product
                    { 
                        Id = "Zooropa", 
                        Performers = new Artist[]{new Artist(){Name = "U2"}}
                    },
                };

                foreach (Product pr in prs)
                {
                    pr.Name = pr.Id;
                }

                return prs;
            }

        }

        public Uri GetTrackSampleUri(string trackId)
        {
            if (_musicClient != null)
            {
                return _musicClient.GetTrackSampleUri(trackId);
            }
            else 
            {
                return null;
            }
        }
    }
}
