using System.Windows.Navigation;
using GalaSoft.MvvmLight.Messaging;
using IndovinaCanzoni.Messages;
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
                case IndovinaCanzoni.Utils.Messages.Play:
                    player.Play();
                    break;
                case IndovinaCanzoni.Utils.Messages.Stop:
                    player.Stop();
                    break;
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            Messenger.Default.Send<Message>(new Message(IndovinaCanzoni.Utils.Messages.InitProductList));
        }
    }
}