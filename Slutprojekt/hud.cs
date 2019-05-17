using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Slutprojekt.GameObjects.Towers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Slutprojekt.GameObject;

namespace Slutprojekt
{
    static class Hud
    {
        private static Texture2D HotbarTex { get; set; }
        private static Texture2D NextWaveTex { get; set; }
        private static Texture2D ActivateSpellTex { get; set; }
        public static Rectangle NextWaveBox { get; } = new Rectangle(740, 510, 40, 40);
        private static Rectangle ActivateSpellBox { get; set; } = new Rectangle(40, 510, 40, 40);
        private static Dictionary<Tower, Rectangle> TowerTypes = new Dictionary<Tower, Rectangle>();
        public static int Hp { get; set; } = 100;
        public static int Money { get; set; } = 100;
        public static Tower TowerSelected { get; set; }
        private static int spellCount { get; set; } = 0;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="towers"></param>
        public static void Load(List<Tower> towers)
        {
            NextWaveTex = LoadData.LoadTexture2D(Game1.graphics.GraphicsDevice, "Hud/NextWave.png");
            HotbarTex = LoadData.LoadTexture2D(Game1.graphics.GraphicsDevice, "Hud/TowerBar.png");
            ActivateSpellTex = LoadData.LoadTexture2D(Game1.graphics.GraphicsDevice, "Hud/Spell.png");
            for (int i = 0; i < towers.Count && i < 8; i++)
            {
                TowerTypes.Add(towers[i], new Rectangle((10 + 100 * i), 570, 80, 60));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static void Reset()
        {
            Hp = 100;
            Money = 100;
            TowerTypes = new Dictionary<Tower, Rectangle>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="towers"></param>
        public static void Update(List<Tower> towers)
        {
            if (TowerSelected != null)
            {
                TowerSelected.Drawbox = new Rectangle(Game1.mouseState.Position.X - TowerSelected.Drawbox.Width / 2, Game1.mouseState.Position.Y - TowerSelected.Drawbox.Height / 2, TowerSelected.Drawbox.Width, TowerSelected.Drawbox.Height);
                TowerSelected.Update();
            }
            int n = 0;
            foreach (Tower tower in TowerTypes.Keys)
            {
                Rectangle hotbarBox;
                TowerTypes.TryGetValue(tower, out hotbarBox);
                if (Game1.mouseState.LeftButton == ButtonState.Pressed && Game1.prevMouseState.LeftButton != ButtonState.Pressed && hotbarBox.Contains(Game1.mouseState.Position) || Game1.KeyState.IsKeyDown(Options.hotbarKeys[n]) && Game1.prevKeyState.IsKeyUp(Options.hotbarKeys[n]) && Money >= tower.Cost)
                {
                    TowerSelected = tower;
                }
                n++;
            }
            int tempCounter = 0;
            foreach(Tower tower in towers)
            {
                if(tower is SpellTower)
                {
                    if((tower as SpellTower).SpellReady)
                    {
                        tempCounter++;
                        if (Game1.mouseState.LeftButton == ButtonState.Pressed && Game1.prevMouseState.LeftButton != ButtonState.Pressed && ActivateSpellBox.Contains(Game1.mouseState.Position) || Game1.KeyState.IsKeyDown(Options.activateSpell) && Game1.prevKeyState.IsKeyDown(Options.activateSpell))
                        {
                            (tower as SpellTower).SpellActivate = true;
                            break;
                        }
                    }
                }
            }
            spellCount = tempCounter;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="spriteBatch"></param>
        public static void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Game1.font, $"Hp: {Hp}", new Vector2(20, 10), Color.Black);
            spriteBatch.DrawString(Game1.font, $"Money: {Money}", new Vector2(20, 30), Color.Black);
            spriteBatch.DrawString(Game1.font, $"Wave: {EntitySpawner.wave}", new Vector2(20, 50), Color.Black);
            spriteBatch.Draw(ActivateSpellTex, ActivateSpellBox, Color.White);
            spriteBatch.DrawString(Game1.font, spellCount.ToString(), new Vector2(ActivateSpellBox.Right, ActivateSpellBox.Top), Color.Black);
            spriteBatch.Draw(HotbarTex, new Rectangle(0, 560, 800, 80), Color.White);
            spriteBatch.Draw(NextWaveTex, NextWaveBox, Color.White);
            foreach(Tower tower in TowerTypes.Keys)
            {
                Rectangle hotbarBox;
                TowerTypes.TryGetValue(tower, out hotbarBox);
                if (hotbarBox.Contains(Game1.mouseState.Position))
                {
                    spriteBatch.DrawString(Game1.font, tower.Name, new Vector2(hotbarBox.X + 10, hotbarBox.Y - 25), Color.DarkGreen);
                }
                if (Money < tower.Cost)
                    spriteBatch.Draw(tower.Texture, hotbarBox, Color.DimGray);
                else
                    spriteBatch.Draw(tower.Texture, hotbarBox, Color.White);
            }
            if (TowerSelected != null)
                TowerSelected.Draw(spriteBatch);
        }
    }
}
