using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slutprojekt
{
    abstract public class GameObject
    {
        public interface ISpell
        {
            string Spell { get; set; }
            int SpellCooldown { get; set; }
            int SpellRadius { get; set; }
            void ActivateSpell();
        }

        public Rectangle Drawbox { get; set; }
        public Texture2D Texture { get; set; }
        public int Radius { get; set; }
        public Color Color { get; set; } = Color.White;
        public Vector2 Center { get; set; }

        public GameObject(Rectangle drawbox, Texture2D texture, int radius)
        {
            Drawbox = drawbox;
            Texture = texture;
            Radius = radius;
            Center = Drawbox.Center.ToVector2();
        }

        public virtual void Update()
        {
            Center = Drawbox.Center.ToVector2();
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Drawbox, Color);
        }
    }
}
