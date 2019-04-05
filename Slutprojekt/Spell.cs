﻿using Microsoft.Xna.Framework;
using Slutprojekt.Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slutprojekt
{
    static class Spell
    {
        public static Dictionary<string, Action<float, Vector2>> Spells = new Dictionary<string, Action<float, Vector2>>();
        public static void LoadSpells()
        {
            Spells.Add("slow", TowerSpells.Slow);
        }
    }
}
