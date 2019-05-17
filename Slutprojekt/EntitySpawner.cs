using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Slutprojekt.GameObjects.Enemies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slutprojekt
{
    static class EntitySpawner
    {
        public static int wave = 0;
        private static double spawnPoints;
        public static Queue<Enemy> EnemiesToSpawn { get; private set; }
        /// <summary>
        /// Generate next set of enemies and add them to EnemiesToSpawn queue
        /// </summary>
        /// <param name="enemyTypes">List of diffrent enemy types that can be added</param>
        public static void NextWave(List<Enemy> enemyTypes, Queue<Vector2> path)
        {
            Queue<Vector2> Path = new Queue<Vector2>(path);
            wave++;
            spawnPoints = wave * 100 * (0.5 + Game1.rng.NextDouble());
            Queue<Enemy> enemiesToSpawn = GenerateEnemies(spawnPoints, enemyTypes, Path);
            EnemiesToSpawn = enemiesToSpawn;
        }
        /// <summary>
        /// Add new enemy from EnemiesToSpawn queue to enemies list
        /// </summary>
        /// <param name="enemies">The list you want to add enemies to</param>
        public static void SpawnNextEnemy(List<Enemy> enemies)
        {
            enemies.Add(EnemiesToSpawn.Dequeue());
        }
        /// <summary>
        /// Generates a queue of enemies based of number of points it has
        /// </summary>
        /// <param name="points">Points to spend on enemies, 10 points normalType - 100 points elitType</param>
        /// <param name="enemyTypes">List of diffrent enemy types that can be added</param>
        /// <returns></returns>
        private static Queue<Enemy> GenerateEnemies(double points, List<Enemy> enemyTypes, Queue<Vector2> path)
        {
            List<ElitType> elitTypes = new List<ElitType>();
            List<NormalType> normalTypes = new List<NormalType>();
            Queue<Enemy> enemiesToSpawn = new Queue<Enemy>();
            Vector2 spawnPos = path.Peek();
            foreach (Enemy enemy in enemyTypes)
            {
                if(enemy is ElitType)
                {
                    elitTypes.Add(enemy as ElitType);
                }
                else if(enemy is NormalType)
                {
                    normalTypes.Add(enemy as NormalType);
                }
            }
            while (points >= 10)
            {
                Texture2D texture;
                int n = Game1.rng.Next(0, normalTypes.Count - 1);
                int e = Game1.rng.Next(0, elitTypes.Count - 1);
                if (Game1.rng.Next(0, 5) != 0)
                {
                    texture = new Texture2D(Game1.graphics.GraphicsDevice, normalTypes[n].Texture.Width, normalTypes[n].Texture.Height);
                    Color[] rawData = new Color[texture.Width * texture.Height];
                    normalTypes[n].Texture.GetData(rawData);
                    texture.SetData(rawData);
                    enemiesToSpawn.Enqueue(new NormalType(new Rectangle((int)spawnPos.X - normalTypes[n].Texture.Width/2, (int)spawnPos.Y - normalTypes[n].Texture.Height/2, normalTypes[n].Texture.Width, normalTypes[n].Texture.Height), texture, normalTypes[n].Radius, normalTypes[n].Speed, normalTypes[n].Hp, normalTypes[n].Resistance, new Queue<Vector2>(path)));
                    points -= 10;
                }
                else
                {
                    texture = new Texture2D(Game1.graphics.GraphicsDevice, elitTypes[e].Texture.Width, elitTypes[e].Texture.Height);
                    Color[] rawData = new Color[texture.Width * texture.Height];
                    elitTypes[e].Texture.GetData(rawData);
                    texture.SetData(rawData);
                    if (points >= 100)
                    {
                        enemiesToSpawn.Enqueue(new ElitType(new Rectangle((int)spawnPos.X - elitTypes[e].Texture.Width/2, (int)spawnPos.Y - elitTypes[e].Texture.Height/2, elitTypes[e].Texture.Width, elitTypes[e].Texture.Height), texture, elitTypes[e].Radius, elitTypes[e].Speed, elitTypes[e].Hp, elitTypes[e].Resistance, elitTypes[e].SpellKey, elitTypes[e].SpellCooldown, elitTypes[e].SpellRadius, new Queue<Vector2>(path)));
                        points -= 100;
                    }
                }
            }
            return enemiesToSpawn;
        }

        /// <summary>
        /// 
        /// </summary>
        public static void Reset()
        {
            wave = 0;
        }
    }
}
