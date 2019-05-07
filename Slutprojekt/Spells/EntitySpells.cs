using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slutprojekt.Spells
{
    static class EntitySpells
    {
        public static void Slow(float radius, Vector2 center, List<Tower> towers)//TODO fixa
        {
            for (int i = 0; i < towers.Count && i < 10; i++)
            {
                if (Game1.CheckIfInRange(towers[i].Center, towers[i].Radius, center, radius))
                {
                    //towers[i]. = Math.Max((int)(enemies[i].Speed * 0.66), 1);
                    towers[i].Color = Color.Blue;
                }
            }
        }
    }
}
