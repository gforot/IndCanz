using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using IndovinaCanzoni.Model;
using Nokia.Music.Types;
using IndovinaCanzoni.Messages;
using Cimbalino.Phone.Toolkit.Services;
using IndovinaCanzoni.Utils;


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
    public class MainViewModel : ViewModelCommon
    {
        #region Properties

        //lista dei Generi disponibili.
        public ObservableCollection<Genre> Genres { get; private set; }

        private const string _selectedGenrePrpName = "CSelectedGenre";
        private Genre _cselectedGenre;
        public Genre CSelectedGenre
        {
            get
            {
                return _cselectedGenre;
            }
            set
            {
                _cselectedGenre = value;
                RaisePropertyChanged(_selectedGenrePrpName);
                SelectGenreCommand.RaiseCanExecuteChanged();
            }
        }


        #region User

        public string User
        {
            get
            {
                return IndovinaCanzoni.App.User;
            }
            set
            {
                IndovinaCanzoni.App.User = value;
                RaisePropertyChanged("User");
            }
        }

        #endregion

        #region IsNetworkAvailable

        private const string _isNetworkAvailablePrpName = "IsNetworkAvailable";

        private bool _isNetworkAvailable;
        public bool IsNetworkAvailable
        {
            get 
            {
                if (IsInDesignMode)
                {
                    return false;
                }
                return _isNetworkAvailable; 
            }
            set
            {
                _isNetworkAvailable = value;
                RaisePropertyChanged(_isNetworkAvailablePrpName);
            }
        }

        #endregion

        #endregion

        #region Commands

        #region SelectGenre
        public RelayCommand SelectGenreCommand { get; private set; }

        private bool CanSelectGenre()
        {
            return _cselectedGenre != null;
        }

        private void SelectGenre()
        {
            IndovinaCanzoni.App.SaveScoreItems();

            IndovinaCanzoni.App.SelectedGenre = CSelectedGenre;

            IndovinaCanzoni.App.HighScores.SetHighscores(Data.DataLayer.GetInstance().LoadScoreItems(CSelectedGenre.Id));

            //Navigazione
            NavigationService.NavigateTo(new Uri(PageAddresses.ScoresPage, UriKind.Relative));
        }
        #endregion

        #region CheckConnection

        public RelayCommand CheckConnectionCommand { get; private set; }

        #endregion

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(INavigationService navigationService)
            : base (navigationService)
        {
            CheckConnection();

            Genres = new ObservableCollection<Genre>();
            SelectGenreCommand = new RelayCommand(SelectGenre, CanSelectGenre);
            CheckConnectionCommand = new RelayCommand(CheckConnection);

            MessengerInstance.Register<Message>(this, OnMessageReceived);
            MessengerInstance.Send<Message>(new Message(IndovinaCanzoni.Utils.Messages.GetGenres));
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

        private async void OnMessageReceived(Message message)
        {
            if (message.Key == IndovinaCanzoni.Utils.Messages.GetGenres)
            {
                await GetGenres();
            }
        }

        private void CheckConnection()
        {
            IsNetworkAvailable = Microsoft.Phone.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable();
        }
    }
}