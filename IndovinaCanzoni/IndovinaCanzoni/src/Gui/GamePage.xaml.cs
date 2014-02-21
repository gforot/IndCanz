using System.Windows.Navigation;
using GalaSoft.MvvmLight.Messaging;
using IndovinaCanzoni.Messages;
using IndovinaCanzoni.Utils;
using Microsoft.Phone.Controls;

namespace IndovinaCanzoni.src.Gui
{
    public partial class GamePage : PhoneApplicationPage
    {
        public GamePage()
        {
            InitializeComponent();
            Messenger.Default.Register<Message>(this, OnMessageReceived);
        }

        public void OnMessageReceived(Message message)
        {
            switch (message.Key)
            {
                case IndovinaCanzoni.Utils.Messages.RePlay:
                    if (Constants.IsNetworkAvailable)
                    {
                        player.Play();
                    }
                    break;
                case IndovinaCanzoni.Utils.Messages.Stop:
                    if (Constants.IsNetworkAvailable)
                    {
                        player.Stop();
                    }
                    break;
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            Messenger.Default.Send<Message>(new Message(IndovinaCanzoni.Utils.Messages.NewGame));
        }
    }
}