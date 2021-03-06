﻿using Microsoft.Xna.Framework;
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
        /// Laddar in GameObjects såsom torn och fiender
        /// </summary>
        /// <typeparam name="T">Tower, Enemy</typeparam>
        /// <param name="paths">En array med filsökvägar</param>
        /// <returns></returns>
        public static List<T> Load<T>(string[] paths) where T : GameObject
        {
            Texture2D texture;
            string texturePath;
            string type = null;
            Microsoft.Xna.Framework.Rectangle drawBox;
            List<T> objects = new List<T>();
            foreach (string path in paths)
            {
                try
                {
                    XmlDocument docTemp = XmlLoad(path);
                    if (typeof(T) == typeof(Tower))
                    {
                        Texture2D projectileTexture = null;
                        string projectiletexturePath = null;
                        string name = null;
                        int radius = 0, aRange = 0, aDmg = 0, roadHp = 1, spellCooldown = 0, spellRadius = 0, towerHp = 0, towerCost = 1, projectileEffectInt = 0;
                        float aSpeed = 0;
                        string spellType = null, projectileType = null;
                        foreach (XmlNode xnode in docTemp)
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
                            else if (xnode.Name == "STower")
                            {
                                type = "STower";
                                break;
                            }
                        }
                        texturePath = docTemp.SelectSingleNode($"/{type}/Texture").InnerText;
                        texture = LoadTexture2D(Game1.graphics.GraphicsDevice, texturePath);
                        drawBox = new Microsoft.Xna.Framework.Rectangle(0, 0, texture.Width, texture.Height);
                        int.TryParse(docTemp.SelectSingleNode($"/{type}/Cost").InnerText, out towerCost);
                        int.TryParse(docTemp.SelectSingleNode($"/{type}/Radius").InnerText, out radius);
                        name = docTemp.SelectSingleNode($"{type}/Name").InnerText;
                        if (type != "STower")
                        {
                            projectiletexturePath = docTemp.SelectSingleNode($"/{type}/Attack/projectileTexture").InnerText;
                            projectileTexture = LoadTexture2D(Game1.graphics.GraphicsDevice, projectiletexturePath);
                            int.TryParse(docTemp.SelectSingleNode($"/{type}/Attack/range").InnerText, out aRange);
                            float.TryParse(docTemp.SelectSingleNode($"/{type}/Attack/speed").InnerText, out aSpeed);
                            int.TryParse(docTemp.SelectSingleNode($"/{type}/Attack/dmg").InnerText, out aDmg);
                            int.TryParse(docTemp.SelectSingleNode($"/{type}/Attack/typeModifier").InnerText, out projectileEffectInt);
                            if (docTemp.SelectSingleNode($"/{type}/Attack/type").InnerText == "pierce")
                                projectileType = docTemp.SelectSingleNode($"/{type}/Attack/type").InnerText;
                            else if (docTemp.SelectSingleNode($"/{type}/Attack/type").InnerText == "splash")
                                projectileType = docTemp.SelectSingleNode($"/{type}/Attack/type").InnerText;
                            else
                                continue;
                            if (type == "RTower")
                            {
                                int.TryParse(docTemp.SelectSingleNode($"/{type}/Hp").InnerText, out roadHp);
                            }
                        }
                        else if (type == "STower")
                        {
                            int.TryParse(docTemp.SelectSingleNode($"/{type}/Spell/cooldown").InnerText, out spellCooldown);
                            int.TryParse(docTemp.SelectSingleNode($"/{type}/Spell/radius").InnerText, out spellRadius);
                            foreach (string Spellkey in Spell.Tspells.Keys)
                            {
                                if (docTemp.SelectSingleNode($"{type}/Spell/type").InnerText == Spellkey)
                                {
                                    spellType = docTemp.SelectSingleNode($"{type}/Spell/type").InnerText;
                                    break;
                                }
                            }
                            if (spellType == null)
                            {
                                continue;
                            }
                        }
                        if (type == "CTower")
                            objects.Add(new ClassicTower(drawBox, texture, radius, towerCost, name, projectileTexture, aRange, aSpeed, aDmg, projectileType, projectileEffectInt) as T);
                        else if (type == "RTower")
                            objects.Add(new RoadTower(drawBox, texture, radius, towerCost, name, projectileTexture, aRange, aSpeed, aDmg, projectileType, projectileEffectInt, towerHp) as T);
                        else if (type == "STower")
                            objects.Add(new SpellTower(drawBox, texture, radius, towerCost, name, spellType, spellCooldown, spellRadius) as T);
                    }
                    else if (typeof(T) == typeof(Enemy))
                    {
                        int hp, radius, speed, spellCooldown = 0, spellRadius = 0;
                        string resistance;
                        string enemySpell = null;
                        foreach (XmlNode xnode in docTemp)
                        {
                            if (xnode.Name == "NEnemy")
                            {
                                type = "NEnemy";
                                break;
                            }
                            else if (xnode.Name == "EEnemy")
                            {
                                type = "EEnemy";
                                break;
                            }
                        }
                        texturePath = docTemp.SelectSingleNode($"{type}/Texture").InnerText;
                        texture = LoadTexture2D(Game1.graphics.GraphicsDevice, texturePath);
                        drawBox = new Microsoft.Xna.Framework.Rectangle(0, 0, texture.Width, texture.Height);
                        int.TryParse(docTemp.SelectSingleNode($"/{type}/Speed").InnerText, out speed);
                        int.TryParse(docTemp.SelectSingleNode($"/{type}/Hp").InnerText, out hp);
                        int.TryParse(docTemp.SelectSingleNode($"/{type}/Radius").InnerText, out radius);
                        if (docTemp.SelectSingleNode($"/{type}/Resistance").InnerText == "splash")
                        {
                            resistance = docTemp.SelectSingleNode($"/{type}/Resistance").InnerText;
                        }
                        else if (docTemp.SelectSingleNode($"/{type}/Resistance").InnerText == "pierce")
                        {
                            resistance = docTemp.SelectSingleNode($"/{type}/Resistance").InnerText;
                        }
                        else
                            continue;
                        if (type == "EEnemy")
                        {
                            int.TryParse(docTemp.SelectSingleNode($"/{type}/Ability/cooldown").InnerText, out spellCooldown);
                            int.TryParse(docTemp.SelectSingleNode($"/{type}/Ability/radius").InnerText, out spellRadius);
                            foreach (string Spellkey in Spell.Espells.Keys)
                            {
                                if (docTemp.SelectSingleNode($"{type}/Ability/type").InnerText == Spellkey)
                                {
                                    enemySpell = docTemp.SelectSingleNode($"{type}/Ability/type").InnerText;
                                    break;
                                }
                            }
                            if (enemySpell == null)
                            {
                                continue;
                            }

                        }
                        if (type == "NEnemy")
                            objects.Add(new NormalType(drawBox, texture, radius, speed, hp, resistance) as T);
                        else if (type == "EEnemy")
                            objects.Add(new ElitType(drawBox, texture, radius, speed, hp, resistance, enemySpell, spellCooldown, spellRadius) as T);
                    }
                    else
                    {
                        throw new Exception("Invalid data type");
                    }
                }
                catch
                {
                    continue;
                }
            }
            return objects;
        }
        /// <summary>
        /// Laddar in kartor utifrpn 'paths' och returnerar en lista med Map object
        /// </summary>
        /// <param name="paths">En array med filsökvägar</param>
        /// <returns>En lista med Map objekt</returns>
        public static List<Map> Load(string[] paths)
        {
            Texture2D texture;
            List<Map> maps = new List<Map>();
            foreach (string path in paths)
            {
                try
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
                catch
                {
                    continue;
                }
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
            try
            {
                Bitmap picture = new Bitmap(texturePath);
                Microsoft.Xna.Framework.Color[] rawData = new Microsoft.Xna.Framework.Color[picture.Width * picture.Height];
                for (int y = 0; y < picture.Height; y++)
                {
                    for (int x = 0; x < picture.Width; x++)
                    {
                        System.Drawing.Color tempColor = picture.GetPixel(x, y);
                        rawData[y * picture.Width + x] = new Microsoft.Xna.Framework.Color(tempColor.R, tempColor.G, tempColor.B, tempColor.A);
                    }
                }
                Texture = new Texture2D(graphicsDevice, picture.Width, picture.Height);
                Texture.SetData(rawData);
            }
            catch
            {
                return Game1.ErrorTex;
            }
            return Texture;
        }
    }
}
