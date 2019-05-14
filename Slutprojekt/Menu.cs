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
        private static RestClient client = new RestClient("http://quotes.rest/qod");
        private static RestRequest request = new RestRequest("/", Method.GET);
        private static IRestResponse response = client.Execute(request);
        private static string content = response.Content;
        private static ApiQuotes ApiQuotes { get; } = JsonConvert.DeserializeObject<ApiQuotes>(content);

        private static string QuoteString { get; set; }
        private static int Page { get; set; } = 1;
        private static int MapIndex { get; set; } = 1;
        private static Texture2D ArrowLTex { get; set; }
        private static Texture2D ArrowRTex { get; set; }
        private static Texture2D CurrentTexture { get; set; }
        private static Rectangle DrawBox { get; set; } = Game1.graphics.GraphicsDevice.Viewport.Bounds;
        private static Dictionary<Location, Texture2D> MenuTextures { get; set; } = new Dictionary<Location, Texture2D>();
        private static Dictionary<string, Rectangle> MenuBoxes { get; set; } = new Dictionary<string, Rectangle>();
        private static List<Tower> SelectedTowers = new List<Tower>();
        private static bool ChangeHotkey { get; set; } = false;
        public enum Location
        {
            MainMenu,
            Options, 
            MapSelector,
            TowerSelector,
            InGame
        };
        public static Location location;

        /// <summary>
        /// Call to set menus rectangles and load textures
        /// </summary>
        public static void Load()
        {
            //QuoteString = ApiQuotes.contents.quotes[0].quote;
            MenuBoxes.Add("mapSlot1", new Rectangle(64, 52, 285, 235));
            MenuBoxes.Add("mapSlot2", new Rectangle(440, 48, 285, 235));
            MenuBoxes.Add("mapSlot3", new Rectangle(64, 334, 285, 235));
            MenuBoxes.Add("mapSlot4", new Rectangle(440, 330, 285, 235));
            MenuBoxes.Add("start", new Rectangle(225,125, 325, 60));
            MenuBoxes.Add("option", new Rectangle(225, 240, 325, 60));
            MenuBoxes.Add("exit", new Rectangle(225, 350, 325, 60));
            MenuBoxes.Add("arrowLeft", new Rectangle(190, 580, 50, 50));
            MenuBoxes.Add("arrowRight", new Rectangle(570, 580, 50, 50));
            MenuBoxes.Add("startWaveKey", new Rectangle(225, 140, 50, 20));
            for (int i = 0; i < 8; i++)
            {
                MenuBoxes.Add($"selectTowerKey{i}", new Rectangle(225, 150 + (10 * i), 50, 20));
            }
            for(int i = 0; i < 3; i++)
            {
                for(int j = 0; j < 4; j++)
                {
                    MenuBoxes.Add($"tower{(i * 4) + j}", new Rectangle(35 + (j * 190), 127 + (i * 100), 160, 75));
                }
            }
            MenuTextures.Add(Location.MainMenu, LoadData.LoadTexture2D(Game1.graphics.GraphicsDevice, "MenuGraphics/MainMenu.png"));
            MenuTextures.Add(Location.MapSelector, LoadData.LoadTexture2D(Game1.graphics.GraphicsDevice, "MenuGraphics/MapSelection.png"));
            MenuTextures.Add(Location.Options, LoadData.LoadTexture2D(Game1.graphics.GraphicsDevice, "MenuGraphics/OptionsBackground.png"));
            MenuTextures.Add(Location.TowerSelector, LoadData.LoadTexture2D(Game1.graphics.GraphicsDevice, "MenuGraphics/TowerSelection.png"));
            ArrowLTex = LoadData.LoadTexture2D(Game1.graphics.GraphicsDevice, "MenuGraphics/arrowLeft.png");
            ArrowRTex = LoadData.LoadTexture2D(Game1.graphics.GraphicsDevice, "MenuGraphics/arrowRight.png");
        }

        /// <summary>
        /// 
        /// </summary>
        public static void Update(List<Tower> towerTypes)
        {
            if (location == Location.MainMenu)
            {
                if (Game1.mouseState.LeftButton == ButtonState.Pressed && Game1.prevMouseState.LeftButton != ButtonState.Pressed && MenuBoxes["start"].Contains(Game1.mouseState.Position))
                    location = Location.MapSelector;
                else if (Game1.mouseState.LeftButton == ButtonState.Pressed && Game1.prevMouseState.LeftButton != ButtonState.Pressed && MenuBoxes["option"].Contains(Game1.mouseState.Position))
                    location = Location.Options;
                else if (Game1.mouseState.LeftButton == ButtonState.Pressed && Game1.prevMouseState.LeftButton != ButtonState.Pressed && MenuBoxes["exit"].Contains(Game1.mouseState.Position))
                    Game1.State = Game1.GameState.Quit;
            }
            else if (location == Location.Options)
            {
                if (Game1.mouseState.LeftButton == ButtonState.Pressed && Game1.prevMouseState.LeftButton != ButtonState.Pressed && MenuBoxes["startWaveKey"].Contains(Game1.mouseState.Position))
                {
                    ChangeHotkey = true;
                    if (Game1.KeyState.GetPressedKeys().Length == 1)
                        Options.startWave = Game1.KeyState.GetPressedKeys().First();
                }
            }
            else if (location == Location.MapSelector)
            {
                if (Game1.mouseState.LeftButton == ButtonState.Pressed && Game1.prevMouseState.LeftButton != ButtonState.Pressed && MenuBoxes["mapSlot1"].Contains(Game1.mouseState.Position))
                {
                    location = Location.TowerSelector;
                    MapIndex = Page * 1;
                    Page = 1;
                }
                else if (Game1.mouseState.LeftButton == ButtonState.Pressed && Game1.prevMouseState.LeftButton != ButtonState.Pressed && MenuBoxes["mapSlot2"].Contains(Game1.mouseState.Position))
                {
                    location = Location.TowerSelector;
                    MapIndex = Page * 2;
                    Page = 1;
                }
                else if (Game1.mouseState.LeftButton == ButtonState.Pressed && Game1.prevMouseState.LeftButton != ButtonState.Pressed && MenuBoxes["mapSlot3"].Contains(Game1.mouseState.Position))
                {
                    location = Location.TowerSelector;
                    MapIndex = Page * 3;
                    Page = 1;
                }
                else if (Game1.mouseState.LeftButton == ButtonState.Pressed && Game1.prevMouseState.LeftButton != ButtonState.Pressed && MenuBoxes["mapSlot4"].Contains(Game1.mouseState.Position))
                {
                    location = Location.TowerSelector;
                    MapIndex = Page * 4;
                    Page = 1;
                }
                else if (Game1.mouseState.LeftButton == ButtonState.Pressed && Game1.prevMouseState.LeftButton != ButtonState.Pressed && MenuBoxes["arrowLeft"].Contains(Game1.mouseState.Position))
                {
                    if(Page != 1)
                        Page--;
                }
                else if (Game1.mouseState.LeftButton == ButtonState.Pressed && Game1.prevMouseState.LeftButton != ButtonState.Pressed && MenuBoxes["arrowRight"].Contains(Game1.mouseState.Position))
                    Page++;
            }
            else if (location == Location.TowerSelector)
            {
                if (Game1.mouseState.LeftButton == ButtonState.Pressed && Game1.prevMouseState.LeftButton != ButtonState.Pressed && MenuBoxes["arrowLeft"].Contains(Game1.mouseState.Position))
                {
                    if (Page != 1)
                        Page--;
                }
                else if (Game1.mouseState.LeftButton == ButtonState.Pressed && Game1.prevMouseState.LeftButton != ButtonState.Pressed && MenuBoxes["arrowRight"].Contains(Game1.mouseState.Position))
                    Page++;
                if (SelectedTowers.Count < 8 && SelectedTowers.Count < towerTypes.Count)
                {
                    for(int i = 0; i < 12 && i < towerTypes.Count; i++)
                    {
                        if (Game1.mouseState.LeftButton == ButtonState.Pressed && Game1.prevMouseState.LeftButton != ButtonState.Pressed && MenuBoxes[$"tower{i}"].Contains(Game1.mouseState.Position))
                        {
                            bool Duplicate = false;
                            if(SelectedTowers.Count != 0)
                            {
                                foreach(Tower tower in SelectedTowers)
                                {
                                    if(tower == towerTypes[i])
                                    {
                                        Duplicate = true;
                                    }
                                }
                            }
                            if(!Duplicate)
                                SelectedTowers.Add(towerTypes[i]);
                        }
                    }
                }
                else
                {
                    Game1.StartGame(MapIndex, SelectedTowers);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="spriteBatch">Spritebatch</param>
        /// <param name="maps">The selection of maps to choose from</param>
        public static void Draw(SpriteBatch spriteBatch, List<Map> maps, List<Tower> towerTypes)
        {
            spriteBatch.Draw(MenuTextures[location], DrawBox, Color.White);
            if(location == Location.MainMenu)
            {
                try
                {
                    spriteBatch.DrawString(Game1.font, $"{QuoteString}", new Vector2(70, 60), Color.Black);
                }
                catch
                {

                }
            }
            else if(location == Location.Options)
            {
                //spriteBatch.DrawString()//TODO: skriv ut hotkey texterna
                if (ChangeHotkey)
                {
                    spriteBatch.DrawString(Game1.font, "Press any button to make it the new hotkey", new Vector2(200, 50), Color.Black);
                }
            }
            else if (location == Location.MapSelector)
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
            else if (location == Location.TowerSelector)
            {
                spriteBatch.Draw(ArrowLTex, MenuBoxes["arrowLeft"], Color.White);
                spriteBatch.Draw(ArrowRTex, MenuBoxes["arrowRight"], Color.White);
                spriteBatch.DrawString(Game1.font, $"Page: {Page}", new Vector2(30, 20), Color.Black);
                for (int i = (Page - 1) * 12; i < (Page - 1) * 12 + 12 && i < towerTypes.Count; i++)
                {
                    if (i % 12 == i)
                        spriteBatch.Draw(towerTypes[i].Texture, MenuBoxes[$"tower{i}"], towerTypes[i].Color);
                }
            }
        }
    }
}
