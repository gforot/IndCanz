namespace IndovinaCanzoni.Model
{
    public class ArtistResponse: ResponseBase
    {
        private const string _artistPrpName = "Artist";
        private string _artist;
        public string Artist
        {
            get { return _artist; }
            set
            {
                _artist = value;
                RaisePropertyChanged(_artistPrpName);
            }
        }

        public ArtistResponse(string artist, bool isCorrect)
            : base(isCorrect)
        {
            _artist = artist;
        }
    }
}
