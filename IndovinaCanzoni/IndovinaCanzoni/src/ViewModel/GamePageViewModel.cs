using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using IndovinaCanzoni.Model;
using Nokia.Music.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndovinaCanzoni.ViewModel
{
    public class GamePageViewModel : ViewModelBase
    {
        public event EventHandler PlayRequested;

        #region Command
        public RelayCommand PlayCommand { get; private set; }

        private bool CanPlay()
        {
            return true;
        }

        private void Play()
        {
            if (PlayRequested != null)
            {
                PlayRequested(this, new EventArgs());
            }
        }
        #endregion



        public GamePageViewModel()
        {
            //recupero la lista di canzoni.
        }

        public async Task Init()
        {
            List<Product> products = await MusicClientAPI.GetInstance().GetListOfProductsByGenreAsync(App.SelectedGenre);
        }
    }
}
