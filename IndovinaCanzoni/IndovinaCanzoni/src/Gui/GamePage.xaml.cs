using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using IndovinaCanzoni.ViewModel;
using IndovinaCanzoni.Resources;
using GalaSoft.MvvmLight.Messaging;
using IndovinaCanzoni.Messages;

namespace IndovinaCanzoni.src.Gui
{
    public partial class GamePage : PhoneApplicationPage
    {
        private GamePageViewModel _vm;

        public GamePage()
        {
            InitializeComponent();
            Messenger.Default.Register<Message>(this, OnMessageReceived);
        }


        public void OnMessageReceived(Message message)
        {
            switch (message.Key)
            {
                case IndovinaCanzoni.Utils.Messages.Play:
                    player.Play();
                    break;
                case IndovinaCanzoni.Utils.Messages.Stop:
                    player.Stop();
                    break;
            }
        }
    }
}