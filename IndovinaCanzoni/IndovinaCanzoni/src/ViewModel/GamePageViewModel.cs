using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Threading;
using Cimbalino.Phone.Toolkit.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using IndovinaCanzoni.Data;
using IndovinaCanzoni.Messages;
using IndovinaCanzoni.Model;
using IndovinaCanzoni.Utils;
using Microsoft.Phone.Shell;
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

        public int GuessedBoth{get;set;}
        public int GuessedAuthors{get;set;}
        public int GuessedTitle{get;set;}
        

        public bool IsGameFinished
        {
            get { return _numberOfAnswers >= 10; }
        }
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
            MessengerInstance.Send<Message>(new Message(IndovinaCanzoni.Utils.Messages.Play));
            IsPlaying = true;
            _dispatcherTimer.Start();
        }

        public RelayCommand AboutCommand { get; private set; }

        private void About()
        {
            NavigationService.NavigateTo(new Uri("/src/Gui/AboutPage.xaml", UriKind.Relative));
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
            Answered = true;

            ArtistAnswerResult = CurrentProduct.Name.Equals(_answerTitle);
            TitleAnswerResult = CurrentProduct.Performers[0].Name.Equals(_answerArtist);

            TitleAnswerResult = true;
            #region Punteggio
            if ((ArtistAnswerResult.HasValue) && (TitleAnswerResult.HasValue))
            {
                if (ArtistAnswerResult.Value && TitleAnswerResult.Value)
                {
                    GuessedBoth++;
                    //entrambe le risposte corrette
                    CurrentScore += Constants.GuessBothPoints;
                }
                else if (ArtistAnswerResult.Value)
                {
                    //solo artista
                    GuessedAuthors++;
                    CurrentScore += Constants.GuessAuthorPoints;
                }
                else if (TitleAnswerResult.Value)
                {
                    GuessedTitle++;
                    //solo titolo
                    CurrentScore += Constants.GuessTitlePoints;
                }
            }
            #endregion

            _numberOfAnswers++;

            if (IsGameFinished)
            {
                ScoreItem newScore = new ScoreItem()
                {
                    GuessedAuthors = GuessedAuthors,
                    GuessedBoth = GuessedBoth,
                    GuessedTitles = GuessedTitle
                };
                ScoresPageViewModel.ScoreItems.Add(newScore);

                DataLayer.GetInstance().SaveScoreItems(App.SelectedGenre.Id, ScoresPageViewModel.ScoreItems);

                PhoneApplicationService.Current.State.Add("scoreItems", newScore);
                NavigationService.NavigateTo(new Uri("/src/Gui/ResultPage.xaml", UriKind.Relative));
            }
        }
        #endregion

        #region NextCommand

        public RelayCommand MoveNextCommand { get; set; }

        private bool CanMoveNext()
        {
            return _answered && !IsGameFinished;
        }

        private void MoveNext()
        {
            //mi sposto sulla canzone successiva.
            GetNextIndex();

            AnswerArtist = string.Empty;
            AnswerTitle = string.Empty;

            ArtistAnswerResult = null;
            TitleAnswerResult = null;

            Answered = false;
        }

        #endregion

        #endregion

        public GamePageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
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
            _numberOfAnswers = 0;

            MessengerInstance.Register<Message>(this, OnMessageReceived);
            _availableProductIndexes = new List<int>();
            _randomizer = new Random();
            GuessedTitle = 0;
            GuessedAuthors = 0;
            GuessedBoth = 0;
       }

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
            AnswerArtist = string.Empty;
            AnswerTitle = string.Empty;
            ArtistAnswerResult = null;
            TitleAnswerResult = null;
            GuessedTitle = 0;
            GuessedAuthors = 0;
            GuessedBoth = 0;

            Products = await MusicClientAPI.GetInstance().GetListOfTracksByGenreAsync(App.SelectedGenre);

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
            return Index;
        }

    }
}
