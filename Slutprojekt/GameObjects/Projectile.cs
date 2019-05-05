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
        private Vector2 Direction;
        private int Speed = 5;
        public bool IsDead { get; set; } = false;
        public Projectile(Rectangle drawbox, Texture2D texture, int radius, Vector2 direction) : base(drawbox, texture, radius)
        {
            Direction = direction;
        }

        public virtual void Update()//TODO Eventuellt ändra på update
        {
            base.Update();
            Drawbox = new Rectangle((int)(Drawbox.X + Direction.X * Speed), (int)(Drawbox.Y + Direction.Y * Speed), Drawbox.Width, Drawbox.Height);
        }

        abstract public void Effect(List<Enemy> enemies, int dmg);
    }
}
