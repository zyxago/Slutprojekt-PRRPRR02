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

        private static int Page { get; set; } = 1;
        public static Texture2D CurrentTexture { get; set; }
        private static Rectangle DrawBox = Game1.graphics.GraphicsDevice.Viewport.Bounds;
        private static Dictionary<Location, Texture2D> MenuTextures = new Dictionary<Location, Texture2D>();
        private static Dictionary<string, Rectangle> MenuBoxes = new Dictionary<string, Rectangle>();
        public enum Location
        {
            MainMenu,
            Options, 
            MapSelector,
            InGame
        };
        public static Location location;

        public static void Load()
        {
            MenuBoxes.Add("mapSlot1", new Rectangle(64, 52, 285, 235));
            MenuBoxes.Add("mapSlot2", new Rectangle(440, 48, 285, 235));
            MenuBoxes.Add("mapSlot3", new Rectangle(64, 334, 285, 235));
            MenuBoxes.Add("mapSlot4", new Rectangle(440, 330, 285, 235));
            MenuBoxes.Add("start", new Rectangle(225,125, 325, 60));
            MenuBoxes.Add("option", new Rectangle(225, 240, 325, 60));
            MenuBoxes.Add("exit", new Rectangle(225, 350, 325, 60));
            MenuBoxes.Add("arrowLeft", new Rectangle());
            MenuBoxes.Add("arrowRight", new Rectangle());
            MenuTextures.Add(Location.MainMenu, LoadData.LoadTexture2D(Game1.graphics.GraphicsDevice, "MenuGraphics/MainMenu.png"));
            MenuTextures.Add(Location.MapSelector, LoadData.LoadTexture2D(Game1.graphics.GraphicsDevice, "MenuGraphics/MapSelection.png"));
        }

        public static void Update()
        {
            if (location == Location.MainMenu)
            {
                if (Game1.mouseState.LeftButton != Game1.prevMouseState.LeftButton && MenuBoxes["start"].Contains(Game1.mouseState.Position))
                {
                    location = Location.MapSelector;
                }
                else if (Game1.mouseState.LeftButton != Game1.prevMouseState.LeftButton && MenuBoxes["option"].Contains(Game1.mouseState.Position))
                {
                    location = Location.Options;
                }
                else if (Game1.mouseState.LeftButton != Game1.prevMouseState.LeftButton && MenuBoxes["exit"].Contains(Game1.mouseState.Position))
                {
                    Game1.State = Game1.GameState.Quit;
                }
            }
            else if (location == Location.Options)
            {

            }
            else if (location == Location.MapSelector)
            {
                if (Game1.mouseState.LeftButton != Game1.prevMouseState.LeftButton && MenuBoxes["mapSlot1"].Contains(Game1.mouseState.Position))
                {
                    location = Location.InGame;
                    Game1.StartGame(Page * 1);
                }
                else if (Game1.mouseState.LeftButton != Game1.prevMouseState.LeftButton && MenuBoxes["mapSlot2"].Contains(Game1.mouseState.Position))
                {
                    location = Location.InGame;
                    Game1.StartGame(Page * 2);
                }
                else if (Game1.mouseState.LeftButton != Game1.prevMouseState.LeftButton && MenuBoxes["mapSlot3"].Contains(Game1.mouseState.Position))
                {
                    location = Location.InGame;
                    Game1.StartGame(Page * 3);
                }
                else if (Game1.mouseState.LeftButton != Game1.prevMouseState.LeftButton && MenuBoxes["mapSlot4"].Contains(Game1.mouseState.Position))
                {
                    location = Location.InGame;
                    Game1.StartGame(Page * 4);
                }
                else if (Game1.mouseState.LeftButton != Game1.prevMouseState.LeftButton && MenuBoxes["arrowLeft"].Contains(Game1.mouseState.Position))
                {

                }
                else if (Game1.mouseState.LeftButton != Game1.prevMouseState.LeftButton && MenuBoxes["arrowRight"].Contains(Game1.mouseState.Position))
                {

                }
            }
        }

        public static void Draw(SpriteBatch spriteBatch, List<Map> maps)
        {
            spriteBatch.Draw(MenuTextures[location], DrawBox, Color.White);
            if (location == Location.MapSelector)
            {
                for (int i = 0; i < maps.Count; i++)
                {
                    if (i+1 == 1 * Page)
                        spriteBatch.Draw(maps[i].Texture, MenuBoxes["mapSlot1"], Color.White);
                    else if (i+1 == 2 * Page)
                        spriteBatch.Draw(maps[i].Texture, MenuBoxes["mapSlot2"], Color.White);
                    else if (i+1 == 3 * Page)
                        spriteBatch.Draw(maps[i].Texture, MenuBoxes["mapSlot3"], Color.White);
                    else if (i+1 == 4 * Page)
                        spriteBatch.Draw(maps[i].Texture, MenuBoxes["mapSlot4"], Color.White);
                }
            }
        }
    }
}
