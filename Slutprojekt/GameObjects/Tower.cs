using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Slutprojekt.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slutprojekt
{
    public abstract class Tower : GameObject
    {
        public interface IAttack
        {
            int AttackRange { get; set; }
            float AttackSpeed { get; set; }
            int AttackDMG { get; set; }
            int ProjectileRadius { get; set; }
            Rectangle ProjectileDrawbox { get; set; }
            Texture2D ProjectileTexture { get; set; }
            List<Projectile> Projectiles { get; set; }
            TimeSpan AttackDelay { get; set; }
            int ProjectileEffect { get; set; }
            void Attack(Enemy target);
        }
        protected List<Enemy> Targets { get; set; }
        public int Cost { get; private set; }

        public Tower(Rectangle drawBox, Texture2D texture, int radius, int cost) : base(drawBox, texture, radius)
        {
            Cost = cost;
        }

        public virtual void Update(List<Enemy> enemies, GameTime gameTime)
        {
            base.Update();
        }
    }
}
