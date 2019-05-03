using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
        public static List<Tower> TowerList { get; set; }
        public static Rectangle NextWave { get; } = new Rectangle();//TODO fixa position för rectangeln
        public static int Hp { get; set; }
        public static int Money { get; set; }

        public static void Load()
        {
            HotbarTex = LoadData.LoadTexture2D(Game1.graphics.GraphicsDevice, "Hud/TowerBar.png");
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Game1.font, $"Hp: {Hp}", new Vector2(50, 20), Color.Black);
            spriteBatch.DrawString(Game1.font, $"Money: {Money}", new Vector2(50, 30), Color.Black);
            spriteBatch.Draw(HotbarTex, new Rectangle(0, 560, 800, 80), Color.White);
            for (int i = 0; i < TowerList.Count && i <= 8; i++)
            {
                spriteBatch.Draw(TowerList[i].Texture, new Rectangle((10 + 100 * i), 570, 80, 60), Color.White);//FLYTTA TILL HUD KLASSEN
            }
        }
    }
}
