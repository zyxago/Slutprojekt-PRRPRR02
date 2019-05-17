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
        private static Texture2D ExitOptionsButtonTex { get; set; }
        private static Rectangle DrawBox { get; set; } = Game1.graphics.GraphicsDevice.Viewport.Bounds;
        private static Dictionary<Location, Texture2D> MenuTextures { get; set; } = new Dictionary<Location, Texture2D>();
        private static Dictionary<string, Rectangle> MenuBoxes { get; set; } = new Dictionary<string, Rectangle>();
        private static List<Tower> SelectedTowers = new List<Tower>();
        public enum Location
        {
            MainMenu,
            Options, 
            MapSelector,
            TowerSelector,
            InGame
        };
        private enum ChangeHotkey
        {
            None,
            StartWave,
            Hotbar0,
            Hotbar1,
            Hotbar2,
            Hotbar3,
            Hotbar4,
            Hotbar5,
            Hotbar6,
            Hotbar7,
            ActivateSpell

        }
        private static ChangeHotkey hotkey;
        public static Location location;

        /// <summary>
        /// 
        /// </summary>
        public static void Reset()
        {
            location = Location.MainMenu;
            SelectedTowers = new List<Tower>();
        }

        /// <summary>
        /// Call to set menus rectangles and load textures
        /// </summary>
        public static void Load()
        {
            QuoteString = ApiQuotes.contents.quotes[0].quote;
            MenuBoxes.Add("mapSlot1", new Rectangle(64, 52, 285, 235));
            MenuBoxes.Add("mapSlot2", new Rectangle(440, 48, 285, 235));
            MenuBoxes.Add("mapSlot3", new Rectangle(64, 334, 285, 235));
            MenuBoxes.Add("mapSlot4", new Rectangle(440, 330, 285, 235));
            MenuBoxes.Add("start", new Rectangle(225,125, 325, 60));
            MenuBoxes.Add("option", new Rectangle(225, 240, 325, 60));
            MenuBoxes.Add("exit", new Rectangle(225, 350, 325, 60));
            MenuBoxes.Add("arrowLeft", new Rectangle(190, 580, 50, 50));
            MenuBoxes.Add("arrowRight", new Rectangle(570, 580, 50, 50));
            MenuBoxes.Add("exitOptions", new Rectangle(200, 400, 80, 40));
            MenuBoxes.Add("startWaveKey", new Rectangle(200, 100, 120, 20));
            MenuBoxes.Add("activateSpellKey", new Rectangle(200, 70, 120, 20));
            for (int i = 0; i < 8; i++)
            {
                MenuBoxes.Add($"selectTowerKey{i}", new Rectangle(200, 130 + (30 * i), 120, 20));
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
            ExitOptionsButtonTex = LoadData.LoadTexture2D(Game1.graphics.GraphicsDevice, "MenuGraphics/OptionsExitButton.png");
            ArrowLTex = LoadData.LoadTexture2D(Game1.graphics.GraphicsDevice, "MenuGraphics/arrowLeft.png");
            ArrowRTex = LoadData.LoadTexture2D(Game1.graphics.GraphicsDevice, "MenuGraphics/arrowRight.png");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="towerTypes"></param>
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
                if (Game1.KeyState.GetPressedKeys().Length != 0 && hotkey != ChangeHotkey.None)
                {
                    if (hotkey == ChangeHotkey.StartWave)
                        Options.startWave = Game1.KeyState.GetPressedKeys().First();
                    else if (hotkey == ChangeHotkey.ActivateSpell)
                        Options.activateSpell = Game1.KeyState.GetPressedKeys().First();
                    for(int i = 0; i < 8; i++)
                    {
                        if (hotkey == (ChangeHotkey)(i + 2))
                        {
                            Options.hotbarKeys[i] = Game1.KeyState.GetPressedKeys().First();
                        }
                    }
                    hotkey = ChangeHotkey.None;
                }
                if (Game1.mouseState.LeftButton == ButtonState.Pressed && Game1.prevMouseState.LeftButton != ButtonState.Pressed && MenuBoxes["startWaveKey"].Contains(Game1.mouseState.Position))
                    hotkey = ChangeHotkey.StartWave;
                else if(Game1.mouseState.LeftButton == ButtonState.Pressed && Game1.prevMouseState.LeftButton != ButtonState.Pressed && MenuBoxes["activateSpellKey"].Contains(Game1.mouseState.Position))
                    hotkey = ChangeHotkey.ActivateSpell;
                for (int i = 0; i < 8; i++)
                {
                    if (Game1.mouseState.LeftButton == ButtonState.Pressed && Game1.prevMouseState.LeftButton != ButtonState.Pressed && MenuBoxes[$"selectTowerKey{i}"].Contains(Game1.mouseState.Position))
                        hotkey = (ChangeHotkey)(2 + i);
                }
                if (Game1.mouseState.LeftButton == ButtonState.Pressed && Game1.prevMouseState.LeftButton != ButtonState.Pressed && MenuBoxes["exitOptions"].Contains(Game1.mouseState.Position))
                    location = Location.MainMenu;
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
                try//Nödlösning då vissa karaktärer i stringen får spelet att krasha...
                {
                    spriteBatch.DrawString(Game1.font, QuoteString, new Vector2(70, 60), Color.Black);
                }
                catch
                {

                }
            }
            else if(location == Location.Options)
            {
                spriteBatch.Draw(ExitOptionsButtonTex, MenuBoxes["exitOptions"], Color.White);
                spriteBatch.DrawString(Game1.font, $"Star Wave Key: {Options.startWave}", new Vector2(MenuBoxes["startWaveKey"].X, MenuBoxes["startWaveKey"].Y), Color.Black);
                spriteBatch.DrawString(Game1.font, $"Activate a Spell Key: {Options.activateSpell}", new Vector2(MenuBoxes["activateSpellKey"].X, MenuBoxes["activateSpellKey"].Y), Color.Black);
                for(int i = 0;i < 8; i++)
                {
                    spriteBatch.DrawString(Game1.font, $"Hotbar number {i} key: {Options.hotbarKeys[i]}", new Vector2(MenuBoxes[$"selectTowerKey{i}"].X, MenuBoxes[$"selectTowerKey{i}"].Y), Color.Black);
                }
                if (hotkey != ChangeHotkey.None)
                    spriteBatch.DrawString(Game1.font, "Press any button to make it the new hotkey", new Vector2(200, 50), Color.Black);
                else
                    spriteBatch.DrawString(Game1.font, "Click on a hotkey to change it", new Vector2(200, 50), Color.Black);
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
