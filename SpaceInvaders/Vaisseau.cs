using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace SpaceInvaders
{
    internal abstract class Vaisseau : GameObject
    {
        protected Missile tir = null;

        /// <summary>
        /// Constructeur de vaisseau
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="vie"></param>
        public Vaisseau(float x, float y, int vie) : base(x,y,vie)
        {
        }

    }
}
