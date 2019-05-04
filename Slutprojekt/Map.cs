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
        public Texture2D Texture { get; set; }
        private Rectangle DrawBox = Game1.graphics.GraphicsDevice.Viewport.Bounds;
        public List<Rectangle> Road = new List<Rectangle>();
        public Queue<Vector2> PathQueue { get; }
        public Map(Texture2D texture, Queue<Vector2> pathQueue)
        {
            Texture = texture;
            PathQueue = pathQueue;
        }
        
        public List<Rectangle> CreateRoad()
        {
            List<Rectangle> road = new List<Rectangle>();
            Queue<Vector2> temp = PathQueue;
            while (!temp.IsEmpty())
            {
                //TODO en function som skapar en väg mellan två vector2
            }
            return road;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, DrawBox, Color.White);
            foreach (Rectangle box in Road)
                spriteBatch.Draw(Texture, box, Color.Black);
        }
    }
}
