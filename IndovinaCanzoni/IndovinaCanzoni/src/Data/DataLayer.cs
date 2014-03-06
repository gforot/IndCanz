
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.IO.IsolatedStorage;
using System.Xml.Serialization;
using IndovinaCanzoni.Model;
using IndovinaCanzoni.Utils;
using Nokia.Music.Types;
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
        public List<ScoreItem> LoadScoreItems(string idGenre)
        {
            List<ScoreItem> items = new List<ScoreItem>();
            using (IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForApplication())
            {
                string fileName = GetFileName(idGenre);
                if (isf.FileExists(fileName))
                {
                    using (IsolatedStorageFileStream isfs = isf.OpenFile(fileName, FileMode.Open))
                    {
                        Type t = typeof(List<ScoreItem>);
                        XmlSerializer ser = new XmlSerializer(t);
                        object obj = ser.Deserialize(isfs);

                        if ((obj != null) && (obj is List<ScoreItem>))
                        {
                            items = obj as List<ScoreItem>;
                        }
                    }
                }
                else
                {
                    foreach (ScoreItem item in HighScores.CreateDefaultHighScores())
                    {
                        items.Add(item);
                    }
                }
            }
            return items;
        }

        public void SaveScoreItems(string idGenre, List<ScoreItem> items)
        {
            using (var store = IsolatedStorageFile.GetUserStoreForApplication())
            {
                if (!store.DirectoryExists(Constants.ResultsFolderName))
                {
                    store.CreateDirectory(Constants.ResultsFolderName);
                }
                
                using (var stream = new IsolatedStorageFileStream(GetFileName(idGenre),
                                                                  FileMode.Create,
                                                                  FileAccess.Write,
                                                                  store))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(List<ScoreItem>));
                    serializer.Serialize(stream, items);
                }
            }
        }

        private string GetFileName(string idGenre)
        {
            return string.Format("{0}/{1}", Constants.ResultsFolderName, idGenre);
        }

        public void ClearResults()
        {
            using (var store = IsolatedStorageFile.GetUserStoreForApplication())
            {
                if (store.DirectoryExists(Constants.ResultsFolderName))
                {
                    string[] s = store.GetFileNames(string.Format("{0}/*",Constants.ResultsFolderName));
                    foreach (string st in s)
                    {
                        store.DeleteFile(string.Format("{0}/{1}", Constants.ResultsFolderName, st));
                    }
                    store.DeleteDirectory(Constants.ResultsFolderName);

                }
            }
        }
    }
}
