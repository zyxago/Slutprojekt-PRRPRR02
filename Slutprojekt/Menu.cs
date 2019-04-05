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

        public enum Location
        {
            MainMenu,
            Options, 
            MapSelector,
            TowerSelector
        };
        public static Location location;
        //API
        public static Success success { get; set; }
        public static Contents contents { get; set; }
    }
}
