using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Slutprojekt.API;
using Newtonsoft.Json;
using RestSharp;
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

        private static int Page { get; set; } = 1;
        public static Texture2D ArrowLTex { get; set; }
        public static Texture2D ArrowRTex { get; set; }
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

        public static void Api()
        {
            var client = new RestClient();
            var request = new RestRequest("/", Method.GET);
            IRestResponse response = client.Execute(request);
            string content = response.Content;
            root test = JsonConvert.DeserializeObject<root>(content);
        }

        public static void Load()
        {
            MenuBoxes.Add("mapSlot1", new Rectangle(64, 52, 285, 235));
            MenuBoxes.Add("mapSlot2", new Rectangle(440, 48, 285, 235));
            MenuBoxes.Add("mapSlot3", new Rectangle(64, 334, 285, 235));
            MenuBoxes.Add("mapSlot4", new Rectangle(440, 330, 285, 235));
            MenuBoxes.Add("start", new Rectangle(225,125, 325, 60));
            MenuBoxes.Add("option", new Rectangle(225, 240, 325, 60));
            MenuBoxes.Add("exit", new Rectangle(225, 350, 325, 60));
            MenuBoxes.Add("arrowLeft", new Rectangle(190, 580, 50, 50));
            MenuBoxes.Add("arrowRight", new Rectangle(570, 580, 50, 50));
            MenuTextures.Add(Location.MainMenu, LoadData.LoadTexture2D(Game1.graphics.GraphicsDevice, "MenuGraphics/MainMenu.png"));
            MenuTextures.Add(Location.MapSelector, LoadData.LoadTexture2D(Game1.graphics.GraphicsDevice, "MenuGraphics/MapSelection.png"));
            ArrowLTex = LoadData.LoadTexture2D(Game1.graphics.GraphicsDevice, "MenuGraphics/arrowLeft.png");
            ArrowRTex = LoadData.LoadTexture2D(Game1.graphics.GraphicsDevice, "MenuGraphics/arrowRight.png");
        }

        public static void Update()
        {
            if (location == Location.MainMenu)
            {
                if (Game1.mouseState.LeftButton == ButtonState.Pressed && Game1.prevMouseState.LeftButton != ButtonState.Pressed && MenuBoxes["start"].Contains(Game1.mouseState.Position))
                {
                    location = Location.MapSelector;
                }
                else if (Game1.mouseState.LeftButton == ButtonState.Pressed && Game1.prevMouseState.LeftButton != ButtonState.Pressed && MenuBoxes["option"].Contains(Game1.mouseState.Position))
                {
                    location = Location.Options;
                }
                else if (Game1.mouseState.LeftButton == ButtonState.Pressed && Game1.prevMouseState.LeftButton != ButtonState.Pressed && MenuBoxes["exit"].Contains(Game1.mouseState.Position))
                {
                    Game1.State = Game1.GameState.Quit;
                }
            }
            else if (location == Location.Options)
            {

            }
            else if (location == Location.MapSelector)
            {
                if (Game1.mouseState.LeftButton == ButtonState.Pressed && Game1.prevMouseState.LeftButton != ButtonState.Pressed && MenuBoxes["mapSlot1"].Contains(Game1.mouseState.Position))
                {
                    location = Location.InGame;
                    Game1.StartGame(Page * 1);
                }
                else if (Game1.mouseState.LeftButton == ButtonState.Pressed && Game1.prevMouseState.LeftButton != ButtonState.Pressed && MenuBoxes["mapSlot2"].Contains(Game1.mouseState.Position))
                {
                    location = Location.InGame;
                    Game1.StartGame(Page * 2);
                }
                else if (Game1.mouseState.LeftButton == ButtonState.Pressed && Game1.prevMouseState.LeftButton != ButtonState.Pressed && MenuBoxes["mapSlot3"].Contains(Game1.mouseState.Position))
                {
                    location = Location.InGame;
                    Game1.StartGame(Page * 3);
                }
                else if (Game1.mouseState.LeftButton == ButtonState.Pressed && Game1.prevMouseState.LeftButton != ButtonState.Pressed && MenuBoxes["mapSlot4"].Contains(Game1.mouseState.Position))
                {
                    location = Location.InGame;
                    Game1.StartGame(Page * 4);
                }
                else if (Game1.mouseState.LeftButton == ButtonState.Pressed && Game1.prevMouseState.LeftButton != ButtonState.Pressed && MenuBoxes["arrowLeft"].Contains(Game1.mouseState.Position))
                {
                    if(Page != 1)
                    {
                        Page--;
                    }
                }
                else if (Game1.mouseState.LeftButton == ButtonState.Pressed && Game1.prevMouseState.LeftButton != ButtonState.Pressed && MenuBoxes["arrowRight"].Contains(Game1.mouseState.Position))
                {
                    Page++;
                }
            }
        }

        public static void Draw(SpriteBatch spriteBatch, List<Map> maps)
        {
            spriteBatch.Draw(MenuTextures[location], DrawBox, Color.White);
            if (location == Location.MapSelector)
            {
                spriteBatch.Draw(ArrowLTex, MenuBoxes["arrowLeft"], Color.White);
                spriteBatch.Draw(ArrowRTex, MenuBoxes["arrowRight"], Color.White);
                for (int i = (Page - 1) * 4; i < (Page - 1) * 4 + 4 && i < maps.Count; i++)
                {
                    if (i % 4 == 0) 
                        spriteBatch.Draw(maps[i].Texture, MenuBoxes["mapSlot1"], Color.White);
                    else if (i % 4 == 1)
                        spriteBatch.Draw(maps[i].Texture, MenuBoxes["mapSlot2"], Color.White);
                    else if (i % 4 == 2)
                        spriteBatch.Draw(maps[i].Texture, MenuBoxes["mapSlot3"], Color.White);
                    else if (i % 4 == 3)
                        spriteBatch.Draw(maps[i].Texture, MenuBoxes["mapSlot4"], Color.White);
                }
                spriteBatch.DrawString(Game1.font, $"Page: {Page}", new Vector2(30, 20) ,Color.Black);
            }
        }
    }
}
