using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slutprojekt
{
    abstract class Tower : GameObject
    {
        public float AttackRange { get; set; }
        public float AttackSpeed { get; set; }
        public float AttackDMG { get; set; }
        public enum ProjectileType
        {
            splash,
            pierce
        };
        public ProjectileType Ptype { get; set; }

        public Tower(Rectangle drawBox, Texture2D texture, float Arange, float Aspeed, float Admg, ProjectileType type) : base(drawBox, texture)
        {
            AttackRange = Arange;
            AttackSpeed = Aspeed;
            AttackDMG = Admg;
        }

        public void Update()
        {

        }
    }
}
