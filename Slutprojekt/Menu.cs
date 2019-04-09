using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Slutprojekt.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slutprojekt
{
    static class Menu
    {
        //API
        public static Success success { get; set; }
        public static Contents contents { get; set; }
        public static Texture2D CurrentTexture { get; set; }
        private static Rectangle DrawBox = Game1.graphics.GraphicsDevice.Viewport.Bounds;
        public static Dictionary<Location, Texture2D> MenuTextures = new Dictionary<Location, Texture2D>();
        public static Dictionary<string, Rectangle> MenuBoxes = new Dictionary<string, Rectangle>();
        public enum Location
        {
            MainMenu,
            Options, 
            MapSelector,
            TowerSelector
        };
        public static Location location;

        public static void Load()
        {
            MenuBoxes.Add("start", new Rectangle());
            MenuTextures.Add(Location.MainMenu, LoadData.LoadTexture2D(Game1.graphics.GraphicsDevice, "MenuGraphics/MainMenu.png"));
            MenuTextures.Add(Location.MapSelector, LoadData.LoadTexture2D(Game1.graphics.GraphicsDevice, "MenuGraphics/MapSelection.png"));
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(MenuTextures[location], DrawBox, Color.White);
        }
    }
}
