using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace Slutprojekt
{
    static class ConstructTexture
    {
        public static Texture2D ConstructTexture2D(GraphicsDevice graphicsDevice, string texturePath)
        {
            Bitmap
            Color[] rawData;
            Texture2D Texture;
            rawData = new Color[/*Bildens: Width*/ * /*Bildens: Height*/];
            boxText.GetData<Color>(rawData);
            //Lägger till texturer med 1 pixel i från orginal bilden
            for (int i = 0; i < rawData.Length; i++)
            {
                Color[] tempColorArr = new Color[1] { rawData[i] };
                pixelList.Insert(i, new Texture2D(graphicsDevice, 1, 1));
                pixelList[i].SetData<Color>(tempColorArr);
                Vector2 direction = OriginBox.Center.ToVector2() - rectangles[i].Center.ToVector2();
                direction.Normalize();
                directionList.Insert(i, direction);
            }
            Texture = new Texture2D(graphicsDevice, /*Bildens: Width*/ , /*Bildens: Height*/)
            return Texture;
        }

    }
}
