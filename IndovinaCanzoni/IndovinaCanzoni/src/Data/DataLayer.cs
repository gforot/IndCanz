
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.IO.IsolatedStorage;
using System.Xml.Serialization;
using IndovinaCanzoni.Model;
using IndovinaCanzoni.Utils;
namespace IndovinaCanzoni.Data
{
    public class DataLayer
    {
        private static DataLayer _instance;
        public static DataLayer GetInstance()
        {
            return _instance ?? (_instance = new DataLayer());
        }

        public void ReleaseInstance()
        {
            _instance = null;
        }

        private DataLayer()
        {

        }

        /// <summary>
        /// Get the score "table" of the given genre
        /// </summary>
        /// <param name="idGenre"></param>
        /// <returns></returns>
        public ObservableCollection<ScoreItem> LoadScoreItems(string idGenre)
        {
            ObservableCollection<ScoreItem> items = new ObservableCollection<ScoreItem>();
            using (IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForApplication())
            {
                if (isf.FileExists(idGenre))
                {
                    using (IsolatedStorageFileStream isfs = isf.OpenFile(idGenre, FileMode.Open))
                    {
                        Type t = typeof(ObservableCollection<ScoreItem>);
                        XmlSerializer ser = new XmlSerializer(t);
                        object obj = ser.Deserialize(isfs);

                        if ((obj != null) && (obj is ObservableCollection<ScoreItem>))
                        {
                            items = obj as ObservableCollection<ScoreItem>;
                        }
                    }
                }
            }
            return items;
        }

        public void SaveScoreItems(string idGenre, ObservableCollection<ScoreItem> items)
        {
            using (var store = IsolatedStorageFile.GetUserStoreForApplication())
            {
                using (var stream = new IsolatedStorageFileStream(idGenre,
                                                                  FileMode.Create,
                                                                  FileAccess.Write,
                                                                  store))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<ScoreItem>));
                    serializer.Serialize(stream, items);
                }
            }
        }
    }
}
