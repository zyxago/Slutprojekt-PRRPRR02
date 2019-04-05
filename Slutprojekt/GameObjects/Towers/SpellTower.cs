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
        string Spell;
        public float Cooldown { get; set; }
        public float SpellRadius { get; set; }
        public SpellTower(Rectangle drawBox, Texture2D texture, string spell, float spellCooldown, float spellRadius) :base(drawBox, texture)
        {
            Spell = spell;
            Cooldown = spellCooldown;
            SpellRadius = spellRadius;
        }
    }
}
