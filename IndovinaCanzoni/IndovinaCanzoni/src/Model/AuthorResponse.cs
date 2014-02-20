namespace IndovinaCanzoni.Model
{
    public class AuthorResponse : ResponseBase
    {
        private const string _authorPrpName = "Author";
        private string _author;
        public string Author
        {
            get { return _author; }
            set
            {
                _author = value;
                RaisePropertyChanged(_authorPrpName);
            }
        }

        public AuthorResponse(string title, bool isCorrect)
            : base(isCorrect)
        {
            _author = title;
        }
    }
}
