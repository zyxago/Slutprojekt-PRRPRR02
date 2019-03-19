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
        public static List<T> Load<T>(string[] paths)
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
                else if (typeof(T) == typeof(Map))
                {
                    List<Vector2> points = new List<Vector2>();
                    var pointsTemp = docTemp.SelectNodes("/Map/Path/point");
                    foreach(var point in pointsTemp)
                    {
                        //TODO: gör så att en ny vector läggs till i points listan.
                    }
                }
            }
            return objects;
        }
    }
}
