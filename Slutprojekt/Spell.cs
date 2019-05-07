using Microsoft.Xna.Framework;
using Slutprojekt.Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slutprojekt
{
    abstract class Spell
    {
        public static Dictionary<string, Action<float, Vector2, List<Enemy>>> Tspells = new Dictionary<string, Action<float, Vector2, List<Enemy>>>();
        public static Dictionary<string, Action<float, Vector2, List<Tower>>> Espells = new Dictionary<string, Action<float, Vector2, List<Tower>>>();

        public static void LoadSpells()
        {
            Tspells.Add("Tslow", TowerSpells.Slow);

            Espells.Add("Eslow", EntitySpells.Slow);
        }
        
        public static void CastSpell(string spellKey, int radius, Vector2 center, List<Enemy> enemies)
        {
            Tspells[spellKey].Invoke(radius, center, enemies);
        }

        public static void Castspell(string spellkey, int radius, Vector2 center, List<Tower> towers)
        {
            Espells[spellkey].Invoke(radius, center, towers);
        }
    }
}
