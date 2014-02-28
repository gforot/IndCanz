using System.Windows;
using GalaSoft.MvvmLight.Command;
using IndovinaCanzoni.ViewModel;

namespace IndovinaCanzoni.ViewModel
{
    public class AttributionPageViewModel : ViewModelCommon
    {
        public string NokiaMixRadioAddress { get; private set; }

        #region Commands

        public RelayCommand ExitCommand { get; private set; }

        private void Exit()
        {
            Application.Current.Terminate();
        }

        #endregion

        public AttributionPageViewModel()
        {
            NokiaMixRadioAddress = "http://www.windowsphone.com/it-it/store/app/nokia-mixradio/f5874252-1f04-4c3f-a335-4fa3b7b85329";
            ExitCommand = new RelayCommand(Exit);
        }
    }
}
