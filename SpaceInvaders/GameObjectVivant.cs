using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders
{
    abstract class GameObjectVivant : GameObject
    {
        protected int vie;

        public GameObjectVivant(int vie)
        {
            this.vie = vie;
        }

        public override bool IsAlive()
        {
            return vie > 0;
        }


    }
}
