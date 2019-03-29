using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slutprojekt
{
    abstract class Enemy : GameObject
    {
        public Enemy(Rectangle drawBox, Texture2D texture) : base(drawBox, texture)
        {

        }
    }
}
