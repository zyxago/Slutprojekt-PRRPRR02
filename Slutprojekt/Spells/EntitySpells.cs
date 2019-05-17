using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Slutprojekt.Tower;

namespace Slutprojekt.Spells
{
    static class EntitySpells
    {
        public static void Slow(float radius, Vector2 center, List<Tower> towers)
        {
            for (int i = 0; i < towers.Count && i < 10; i++)
            {
                if (Game1.CheckIfInRange(towers[i].Center, towers[i].Radius, center, radius))
                {
                    if(towers[i] is IAttack)
                    {
                        (towers[i] as IAttack).AttackSpeed = Math.Max((int)((towers[i] as IAttack).AttackSpeed * 0.8), 1);
                        towers[i].Color = Color.Gray;
                    }
                }
            }
        }
    }
}
