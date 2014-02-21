using System.Collections.Generic;
using Nokia.Music.Types;

namespace IndovinaCanzoni.src.Utils
{
    public class ArtistsComparer : IEqualityComparer<Artist>
    {
        public bool Equals(Artist x, Artist y)
        {
            return x.Id.Equals(y.Id);
        }

        public int GetHashCode(Artist obj)
        {
            return obj.GetHashCode();
        }
    }
}
