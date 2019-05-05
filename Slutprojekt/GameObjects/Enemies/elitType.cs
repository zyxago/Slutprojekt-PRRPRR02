using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Slutprojekt.GameObject;

namespace Slutprojekt.GameObjects.Enemies
{
    class ElitType : Enemy, ISpell
    {
        public string Spell { get; set; }
        public float SpellCooldown { get; set; }
        public float SpellRadius { get; set; }

        public ElitType(Rectangle drawBox, Texture2D texture, float radius, float speed, float hp, string resistance, string enemySpell, float spellCooldown, float spellRadius, Queue<Vector2> path = null) : base(drawBox, texture, radius, speed, hp, resistance, path)
        {
            Dmg = 10;
            Spell = enemySpell;
            SpellCooldown = spellCooldown;
            SpellRadius = spellRadius;
        }

        public void ActivateSpell()
        {

        }
    }
}
