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
    static class LoadData//TODO: FIXA TRY, CATCH PÅ ALLT SOM HAR MED FILER ATT GÖRA
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
            string texturePath;
            string type = null;
            Microsoft.Xna.Framework.Rectangle drawBox;
            List<T> objects = new List<T>();
            foreach(string path in paths)
            {
                XmlDocument docTemp = XmlLoad(path);
                if (typeof(T) == typeof(Tower))
                {
                    float aRange, aSpeed, aDmg, roadHp, spellCooldown, spellRadius;
                    string spellType = null;
                    Tower.ProjectileType projectileType = Tower.ProjectileType.none;
                    foreach(XmlNode xnode in docTemp)
                    {
                        if (xnode.Name == "CTower")
                        {
                            type = "CTower";
                            break;
                        }
                        else if (xnode.Name == "RTower")
                        {
                            type = "RTower";
                            break;
                        }
                        else if(xnode.Name == "STower")
                        {
                            type = "STower";
                            break;
                        }
                    }
                    try
                    {
                        //Allt detta ska läsas in på alla sorters torn
                        texturePath = docTemp.SelectSingleNode($"/{type}/Texture").InnerText;
                        texture = LoadTexture2D(Game1.graphics.GraphicsDevice, texturePath);
                        float.TryParse(docTemp.SelectSingleNode($"/{type}/Attack/range").InnerText, out aRange);
                        float.TryParse(docTemp.SelectSingleNode($"/{type}/Attack/speed").InnerText, out aSpeed);
                        float.TryParse(docTemp.SelectSingleNode($"/{type}/Attack/dmg").InnerText, out aDmg);
                        if (docTemp.SelectSingleNode($"/{type}/Attack/type").InnerText == "spalsh")
                            projectileType = Tower.ProjectileType.splash;
                        else if (docTemp.SelectSingleNode($"/{type}/Attack/type").InnerText == "pierce")
                            projectileType = Tower.ProjectileType.pierce;
                        else
                            continue;
                        //Om det är ett RoadTower ska denna också läsas in
                        if(type == "RTower")
                        {
                            float.TryParse(docTemp.SelectSingleNode($"/{type}/Hp").InnerText, out roadHp);
                        }
                        //och om det är ett SpellTower ska dessa in 
                        else if(type == "STower")
                        {
                            float.TryParse(docTemp.SelectSingleNode($"/{type}/Spell/cooldown").InnerText, out spellCooldown);
                            float.TryParse(docTemp.SelectSingleNode($"/{type}/Spell/radius").InnerText, out spellRadius);
                            foreach(string Spellkey in Spell.Spells.Keys)
                            {
                                if(docTemp.SelectSingleNode($"{type}/Spell/type").InnerText == Spellkey)
                                {
                                    spellType = docTemp.SelectSingleNode($"{type}/Spell/type").InnerText;
                                    break;
                                }
                            }
                            if(spellType == null)
                            {
                                continue;
                            }
                        }
                    }
                    catch
                    {
                        continue;
                    }
                    drawBox = new Microsoft.Xna.Framework.Rectangle(0, 0, texture.Width, texture.Height);
                    if (type == "CTower")
                        objects.Add(new ClassicTower(drawBox, texture, aRange, aSpeed, aDmg, projectileType) as T);
                    else if (type == "RTower")
                        objects.Add(new RoadTower() as T);
                    else if (type == "STower")
                        objects.Add(new SpellTower() as T);
                }
                else if (typeof(T) == typeof(Enemy))
                {

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
