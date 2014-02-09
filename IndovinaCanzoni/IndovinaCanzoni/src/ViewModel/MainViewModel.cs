using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
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
        private Genre _selectedGenre;

        public ObservableCollection<Genre> Genres { get; private set; }
        public Genre SelectedGenre {
            get { return _selectedGenre; }
            set
            {
                _selectedGenre = value;
                RaisePropertyChanged("SelectedGenre");
            }
        }

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            if (IsInDesignMode)
            {
                // Code runs in Blend --> create design time data.
                Genres = new ObservableCollection<Genre>
                {
                    new Genre(){Id = "1", Name="Rock"},
                    new Genre(){Id = "2", Name="Pop"},
                    new Genre(){Id="3", Name="Classica"},
                    new Genre(){Id = "4", Name="Metal"}
                };
            }
            else
            {
                // Code runs "for real"
                //Genres = new ObservableCollection<Genre>(MusicClientAPI.GetInstance().Genres);
            }

            //_selectedGenre = Genres[2];
        }


    }
}