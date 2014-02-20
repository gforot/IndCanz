namespace IndovinaCanzoni.Model
{
    public class TitleResponse : ResponseBase
    {
        private const string _titlePrpName = "Title";
        private string _title;
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                RaisePropertyChanged(_titlePrpName);
            }
        }

        public TitleResponse(string title, bool isCorrect)
            : base(isCorrect)
        {
            Title = title;
        }
    }
}
