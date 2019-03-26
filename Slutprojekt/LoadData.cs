using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Slutprojekt.GameObjects.Enemies;
using Slutprojekt.GameObjects.Towers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Slutprojekt
{
    static class LoadData
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private static XmlDocument XmlLoad(string path)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(path);
            return doc;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="paths"></param>
        /// <returns>En lista med GameObject objekt</returns>
        public static List<T> Load<T>(string[] paths) where T : GameObject
        {
            Texture2D texture;
            List<T> objects = new List<T>();
            foreach(string path in paths)
            {
                XmlDocument docTemp = XmlLoad(path);
                string texturePath = docTemp.SelectSingleNode("/Map/Texture").InnerText;
                if (typeof(T) == typeof(Towers))
                {
                    if (typeof(T) == typeof(ClassicTower))
                    {

                    }
                    else if (typeof(T) == typeof(RoadTower))
                    {

                    }
                    else if (typeof(T) == typeof(SpellTower))
                    {

                    }
                }
                else if (typeof(T) == typeof(Enemies))
                {
                    if (typeof(T) == typeof(NormalType))
                    {

                    }
                    else if (typeof(T) == typeof(ElitType))
                    {

                    }
                }
            }
            return objects;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="paths"></param>
        /// <returns>En lista med Map objekt</returns>
        public static List<Map> LoadMaps(string[] paths)
        {
            Texture2D texture;
            List<Map> maps = new List<Map>();
            foreach (string path in paths)
            {
                Queue<Vector2> points = new Queue<Vector2>();
                XmlDocument docTemp = XmlLoad(path);
                string texturePath = docTemp.SelectSingleNode("/Map/Texture").InnerText;
                var pointsTemp = docTemp.SelectNodes("/Map/Path/point");
                foreach (XmlNode point in pointsTemp)
                {
                    int.TryParse(point.SelectSingleNode("X").InnerText, out int x);
                    int.TryParse(point.SelectSingleNode("Y").InnerText, out int y);
                    points.Enqueue(new Vector2(x, y));
                }
                texture = LoadTexture2D(Game1.graphics.GraphicsDevice, texturePath);
                maps.Add(new Map(texture, points));
            }
            return maps;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="graphicsDevice">Xnas grafik hanterare behövs för att skapa en Texture2D</param>
        /// <param name="texturePath">Filsökvägen till bilden som ska laddas in</param>
        /// <returns>returnerar en Texture2D som har samma ColorData som bilden som man laddade in</returns>
        public static Texture2D LoadTexture2D(GraphicsDevice graphicsDevice, string texturePath)
        {
            Texture2D Texture;
            Bitmap picture = new Bitmap(texturePath);
            Microsoft.Xna.Framework.Color[] rawData = new Microsoft.Xna.Framework.Color[picture.Width * picture.Height];
            for (int y = 0; y < picture.Height; y++)
            {
                for(int x = 0; x < picture.Width; x++)
                {
                    System.Drawing.Color tempColor = picture.GetPixel(x, y);
                    rawData[y * picture.Width + x] = new Microsoft.Xna.Framework.Color(tempColor.R, tempColor.G, tempColor.B);
                }
            }
            Texture = new Texture2D(graphicsDevice, picture.Width , picture.Height);
            Texture.SetData(rawData);
            return Texture;
        }
    }
}
