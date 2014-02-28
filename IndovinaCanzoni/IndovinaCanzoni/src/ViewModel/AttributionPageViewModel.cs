using System.Windows;
using GalaSoft.MvvmLight.Command;
using IndovinaCanzoni.ViewModel;

namespace IndovinaCanzoni.ViewModel
{
    public class AttributionPageViewModel : ViewModelCommon
    {
        #region Commands

        public RelayCommand ExitCommand { get; private set; }

        private void Exit()
        {
            Application.Current.Terminate();
        }

        #endregion

        public AttributionPageViewModel()
        {
            ExitCommand = new RelayCommand(Exit);
        }
    }
}
