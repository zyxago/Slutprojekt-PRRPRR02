using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slutprojekt
{
    class Map
    {
        Texture2D Texture;
        Queue<Vector2> PathQueue;
        public Map(Texture2D texture, Queue<Vector2> pathQueue)
        {
            Texture = texture;
            PathQueue = pathQueue;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Vector2.Zero, Color.White);
        }
    }
}
