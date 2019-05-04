using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Slutprojekt.Tower;

namespace Slutprojekt.GameObjects.Towers
{
    sealed class ClassicTower : Tower , IAttack
    {
        public float AttackRange { get; set; }
        public float AttackSpeed { get; set; }
        public float AttackDMG { get; set; }
        public Texture2D ProjectileTexture { get; set; }
        public enum ProjectileType
        {
            splash,
            pierce
        };
        public ProjectileType Ptype { get; set; }
        public ClassicTower(Rectangle drawBox, Texture2D texture, float radius, int cost, Texture2D projectileTexture, float Arange, float Aspeed, float Admg, string projectileType) : base(drawBox, texture, radius, cost)
        {
            AttackRange = Arange;
            AttackSpeed = Aspeed;
            AttackDMG = Admg;
            if (projectileType == "splash")
                Ptype = ProjectileType.splash;
            else if (projectileType == "pierce")
                Ptype = ProjectileType.pierce;
        }

        public void Attack()
        {

        }
    }
}
