using Microsoft.Xna.Framework.Graphics;
using Slutprojekt.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slutprojekt
{
    static class Menu
    {
        public static Texture2D CurrentTexture { get; set; }

        //enum men vart i menyn man är? tex main menu, option, select map
        //API
        public static Success success { get; set; }
        public static Contents contents { get; set; }
    }
}
