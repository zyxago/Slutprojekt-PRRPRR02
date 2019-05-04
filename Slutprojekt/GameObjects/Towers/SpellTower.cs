using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slutprojekt.GameObjects.Towers
{
    class SpellTower : Tower
    {
        public string Spell { get; private set; }
        public float SpellCooldown { get; private set; }
        public float SpellRadius { get; private set; }
        public SpellTower(Rectangle drawBox, Texture2D texture, float radius, int cost, string spell, float spellCooldown, float spellRadius) :base(drawBox, texture, radius, cost)
        {
            Spell = spell;
            SpellCooldown = spellCooldown;
            SpellRadius = spellRadius;
        }
    }
}
