using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders
{
    internal class Bunkerr : GameObject

    {

        Bitmap imageBunker = SpaceInvaders.Properties.Resources.bunker;

        /// <summary>
        /// Constructeur de Bunker
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="vie"></param>
        public Bunkerr(float x, float y, int vie) : base(x, y,vie)
        {

        }

        /// <summary>
        /// Get et set de imageBunker
        /// </summary>
        public Bitmap ImageBunker
        {
            get { return imageBunker; }
            set { imageBunker = value; }
        }

        /// <summary>
        /// Update de bunker
        /// </summary>
        /// <param name="gameInstance"></param>
        /// <param name="deltaT"></param>
        public override void Update(Game gameInstance, double deltaT)
        {

        }

        /// <summary>
        /// Dessine le bunker
        /// </summary>
        /// <param name="gameInstance"></param>
        /// <param name="graphics"></param>
        public override void Draw(Game gameInstance, Graphics graphics)
        {

            graphics.DrawImage(imageBunker, X, Y, imageBunker.Width, imageBunker.Height);
        }


        /// <summary>
        /// Vérifie la collision d'une missile avec un bunker. 
        /// </summary>
        /// <param name="missile"></param>
        /// <param name="Image"></param>
        /// <param name="gameInstance"></param>
        /// <returns>Retourne vrai si il y a eu collsion, faux sinon</returns>
        public override bool Collision(Missile missile, Bitmap Image)
        {
            return base.Collision(missile, imageBunker);
        }
    }
}

