using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slutprojekt
{
    public abstract class Enemy : GameObject
    {
        private List<Texture2D> ExploPixelList { get; set; } = new List<Texture2D>();
        private List<Rectangle> ExploRectangles { get; set; } = new List<Rectangle>();
        private  List<Vector2> ExploDirectionList { get; set; } = new List<Vector2>();
        private int ExploMoveSpeed { get; set; } = 3;
        private int ExploSize { get; set; }
        public TimeSpan ExploTime { get; set; } = new TimeSpan();

        public Queue<Vector2> Path { get; set; } = new Queue<Vector2>();
        Vector2 Direction = new Vector2();
        public int Dmg { get; set; }
        public int Speed { get; set; }
        public int Hp { get; set; }
        public string Resistance { get; set; }
        public bool IsDead { get; set; }
        public int Worth { get; set; } = 0;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="drawBox"></param>
        /// <param name="texture"></param>
        /// <param name="radius"></param>
        /// <param name="speed"></param>
        /// <param name="hp"></param>
        /// <param name="resistance"></param>
        /// <param name="path"></param>
        public Enemy(Rectangle drawBox, Texture2D texture, int radius, int speed, int hp, string resistance, Queue<Vector2> path) : base(drawBox, texture, radius)
        {
            Speed = speed;
            Hp = hp;
            Resistance = resistance;
            Path = path;
        }

        /// <summary>
        /// 
        /// </summary>
        private void Move()
        {
            if (Vector2.Distance(Drawbox.Center.ToVector2(), Path.Peek()) < 10)
                Path.Dequeue();
            Direction = Path.Peek() - Drawbox.Center.ToVector2();
            Direction.Normalize();
            Drawbox = new Rectangle((int)(Drawbox.X + Direction.X * Speed), (int)(Drawbox.Y + Direction.Y * Speed), Drawbox.Width, Drawbox.Height);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            base.Update();
            if (Path.IsEmpty() && !IsDead)
            {
                IsDead = true;
                Hud.Hp -= Dmg;
            }
            if(Hp <= 0 && !IsDead)
            {
                IsDead = true;
                Hud.Money += Worth;
                ExploTime = gameTime.TotalGameTime.Add(new TimeSpan(0, 0, 1));
                Explode(Game1.graphics.GraphicsDevice, Texture, Drawbox, 3);
            }
            for (int i = 0; i < ExploRectangles.Count; i++)
            {
                ExploMoveSpeed = Game1.rng.Next(1, 6);
                ExploRectangles[i] = new Rectangle((int)(ExploRectangles[i].X + -ExploDirectionList[i].X * ExploMoveSpeed), (int)(ExploRectangles[i].Y + -ExploDirectionList[i].Y * ExploMoveSpeed), ExploSize, ExploSize);
            }
            if (!IsDead)
                Move();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="graphicsDevice"></param>
        /// <param name="texture"></param>
        /// <param name="originBox"></param>
        /// <param name="scale"></param>
        private void Explode(GraphicsDevice graphicsDevice, Texture2D texture, Rectangle originBox, int scale = 1)
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
                Color[] tempColorArr = new Color[] { rawData[i] };
                ExploPixelList.Insert(i, new Texture2D(graphicsDevice, 1, 1));
                ExploPixelList[i].SetData<Color>(tempColorArr);
                Vector2 direction = OriginBox.Center.ToVector2() - ExploRectangles[i].Center.ToVector2();
                direction.Normalize();
                ExploDirectionList.Insert(i, direction);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="spriteBatch"></param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!IsDead)
                base.Draw(spriteBatch);
            for (int i = 0; i < ExploPixelList.Count; i++)
            {
                spriteBatch.Draw(ExploPixelList[i], ExploRectangles[i], Color.White);
            }
        }
    }
}
