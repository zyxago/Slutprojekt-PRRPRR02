using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slutprojekt.GameObjects.Projectiles
{
    class PierceProjectile : Projectile
    {
        int PierceCount { get; set; }
        public PierceProjectile(Rectangle drawbox, Texture2D texture, int radius, Vector2 direction, int pierceCount) : base(drawbox, texture, radius, direction)
        {
            PierceCount = pierceCount;
        }

        public override void Effect(List<Enemy> enemies, int dmg)
        {
            PierceCount--;
            if(PierceCount == 0)
                IsDead = true;
        }
    }
}
