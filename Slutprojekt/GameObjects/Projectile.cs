using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slutprojekt.GameObjects
{
    abstract class Projectile : GameObject
    {
        public Projectile(Rectangle drawbox, Texture2D texture) : base(drawbox, texture)
        {

        }
    }
}
