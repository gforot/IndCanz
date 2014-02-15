using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using IndovinaCanzoni.Model;
using Nokia.Music.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using IndovinaCanzoni.Utils;
using IndovinaCanzoni.Messages;
using Cimbalino.Phone.Toolkit.Services;

namespace IndovinaCanzoni.ViewModel
{
    public class GamePageViewModel : ViewModelBase
    {
        #region Constants
        //durata dell'ascolto del brano.
        //per ora costante. 
        //[TODO] in futuro potrebbe variare in base a "richiesta aiuto", "livello", ecc
        const int _secondsOfTimer = 3;
        #endregion

        private DispatcherTimer _dispatcherTimer;
        private INavigationService _navigationService;
        
        #region Events
        public event EventHandler PlayRequested;
        public event EventHandler StopRequested;
        #endregion

        #region Properties

        public string SelectedGenre
        {
            get { return App.SelectedGenre.Name; }
        }

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

        #region AnswerTitle
        private string _answerTitlePrpName = "AnswerTitle";
        private string _answerTitle;
        public string AnswerTitle
        {
            get { return _answerTitle; }
            set
            {
                _answerTitle = value;
                RaisePropertyChanged(_answerTitlePrpName);
            }
        }
        #endregion

        #region AnswerArtist
        private string _answerArtistPrpName = "AnswerArtist";
        private string _answerArtist;
        public string AnswerArtist
        {
            get { return _answerArtist; }
            set
            {
                _answerArtist = value;
                RaisePropertyChanged(_answerArtistPrpName);
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

        #region ArtistAnswerResult
        private const string _artistAnswerResultPrpName = "ArtistAnswerResult";
        private bool? _artistAnswerResult;

        public bool? ArtistAnswerResult
        {
            get { return _artistAnswerResult; }
            set
            {
                _artistAnswerResult = value;
                RaisePropertyChanged(_artistAnswerResultPrpName);
            }
        }
        #endregion

        #region TitleAnswerResult
        private string _titleAnswerResultPrpName = "TitleAnswerResult";
        private bool? _titleAnswerResult;

        public bool? TitleAnswerResult
        {
            get { return _titleAnswerResult; }
            set
            {
                _titleAnswerResult = value;
                RaisePropertyChanged(_titleAnswerResultPrpName);
            }
        }

        #endregion

        #region Answered

        private const string _answeredPrpName = "Answered";
        private bool _answered;
        public bool Answered
        {
            get { return _answered; }
            set
            {
                _answered = value;
                RaisePropertyChanged(_answeredPrpName);
                AnswerCommand.RaiseCanExecuteChanged();
                MoveNextCommand.RaiseCanExecuteChanged();
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
        #endregion

        #region Command
        #region PlayCommand
        public RelayCommand PlayCommand { get; private set; }

        private bool CanPlay()
        {
            return true;
        }

        private void Play()
        {
            MessengerInstance.Send<PlayMessage>(new PlayMessage());

            if (PlayRequested != null)
            {
                PlayRequested(this, new EventArgs());
                IsPlaying = true;
                _dispatcherTimer.Start();
            }
        }

        public RelayCommand AboutCommand { get; private set; }

        private void About()
        {
            _navigationService.NavigateTo(new Uri("/src/Gui/AboutPage.xaml", UriKind.Relative));
        }
        #endregion

        #region AnswerCommand
        public RelayCommand AnswerCommand { get; private set; }
        private bool CanAnswer()
        {
            return !_answered;
        }

        private void Answer()
        {
            //gestione della risposta dell'utente
            System.Windows.MessageBox.Show(string.Format("L'utente ha risposto : {0} - {1}", _answerTitle, _answerArtist));

            Answered = true;

            ArtistAnswerResult = CurrentProduct.Name.Equals(_answerTitle);
            TitleAnswerResult = CurrentProduct.Performers[0].Name.Equals(_answerArtist);

            #region Punteggio
            if ((ArtistAnswerResult.HasValue) && (TitleAnswerResult.HasValue))
            {
                if (ArtistAnswerResult.Value && TitleAnswerResult.Value)
                {
                    //entrambe le risposte corrette
                    CurrentScore += Constants.GuessBothPoints;
                }
                else if (ArtistAnswerResult.Value)
                {
                    //solo artista
                    CurrentScore += Constants.GuessAuthorPoints;
                }
                else if (TitleAnswerResult.Value)
                {
                    //solo titolo
                    CurrentScore += Constants.GuessTitlePoints;
                }
            }
            #endregion
        }
        #endregion

        #region NextCommand

        public RelayCommand MoveNextCommand { get; set; }

        private bool CanMoveNext()
        {
            return _answered;
        }

        private void MoveNext()
        {
            //mi sposto sulla canzone successiva.
            Index++;

            AnswerArtist = string.Empty;
            AnswerTitle = string.Empty;

            ArtistAnswerResult = null;
            TitleAnswerResult = null;

            Answered = false;
        }

        #endregion

        #endregion

        public GamePageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            PlayCommand = new RelayCommand(Play, CanPlay);
            MoveNextCommand = new RelayCommand(MoveNext, CanMoveNext);
            AnswerCommand = new RelayCommand(Answer, CanAnswer);

            _dispatcherTimer = new DispatcherTimer();
            _dispatcherTimer.Interval = new TimeSpan(0, 0, _secondsOfTimer);
            _dispatcherTimer.Tick += _dispatcherTimer_Tick;

            _index = 0;

            _answerArtist = string.Empty;
            _answerTitle = string.Empty;

            _isPlaying = false;
            _answered = false;

            _currentScore = 0;
        }

        #region Timer
        private void _dispatcherTimer_Tick(object sender, EventArgs e)
        {
            _dispatcherTimer.Stop();
            IsPlaying = false;
            if (StopRequested != null)
            {
                StopRequested(this, new EventArgs());
            }
        }
        #endregion

        public async Task Init()
        {
            //Leggo i prodotti relativi al genere selezionato.
            Products = await MusicClientAPI.GetInstance().GetListOfTracksByGenreAsync(App.SelectedGenre);
        }
    }
}
