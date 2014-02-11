using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using IndovinaCanzoni.Model;
using Nokia.Music.Types;

namespace IndovinaCanzoni.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        #region Events

        public event EventHandler GenreSelected;

        #endregion

        #region Properties

        //lista dei Generi disponibili.
        public ObservableCollection<Genre> Genres { get; private set; }

        private const string _selectedGenrePrpName = "SelectedGenre";
        private Genre _selectedGenre;
        public Genre SelectedGenre
        {
            get
            {
                return _selectedGenre;
            }
            set
            {
                _selectedGenre = value;
                RaisePropertyChanged(_selectedGenrePrpName);
                SelectGenreCommand.RaiseCanExecuteChanged();
            }
        }
        #endregion

        #region Commands

        public RelayCommand SelectGenreCommand { get; private set; }

        private bool CanSelectGenre()
        {
            return _selectedGenre != null;
        }

        private void SelectGenre()
        {
            if (GenreSelected != null)
            {
                GenreSelected(this, new EventArgs());    
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            Genres = new ObservableCollection<Genre>();
            SelectGenreCommand = new RelayCommand(SelectGenre, CanSelectGenre);
        }
        #endregion

        #region Methods

        internal async Task GetGenres()
        {
            Genres.Clear();
            List<Genre> tmpGenres = await MusicClientAPI.GetInstance().GetListOfGenresAsync();
            foreach (Genre g in tmpGenres)
            {
                Genres.Add(g);
            }
        }

        #endregion
    }
}