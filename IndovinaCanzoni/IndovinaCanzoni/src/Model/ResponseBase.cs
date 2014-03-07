using GalaSoft.MvvmLight;

namespace IndovinaCanzoni.Model
{
    public abstract class ResponseBase : ViewModelBase
    {
        //IsCorrect
        //IsSelected
        private const string _isSelectedPrpName = "IsSelected";
        private bool _isSelected;
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                RaisePropertyChanged(_isSelectedPrpName);
            }
        }

        private const string _isCorrectPrpName = "IsCorrect";
        private bool _isCorrect;
        public bool IsCorrect
        {
            get { return _isCorrect; }
        }

        public ResponseBase(bool isCorrect)
        {
            _isCorrect = isCorrect;
            _isSelected = false;
        }

        public ResponseBase()
            : this(false)
        {
            ResetValue();
        }

        public abstract void ResetValue();
    }
}
