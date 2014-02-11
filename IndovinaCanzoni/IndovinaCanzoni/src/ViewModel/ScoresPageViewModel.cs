﻿using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using IndovinaCanzoni.Model;
using System;

namespace IndovinaCanzoni.ViewModel
{
    public class ScoresPageViewModel : ViewModelBase
    {

        public event EventHandler PlayRequested;
        #region Properties

        public ObservableCollection<ScoreItem> ScoreItems { get; private set; }

        #endregion

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

        #region Constructor

        public ScoresPageViewModel()
        {
            ScoreItems = Data.DataLayer.GetInstance().LoadScoreItems(App.SelectedGenre.Id);
            PlayCommand = new RelayCommand(Play, CanPlay);
        }
        #endregion
    }
}
