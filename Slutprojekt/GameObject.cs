using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slutprojekt
{
    class GameObject
    {
        Rectangle Drawbox { get; set; }
        Texture2D Texture { get; set; }
        float Radius { get; set; }
        public GameObject(Rectangle drawbox, Texture2D texture)
        {
            Drawbox = drawbox;
            Texture = texture;
        }
    }
}
