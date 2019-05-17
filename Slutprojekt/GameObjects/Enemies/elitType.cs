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
    sealed class ElitType : Enemy, ISpell
    {
        public string SpellKey { get; set; }
        public int SpellCooldown { get; set; }
        public int SpellRadius { get; set; }
        public TimeSpan Cooldown { get; set; }

        public ElitType(Rectangle drawBox, Texture2D texture, int radius, int speed, int hp, string resistance, string enemySpell, int spellCooldown, int spellRadius, Queue<Vector2> path = null) : base(drawBox, texture, radius, speed, hp, resistance, path)
        {
            Worth = 28;
            Dmg = 10;
            SpellKey = enemySpell;
            SpellCooldown = spellCooldown;
            SpellRadius = spellRadius;
        }

        public override void Update(List<Tower> towers, GameTime gameTime)
        {
            base.Update(towers, gameTime);
            if (Cooldown <= gameTime.TotalGameTime)
            {
                Cooldown = gameTime.TotalGameTime.Add(new TimeSpan(0, 0, SpellCooldown));
                ActivateSpell(null ,towers);
            }
        }

        public void ActivateSpell(List<Enemy> enemies = null,List<Tower> towers = null)
        {
            Spell.CastSpell(SpellKey, SpellRadius, Center, towers);
        }
    }
}
