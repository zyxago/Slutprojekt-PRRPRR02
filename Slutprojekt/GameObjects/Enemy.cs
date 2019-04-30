using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slutprojekt
{
    abstract class Enemy : GameObject
    {
        List<Texture2D> ExploPixelList = new List<Texture2D>();
        List<Rectangle> ExploRectangles = new List<Rectangle>();
        List<Vector2> ExploDirectionList = new List<Vector2>();
        int ExploMoveSpeed = 5;
        int ExploSize;

        public Enemy(Rectangle drawBox, Texture2D texture) : base(drawBox, texture)
        {

        }

        public void Update()
        {
            for (int i = 0; i < ExploRectangles.Count; i++)
            {
                ExploRectangles[i] = new Rectangle((int)(ExploRectangles[i].X + -ExploDirectionList[i].X * ExploMoveSpeed), (int)(ExploRectangles[i].Y + -ExploDirectionList[i].Y * ExploMoveSpeed), ExploSize, ExploSize);
            }
        }
        //När den dör så exploderar den i så atta lla dens pixlar flyger iväg. Kanske döpa om...
        public void Explode(GraphicsDevice graphicsDevice, Texture2D texture, Rectangle originBox, int scale = 1)
        {
            Color[] rawData;
            Rectangle OriginBox;

            OriginBox = originBox;
            ExploSize = scale;
            rawData = new Color[texture.Width * texture.Height];
            texture.GetData<Color>(rawData);
            //Ger varje pixel textur en rectangle och position utifrån orginal bilden
            for (int i = 0; i < texture.Width; i++)
            {
                for (int j = 0; j < texture.Height; j++)
                {
                    ExploRectangles.Add(new Rectangle(OriginBox.X + (OriginBox.Width / texture.Width * j), OriginBox.Y + (OriginBox.Height / texture.Height * i), ExploSize, ExploSize));
                }
            }
            //Lägger till texturer med 1 pixel i från orginal bilden
            for (int i = 0; i < rawData.Length; i++)
            {
                Color[] tempColorArr = new Color[1] { rawData[i] };
                ExploPixelList.Insert(i, new Texture2D(graphicsDevice, 1, 1));
                ExploPixelList[i].SetData<Color>(tempColorArr);
                Vector2 direction = OriginBox.Center.ToVector2() - ExploRectangles[i].Center.ToVector2();
                direction.Normalize();
                ExploDirectionList.Insert(i, direction);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            for(int i = 0; i < ExploPixelList.Count; i++)
            {
                spriteBatch.Draw(ExploPixelList[i], ExploRectangles[i], Color.White);
            }
        }
    }
}
