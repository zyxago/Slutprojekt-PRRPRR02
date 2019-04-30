using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slutprojekt
{
    abstract class Tower : GameObject
    {
        public interface IAttack
        {
            float AttackRange { get; set; }
            float AttackSpeed { get; set; }
            float AttackDMG { get; set; }
            void Attack();
        }
        public int Cost { get; private set; }

        public Tower(Rectangle drawBox, Texture2D texture, int cost) : base(drawBox, texture)
        {
            Cost = cost;
        }

        public void Update()
        {

        }
    }
}
