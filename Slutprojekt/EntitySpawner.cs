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
        private static Random rng = new Random();
        public static Queue<Enemy> EnemiesToSpawn { get; private set; }
        /// <summary>
        /// Generate next set of enemies and add them to EnemiesToSpawn queue
        /// </summary>
        /// <param name="enemyTypes">List of diffrent enemy types that can be added</param>
        public static void NextWave(List<Enemy> enemyTypes)
        {
            wave++;
            spawnPoints = wave * 100 * (0.5 + rng.NextDouble());
            Queue<Enemy> enemiesToSpawn = GenerateEnemies(spawnPoints, enemyTypes);
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
        private static Queue<Enemy> GenerateEnemies(double points, List<Enemy> enemyTypes)
        {
            List<ElitType> elitTypes = new List<ElitType>();
            List<NormalType> normalTypes = new List<NormalType>();
            Queue<Enemy> enemiesToSpawn = new Queue<Enemy>();
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
            while(points >= 10)
            {
                if (rng.Next(0, 5) != 0)
                {
                    enemiesToSpawn.Enqueue(normalTypes[rng.Next(0, normalTypes.Count - 1)]);
                    points -= 10;
                }
                else
                {
                    if (points >= 100)
                    {
                        enemiesToSpawn.Enqueue(elitTypes[rng.Next(0, elitTypes.Count - 1)]);
                        points -= 100;
                    }
                }
            }
            return enemiesToSpawn;
        }
    }
}
