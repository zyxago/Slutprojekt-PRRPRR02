using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Slutprojekt.GameObject;

namespace Slutprojekt.GameObjects.Towers
{
    sealed class SpellTower : Tower, ISpell
    {
        public string SpellKey { get; set; }
        public int SpellCooldown { get;set; }
        public int SpellRadius { get; set; }
        public TimeSpan Cooldown { get; set; } = new TimeSpan(0);
        public bool SpellReady { get; set; }
        public bool SpellActivate { get; set; }

        public SpellTower(Rectangle drawBox, Texture2D texture, int radius, int cost, string name, string spell, int spellCooldown, int spellRadius) :base(drawBox, texture, radius, cost, name)
        {
            SpellKey = spell;
            SpellCooldown = spellCooldown;
            SpellRadius = spellRadius;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="enemies"></param>
        /// <param name="gameTime"></param>
        public override void Update(List<Enemy> enemies, GameTime gameTime)
        {
            base.Update(enemies, gameTime);
            if(Cooldown <= gameTime.TotalGameTime)
                SpellReady = true;
            if (SpellActivate)
            {
                ActivateSpell(enemies);
                Cooldown = gameTime.TotalGameTime.Add(new TimeSpan(0, 0, SpellCooldown));
                SpellReady = false;
                SpellActivate = false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="enemies"></param>
        /// <param name="towers"></param>
        public void ActivateSpell(List<Enemy> enemies, List<Tower> towers = null)
        {
            Spell.CastSpell(SpellKey, SpellRadius, Center, enemies);
        }
    }
}
