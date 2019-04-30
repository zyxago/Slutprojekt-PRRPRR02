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
        public static Texture2D hotbarTex { get; set; }
        public static List<Tower> towerList { get; set; }
        public static Rectangle NextWave { get; } = new Rectangle();//TODO fixa position för rectangeln
        public static int Hp { get; set; }
        public static int Money { get; set; }
        public static void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Game1.font, $"{Hp}", new Vector2(50, 40), Color.Black);
            spriteBatch.DrawString(Game1.font, $"{Money}", new Vector2(50, 60), Color.Black);
            spriteBatch.Draw(hotbarTex, new Rectangle(0, 560, 800, 80), Color.White);
            for (int i = 0; i < towerList.Count && i <= 8; i++)
            {
                spriteBatch.Draw(towerList[i].Texture, new Rectangle((10 + 100 * i), 570, 80, 60), Color.White);//FLYTTA TILL HUD KLASSEN
            }
            spriteBatch.DrawString(Game1.font, $"{Game1.mouseState.Position}", new Vector2(50, 50), Color.Black);//Ta bort sen!
        }
    }
}
