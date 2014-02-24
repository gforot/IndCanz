using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Threading;
using Cimbalino.Phone.Toolkit.Services;
using GalaSoft.MvvmLight.Command;
using IndovinaCanzoni.Data;
using IndovinaCanzoni.Messages;
using IndovinaCanzoni.Model;
using IndovinaCanzoni.src.Utils;
using IndovinaCanzoni.Utils;
using Nokia.Music.Types;

namespace IndovinaCanzoni.ViewModel
{
    public class GamePageViewModel : ViewModelCommon
    {
        #region Constants
        //durata dell'ascolto del brano.
        //per ora costante. 
        //[TODO] in futuro potrebbe variare in base a "richiesta aiuto", "livello", ecc
        const int _secondsOfTimer = 3;
        #endregion

        private readonly List<int> _availableProductIndexes;
        private DispatcherTimer _dispatcherTimer;
        private readonly Random _randomizer;
        private int _numberOfAnswers;
        private List<Artist> _artists;
        
        #region Properties

        #region Title1
        private const string _title1PrpName = "Title1";
        private TitleResponse _title1;
        public TitleResponse Title1
        {
            get { return _title1; }
            set
            {
                _title1 = value;
                RaisePropertyChanged(_title1PrpName);
            }
        }

        #endregion

        #region Title1State
        private const string _title1StatePrpName = "Title1State";
        public ResponseState Title1State { get { return GetTitleState(Title1); } }
        #endregion

        #region Title2State
        private const string _title2StatePrpName = "Title2State";
        public ResponseState Title2State { get { return GetTitleState(Title2); } }
        #endregion

        #region Title3State
        private const string _title3StatePrpName = "Title3State";
        public ResponseState Title3State { get { return GetTitleState(Title3); } }
        #endregion

        #region Artist1State
        private const string _artist1StatePrpName = "Artist1State";
        public ResponseState Artist1State { get { return GetArtistState(Artist1); } }
        #endregion

        #region Artist2State
        private const string _artist2StatePrpName = "Artist2State";
        public ResponseState Artist2State { get { return GetArtistState(Artist2); } }
        #endregion

        #region Title3State
        private const string _artist3StatePrpName = "Artist3State";
        public ResponseState Artist3State { get { return GetArtistState(Artist3); } }
        #endregion

        private ResponseState GetTitleState(ResponseBase response)
        {
            if (!TitleAnswerDone)
            {
                return ResponseState.Normal;
            }
            if (response.IsCorrect)
            {
                return ResponseState.Correct;
            }
            if (response.IsSelected)
            {
                return ResponseState.Wrong;
            }
            return ResponseState.Normal;
            
        }

        private ResponseState GetArtistState(ResponseBase response)
        {
            if (!ArtistAnswerDone)
            {
                return ResponseState.Normal;
            }
            if (response.IsCorrect)
            {
                return ResponseState.Correct;
            }
            if (response.IsSelected)
            {
                return ResponseState.Wrong;
            }
            return ResponseState.Normal;

        }

        #region Title2
        private const string _title2PrpName = "Title2";
        private TitleResponse _title2;
        public TitleResponse Title2
        {
            get { return _title2; }
            set
            {
                _title2 = value;
                RaisePropertyChanged(_title2PrpName);
            }
        }

        #endregion

        #region Title3
        private const string _title3PrpName = "Title3";
        private TitleResponse _title3;
        public TitleResponse Title3
        {
            get { return _title3; }
            set
            {
                _title3 = value;
                RaisePropertyChanged(_title3PrpName);
            }
        }

        #endregion

        #region Artist1

        private const string _artist1PrpName = "Artist1";
        private ArtistResponse _artist1;
        public ArtistResponse Artist1
        {
            get { return _artist1; }
            set
            {
                _artist1 = value;
                RaisePropertyChanged(_artist1PrpName);
            }
        }

        #endregion

        #region Artist2

        private const string _artist2PrpName = "Artist2";
        private ArtistResponse _artist2;
        public ArtistResponse Artist2
        {
            get { return _artist2; }
            set
            {
                _artist2 = value;
                RaisePropertyChanged(_artist2PrpName);
            }
        }

        #endregion

        #region Artist3

        private const string _artist3PrpName = "Artist3";
        private ArtistResponse _artist3;
        public ArtistResponse Artist3
        {
            get { return _artist3; }
            set
            {
                _artist3 = value;
                RaisePropertyChanged(_artist3PrpName);
            }
        }

        #endregion

        #region SelectedGenre
        public string SelectedGenre
        {
            get { return App.SelectedGenre.Name; }
        }
        #endregion

        #region Products
        private const string _productsPrpName = "Products";

        private List<Product> _products;
        public List<Product> Products
        {
            get { return _products; }
            set
            {
                _products = value;
                RaisePropertyChanged(_currentProductPrpName);
                RaisePropertyChanged(_sourceUriPrpName);
                RaisePropertyChanged(_productsPrpName);
            }
        }

        #endregion

        #region Index
        private string _indexPrpName = "Index";
        private int _index;

        public int Index
        {
            get { return _index; }
            set
            {
                _index = value;
                RaisePropertyChanged(_indexPrpName);
                RaisePropertyChanged(_sourceUriPrpName);
                RaisePropertyChanged(_currentProductPrpName);
            }
        }

        #endregion

        #region IsPlaying

        private string _isPlayingPrpName = "IsPlaying";
        private bool _isPlaying;
        public bool IsPlaying
        {
            get { return _isPlaying; }
            set
            {
                _isPlaying = value;
                RaisePropertyChanged(_isPlayingPrpName);
            }
        }
        
        #endregion

        #region CurrentProduct

        private const string _currentProductPrpName = "CurrentProduct";
        public Product CurrentProduct
        {
            get { return _products[_index]; }
        }

        #endregion

        #region SourceUri
        private string _sourceUriPrpName = "SourceUri";
        public Uri SourceUri
        {
            get
            {
                if ((_products == null) || (_products.Count <= _index))
                {
                    return null;
                }
                return MusicClientAPI.GetInstance().GetTrackSampleUri(_products[_index].Id);
            }
        }
        #endregion

        #region ArtistAnswerDone

        private const string _artistAnswerDonePrpName = "ArtistAnswerDone";

        private bool _artistAnswerDone;

        public bool ArtistAnswerDone
        {
            get { return _artistAnswerDone; }
            set
            {
                _artistAnswerDone = value;
                RaisePropertyChanged(_artistAnswerDonePrpName);
                RaisePropertyChanged(_artist1StatePrpName);
                RaisePropertyChanged(_artist2StatePrpName);
                RaisePropertyChanged(_artist3StatePrpName);

                ArtistResponseCommand.RaiseCanExecuteChanged();
            }
        }

        #endregion

        #region TitleAnswerDone

        private const string _titleAnswerDonePrpName = "TitleAnswerDone";

        private bool _titleAnswerDone;

        public bool TitleAnswerDone
        {
            get { return _titleAnswerDone; }
            set
            {
                _titleAnswerDone = value;
                RaisePropertyChanged(_titleAnswerDonePrpName);
                RaisePropertyChanged(_title1StatePrpName);
                RaisePropertyChanged(_title2StatePrpName);
                RaisePropertyChanged(_title3StatePrpName);

                TitleResponseCommand.RaiseCanExecuteChanged();
            }
        }

        #endregion

        #region CurrentScore

        private const string _currentScorePrpName = "CurrentScore";
        private int _currentScore;

        public int CurrentScore
        {
            get { return _currentScore; }
            set
            {
                _currentScore = value;
                RaisePropertyChanged(_currentScorePrpName);
            }
        }

        #endregion


        public bool IsGameFinished
        {
            get { return _numberOfAnswers >= Constants.NumberOfSongsPerMatch; }
        }


        #endregion

        #region Constructor

        public GamePageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            MessengerInstance.Register<Message>(this, OnMessageReceived);

            PlayCommand = new RelayCommand(Play, CanPlay);
            MoveNextCommand = new RelayCommand(MoveNext, CanMoveNext);
            AboutCommand = new RelayCommand(About);

            ArtistResponseCommand = new RelayCommand<ArtistResponse>((a)=>ArtistResponse(a), (a)=>CanArtistResponse(a));
            TitleResponseCommand = new RelayCommand<TitleResponse>((a) => TitleResponse(a), (a) => CanTitleResponse(a));

            _dispatcherTimer = new DispatcherTimer();
            _dispatcherTimer.Interval = new TimeSpan(0, 0, _secondsOfTimer);
            _dispatcherTimer.Tick += _dispatcherTimer_Tick;

            _randomizer = new Random();
            _availableProductIndexes = new List<int>();

            ResetPropertiesValue();
        }

        #endregion

        #region Command
        #region AboutCommand

        public RelayCommand AboutCommand { get; private set; }

        private void About()
        {
            NavigationService.NavigateTo(new Uri("/src/Gui/AboutPage.xaml", UriKind.Relative));
        }
        #endregion

        #region RePlayCommand
        public RelayCommand PlayCommand { get; private set; }

        private bool CanPlay()
        {
            return !IsPlaying;
        }

        private void Play()
        {
            MessengerInstance.Send<Message>(new Message(IndovinaCanzoni.Utils.Messages.Play));
            IsPlaying = true;
            _dispatcherTimer.Start();
        }


        #endregion

        #region NextCommand

        public RelayCommand MoveNextCommand { get; set; }

        private bool CanMoveNext()
        {
            return !IsGameFinished;
        }

        private void MoveNext()
        {
            //mi sposto sulla canzone successiva.
            GetNextIndex();
        }

        #endregion

        #region TitleResponseCommand

        public RelayCommand<TitleResponse> TitleResponseCommand { get; private set; }

        private bool CanTitleResponse(TitleResponse titleResponse)
        {
            return true;
        }

        private void TitleResponse(TitleResponse titleResponse)
        {
            if (!TitleAnswerDone)
            {
                titleResponse.IsSelected = true;
                TitleAnswerDone = true;
                if (titleResponse.IsCorrect)
                {
                    CurrentScore += Constants.GuessTitlePoints;
                }
            }
        }
        #endregion

        #region ArtistResponseCommand

        public RelayCommand<ArtistResponse> ArtistResponseCommand { get; private set; }

        private bool CanArtistResponse(ArtistResponse artist)
        {
            return true;
        }

        private void ArtistResponse(ArtistResponse artist)
        {
            if (!ArtistAnswerDone)
            {
                artist.IsSelected = true;
                ArtistAnswerDone = true;
                if (artist.IsCorrect)
                {
                    CurrentScore += Constants.GuessArtistPoints;
                }
            }
        }
        #endregion

        #endregion

        #region Timer
        private void _dispatcherTimer_Tick(object sender, EventArgs e)
        {
            MessengerInstance.Send<Message>(new Message(IndovinaCanzoni.Utils.Messages.Stop));
            _dispatcherTimer.Stop();
            IsPlaying = false;
        }
        #endregion

        private async void OnMessageReceived(Message message)
        {
            switch (message.Key)
            {
                case IndovinaCanzoni.Utils.Messages.NewGame:
                    await NewGame();
                    break;
            }
        }

        private async Task NewGame()
        {
            ResetPropertiesValue();

            Products = await MusicClientAPI.GetInstance().GetListOfTracksByGenreAsync(App.SelectedGenre);

            IEnumerable<Artist> artists = from pt 
                          in Products 
                          where pt.Performers.Count()>0 
                          select pt.Performers[0];
            _artists = new List<Artist>(artists.Distinct<Artist>(new ArtistsComparer()));

            _availableProductIndexes.Clear();
            for (int i = 0; i < Products.Count; i++)
            {
                _availableProductIndexes.Add(i);
            }
            GetNextIndex();
        }

        private int GetNextIndex()
        {
            int idx = _randomizer.Next(_availableProductIndexes.Count);
            Index = _availableProductIndexes[idx];
            _availableProductIndexes.RemoveAt(idx);

            List<Product> wrongProducts = GetWrongResponses(CurrentProduct);
            Title1 = new TitleResponse(CurrentProduct.Name, true);
            Title2 = new TitleResponse(wrongProducts[0].Name, false);
            Title3 = new TitleResponse(wrongProducts[1].Name, false);

            List<Artist> wrongArtists = GetWrongArtists(CurrentProduct.Performers[0]);
            Artist1 = new ArtistResponse(CurrentProduct.Performers[0].Name, true);
            Artist2 = new ArtistResponse(wrongArtists[0].Name, false);
            Artist3 = new ArtistResponse(wrongArtists[1].Name, false);

            ArtistAnswerDone = false;
            TitleAnswerDone = false;

            Play();

            return Index;
        }

        private void ResetPropertiesValue()
        {
            _numberOfAnswers = 0;
            _currentScore = 0;
            _index = 0;
            _isPlaying = false;
            if (_availableProductIndexes != null)
            {
                _availableProductIndexes.Clear();
            }
        }

        private List<Product> GetWrongResponses(Product rightResponse)
        {
            if ((Products == null) || (Products.Count < 3))
            {
                return new List<Product>();
            }

            Product w1 = null;
            while (w1 == null)
            {
                int i = _randomizer.Next(Products.Count);
                if (!Products[i].Name.Equals(rightResponse.Name))
                {
                    w1 = Products[i];
                }
            }

            Product w2 = null;
            while (w2 == null)
            {
                int i = _randomizer.Next(Products.Count);
                if ((!Products[i].Name.Equals(rightResponse.Name))&&
                    (!Products[i].Name.Equals(w1.Name)))
                {
                    w2 = Products[i];
                }
            }
            return new List<Product>() { w1, w2 };
        }

        private List<Artist> GetWrongArtists(Artist rightResponse)
        {
            if ((Products == null) || (Products.Count < 3))
            {
                return new List<Artist>();
            }

            Artist a1 = null;
            while (a1 == null)
            {
                int i = _randomizer.Next(Products.Count);
                if (!Products[i].Performers[0].Name.Equals(rightResponse.Name))
                {
                    a1 = Products[i].Performers[0];
                }
            }

            Artist a2 = null;
            while (a2 == null)
            {
                int i = _randomizer.Next(Products.Count);
                if ((!Products[i].Performers[0].Name.Equals(rightResponse.Name)) &&
                    (!Products[i].Performers[0].Name.Equals(a1.Name)))
                {
                    a2 = Products[i].Performers[0];
                }
            }
            return new List<Artist>() { a1, a2 };
        }

        /// <summary>
        /// Torna l'artista selezionato dall'utente
        /// </summary>
        /// <returns></returns>
        private ArtistResponse GetSelectedArtist()
        {
            if (Artist1.IsSelected) return Artist1;
            if (Artist2.IsSelected) return Artist2;
            if (Artist3.IsSelected) return Artist3;
            return null;
        }

        /// <summary>
        /// Torna l'artista selezionato dall'utente
        /// </summary>
        /// <returns></returns>
        private TitleResponse GetSelectedTitle()
        {
            if (Title1.IsSelected) return Title1;
            if (Title2.IsSelected) return Title2;
            if (Title3.IsSelected) return Title3;
            return null;
        }
    }
}
