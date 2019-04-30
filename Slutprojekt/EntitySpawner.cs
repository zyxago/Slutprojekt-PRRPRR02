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

        public static Queue<Enemy> NextWave(List<Enemy> enemyTypes)
        {
            wave++;
            spawnPoints = wave * 100 * (0.5 + rng.NextDouble());
            Queue<Enemy> enemiesToSpawn = GenerateEnemies(spawnPoints, enemyTypes);
            return enemiesToSpawn;
        }

        public static void SpawnEnemy(Queue<Enemy> enemiesToSpawn, List<Enemy> enemies)
        {
            if(enemiesToSpawn.Count > 0)
            {
                enemies.Add(enemiesToSpawn.Dequeue());
            }
        }

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
