using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slutprojekt.GameObjects.Enemies
{
    sealed class NormalType : Enemy
    {
        public NormalType(Rectangle drawBox, Texture2D texture, int radius, int speed, int hp, string resistance, Queue<Vector2> path = null) : base(drawBox, texture, radius, speed, hp, resistance, path)
        {
            Worth = 10;
            Dmg = 1;
        }
    }
}
