using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slutprojekt.GameObjects.Towers
{
    sealed class ClassicTower : Tower
    {
        public ClassicTower(Rectangle drawBox, Texture2D texture, float Arange, float Aspeed, float Admg, ProjectileType type) : base(drawBox, texture, Arange, Aspeed, Admg, type)
        {

        }
    }
}
