using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Slutprojekt.GameObjects.Towers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slutprojekt
{
    static class Hud
    {
        public static Texture2D HotbarTex { get; set; }
        public static Texture2D NextWaveTex { get; set; }
        public static List<Tower> TowerList { get; set; }
        public static Rectangle NextWaveBox { get; } = new Rectangle(740, 525, 40, 40);
        public static Dictionary<Tower, Rectangle> TowerTypes = new Dictionary<Tower, Rectangle>();
        public static int Hp { get; set; } = 100;
        public static int Money { get; set; } = 100;
        public static Tower TowerSelected;

        public static void Load(List<Tower> towers)
        {
            TowerList = towers;
            NextWaveTex = LoadData.LoadTexture2D(Game1.graphics.GraphicsDevice, "Hud/NextWave.png");
            HotbarTex = LoadData.LoadTexture2D(Game1.graphics.GraphicsDevice, "Hud/TowerBar.png");
            for (int i = 0; i < TowerList.Count && i <= 8; i++)
            {
                TowerTypes.Add(TowerList[i], new Rectangle((10 + 100 * i), 570, 80, 60));
            }
        }

        public static void Update()
        {
            if(TowerSelected != null)
                TowerSelected.Drawbox = new Rectangle(Game1.mouseState.Position.X - TowerSelected.Drawbox.Width / 2, Game1.mouseState.Position.Y - TowerSelected.Drawbox.Height / 2, TowerSelected.Drawbox.Width, TowerSelected.Drawbox.Height);
            foreach(Tower tower in TowerTypes.Keys)
            {
                Rectangle hotbarBox;
                TowerTypes.TryGetValue(tower, out hotbarBox);
                if (Game1.mouseState.LeftButton == ButtonState.Pressed && Game1.prevMouseState.LeftButton != ButtonState.Pressed && hotbarBox.Contains(Game1.mouseState.Position) && Money >= tower.Cost)
                {
                    TowerSelected = tower;
                }
            }
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Game1.font, $"Hp: {Hp}", new Vector2(20, 10), Color.Black);
            spriteBatch.DrawString(Game1.font, $"Money: {Money}", new Vector2(20, 30), Color.Black);
            spriteBatch.Draw(HotbarTex, new Rectangle(0, 560, 800, 80), Color.White);
            spriteBatch.Draw(NextWaveTex, NextWaveBox, Color.White);
            foreach(Tower tower in TowerTypes.Keys)
            {
                Rectangle drawBox;
                TowerTypes.TryGetValue(tower, out drawBox);
                if (Money < tower.Cost)
                    spriteBatch.Draw(tower.Texture, drawBox, Color.DimGray);
                else
                    spriteBatch.Draw(tower.Texture, drawBox, Color.White);
            }
            if (TowerSelected != null)
                spriteBatch.Draw(TowerSelected.Texture, TowerSelected.Drawbox, Color.White);
        }
    }
}
