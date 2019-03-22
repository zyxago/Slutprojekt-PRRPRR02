using Microsoft.Xna.Framework;
using Slutprojekt.GameObjects.Enemies;
using Slutprojekt.GameObjects.Towers;
using System;
using System.Collections.Generic;
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
            List<Map> maps = new List<Map>();
            foreach (string path in paths)
            {
                Queue<Vector2> points = new Queue<Vector2>();
                XmlDocument docTemp = XmlLoad(path);
                string texturePath = docTemp.SelectSingleNode("/Map/Texture").InnerText;
                var pointsTemp = docTemp.SelectNodes("/Map/Path/point");
                foreach (XmlNode point in pointsTemp)
                {
                    int.TryParse(point.SelectSingleNode("/X").InnerText, out int x);
                    int.TryParse(point.SelectSingleNode("/Y").InnerText, out int y);
                    points.Enqueue(new Vector2(x, y));
                }
                maps.Add(new Map(texturePath, points));
            }
            return maps;
        }
    }
}
