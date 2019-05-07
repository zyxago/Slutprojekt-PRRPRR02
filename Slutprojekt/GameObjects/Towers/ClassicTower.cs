using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Slutprojekt.GameObjects.Projectiles;
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
        public int AttackRange { get; set; }
        public float AttackSpeed { get; set; }
        public int AttackDMG { get; set; }
        public int ProjectileRadius { get; set; }
        public Rectangle ProjectileDrawbox { get; set; }
        public Texture2D ProjectileTexture { get; set; }
        public List<Projectile> Projectiles { get; set; } = new List<Projectile>();
        public TimeSpan AttackDelay { get; set; } = new TimeSpan(0, 0, 0, 0, 1);
        public int ProjectileEffect { get; set; }

        public enum ProjectileType
        {
            splash,
            pierce
        };
        public ProjectileType Ptype { get; set; }

        public ClassicTower(Rectangle drawBox, Texture2D texture, int radius, int cost, Texture2D projectileTexture, int Arange, float Aspeed, int Admg, string projectileType, int projectileEffect) : base(drawBox, texture, radius, cost)
        {
            AttackRange = Arange;
            AttackSpeed = Aspeed;
            AttackDMG = Admg;
            ProjectileEffect = projectileEffect;
            ProjectileTexture = projectileTexture;
            ProjectileRadius = ProjectileTexture.Width;
            ProjectileDrawbox = new Rectangle(Drawbox.X + drawBox.Width / 2, Drawbox.Y + drawBox.Height / 2, ProjectileTexture.Width, ProjectileTexture.Height);
            if (projectileType == "splash")
                Ptype = ProjectileType.splash;
            else if (projectileType == "pierce")
                Ptype = ProjectileType.pierce;
        }

        public override void Update(List<Enemy> enemies, GameTime gameTime)
        {
            base.Update(enemies, gameTime);
            foreach (Enemy enemy in enemies)
            {
                if (AttackDelay <= gameTime.TotalGameTime && Game1.CheckIfInRange(enemy.Center, enemy.Radius, Center, AttackRange))
                {
                    AttackDelay = gameTime.TotalGameTime.Add(new TimeSpan(0, 0, 0, 0, (int)(AttackSpeed * 1000)));
                    Attack(enemy);
                    break;
                }
            }
            for(int i = 0; i < Projectiles.Count; i++)
            {
                Projectiles[i].Update();
                for(int j = 0; j < enemies.Count; j++)
                {
                    if (Game1.CheckIfInRange(enemies[j].Center, enemies[j].Radius, Projectiles[i].Center, Projectiles[i].Radius))
                    {
                        Projectiles[i].Effect(enemies, AttackDMG, j);//TODO kolla så att det funkar eventuellt fixa nått nytt sätt
                        break;
                    }
                }
                if (Projectiles[i].IsDead)
                {
                    Projectiles.RemoveAt(i);
                    i--;
                }
            }

        }

        public void Attack(Enemy target)
        {
            Vector2 direction = target.Drawbox.Center.ToVector2() - Drawbox.Center.ToVector2();
            direction.Normalize();
            if (Ptype == ProjectileType.pierce)
                Projectiles.Add(new PierceProjectile(ProjectileDrawbox, ProjectileTexture, ProjectileRadius, direction, ProjectileEffect));
            else if (Ptype == ProjectileType.splash)
                Projectiles.Add(new SplashProjectile(ProjectileDrawbox, ProjectileTexture, ProjectileRadius, direction, ProjectileEffect));
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            foreach (Projectile bullet in Projectiles)
                bullet.Draw(spriteBatch);
        }
    }
}
