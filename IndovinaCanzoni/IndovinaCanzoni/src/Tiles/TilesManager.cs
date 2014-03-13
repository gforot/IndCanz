using System;
using System.Linq;
using IndovinaCanzoni.Model;
using IndovinaCanzoni.Resources;
using Microsoft.Phone.Shell;

namespace IndovinaCanzoni.Tiles
{
    public static class TilesManager
    {
        public static void UpdateTiles()
        {
            ShellTile oTile = ShellTile.ActiveTiles.First();
            FlipTileData oFliptile = new FlipTileData();

            #region Front
            oFliptile.Title = AppResources.ApplicationTitle;
            oFliptile.SmallBackgroundImage = new Uri("Assets/Tiles/radio_159_159.png", UriKind.Relative);
            oFliptile.BackgroundImage = new Uri("Assets/Tiles/radio_336_336.png", UriKind.Relative);
            oFliptile.WideBackgroundImage = new Uri("Assets/Tiles/radio_691_336.png", UriKind.Relative);
            #endregion

            #region Back
            oFliptile.BackBackgroundImage = new Uri("/Assets/Tiles/radio_336_336.png", UriKind.Relative);
            oFliptile.WideBackBackgroundImage = new Uri("/Assets/Tiles/radio_691_336.png", UriKind.Relative);

            oFliptile.BackTitle = AppResources.ApplicationTitle;

            oFliptile.BackContent = AppResources.BackTileMessage;
            oFliptile.WideBackContent = AppResources.BackTileWideMessage;
            #endregion

            oTile.Update(oFliptile);
        }

        public static void UpdateTiles(ScoreItem si)
        {
            ShellTile oTile = ShellTile.ActiveTiles.First();
            FlipTileData oFliptile = new FlipTileData();

            #region Front
            oFliptile.Title = AppResources.ApplicationTitle;
            oFliptile.SmallBackgroundImage = new Uri("Assets/Tiles/radio_159_159.png", UriKind.Relative);
            oFliptile.BackgroundImage = new Uri("Assets/Tiles/radio_336_336.png", UriKind.Relative);
            oFliptile.WideBackgroundImage = new Uri("Assets/Tiles/radio_691_336.png", UriKind.Relative);
            #endregion

            #region Back
            oFliptile.BackBackgroundImage = new Uri("/Assets/Tiles/radio_336_336.png", UriKind.Relative);
            oFliptile.WideBackBackgroundImage = new Uri("/Assets/Tiles/radio_691_336.png", UriKind.Relative);

            oFliptile.BackTitle = AppResources.ApplicationTitle;

            oFliptile.BackContent = string.Format(AppResources.BackTileMessageFormat, IndovinaCanzoni.App.SelectedGenre.Name);
            oFliptile.WideBackContent = string.Format(AppResources.BackTileWideMessageFormat, si.User, si.Score);
            #endregion


            oTile.Update(oFliptile);
        }

    }
}
