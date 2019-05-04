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
        public PierceProjectile(Rectangle drawbox, Texture2D texture, float radius) : base(drawbox, texture, radius)
        {

        }
    }
}
