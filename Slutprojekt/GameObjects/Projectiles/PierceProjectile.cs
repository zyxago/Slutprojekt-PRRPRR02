using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slutprojekt.GameObjects.Projectiles
{
    sealed class PierceProjectile : Projectile
    {
        private int PierceCount { get; set; }
        private int LastHitIndex { get; set; } = 999;
        public PierceProjectile(Rectangle drawbox, Texture2D texture, int radius, Vector2 direction, int pierceCount) : base(drawbox, texture, radius, direction)
        {
            PierceCount = pierceCount;
        }

        public override void Effect(List<Enemy> enemies, int dmg, int index)
        {
            if(index != LastHitIndex)
            {
                if (enemies[index].Resistance == "pierce")
                    enemies[index].Hp -= dmg  / 2;
                else
                    enemies[index].Hp -= dmg;
                PierceCount--;
                LastHitIndex = index;
            }
            if(PierceCount == 0)
                IsDead = true;
        }
    }
}
