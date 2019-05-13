using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slutprojekt.GameObjects.Projectiles
{
    sealed class SplashProjectile : Projectile
    {
        int SplashRange { get; set; }
        public SplashProjectile(Rectangle drawbox, Texture2D texture, int radius, Vector2 direction, int splashRange) : base(drawbox, texture, radius, direction)
        {
            SplashRange = splashRange;
        }

        public override void Effect(List<Enemy> enemies, int dmg, int index)
        {
            if (enemies[index].Resistance == "splash")
                enemies[index].Hp -= dmg / 2;
            else
                enemies[index].Hp -= dmg;
            foreach(Enemy enemy in enemies)
            {
                if(Game1.CheckIfInRange(enemy.Center, enemy.Radius, Center, SplashRange))
                {
                    if (enemy.Resistance == "splash")
                        enemy.Hp -= (int)(dmg * 0.8) / 2;
                    else
                        enemy.Hp -= (int)(dmg * 0.8);
                }
            }
            IsDead = true;
        }
    }
}
