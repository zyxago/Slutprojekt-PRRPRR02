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
        public Rectangle Drawbox { get; set; }
        public Texture2D Texture { get; set; }
        public float Radius { get; set; }
        public GameObject(Rectangle drawbox, Texture2D texture, float radius)
        {
            Drawbox = drawbox;
            Texture = texture;
            Radius = radius;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Drawbox, Color.White);
        }
    }
}
