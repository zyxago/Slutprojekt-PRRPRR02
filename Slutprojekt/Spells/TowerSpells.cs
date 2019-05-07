using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slutprojekt.Spells
{
    static class TowerSpells//Ta bort och lägg alla spells under Spell.cs kanske?
    {
        public static void Slow(float radius, Vector2 center, List<Enemy> enemies)
        {
            for(int i = 0; i < enemies.Count && i < 10; i++)
            {
                if(Game1.CheckIfInRange(enemies[i].Center, enemies[i].Radius, center, radius))
                {
                    enemies[i].Speed = Math.Max((int)(enemies[i].Speed * 0.66), 1);
                    enemies[i].Color = Color.Blue;
                }
            }
        }
    }
}
