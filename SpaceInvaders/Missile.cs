using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpaceInvaders
{
    internal class Missile : GameObject
    {
        
        Bitmap imageMissile = SpaceInvaders.Properties.Resources.shoot1;
        private double missileSpeed;
        int type; 

        /// <summary>
        /// Constructeur de missiles
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="type"></param>
        public Missile(float x, float y, int type) : base(x,y,1)
        {
            
            this.type = type;
            if(type == 0)
            {
                missileSpeed = 500;
            }
            else {
                missileSpeed = 100;
            
            }
        }

        /// <summary>
        /// Get du type de missile
        /// </summary>
        public int Type
        {
            get { return type; }
        }

        /// <summary>
        /// Get de l'image du missile
        /// </summary>
        public Bitmap ImageMissile
        {
            get { return imageMissile; }
        }


        /// <summary>
        /// Dessine le missile
        /// </summary>
        /// <param name="gameInstance"></param>
        /// <param name="graphics"></param>
        public override void Draw(Game gameInstance, Graphics graphics)
        {
            
            graphics.DrawImage(imageMissile, X, Y, imageMissile.Width, imageMissile.Height);
        }

        /// <summary>
        /// Update de missile
        /// </summary>
        /// <param name="gameInstance"></param>
        /// <param name="deltaT"></param>
        public override void Update(Game gameInstance, double deltaT)
        {
            if (type == 0)
            {
                Y -= (float)missileSpeed * (float)deltaT;

            }
            else if (type == 1)
            {
                Y += (float)missileSpeed * (float)deltaT;
            }
            if (Y > gameInstance.gameSize.Height + imageMissile.Height || Y < 0)
                Vie = 0;

        }

        /// <summary>
        /// Collision missile/missile
        /// </summary>
        /// <param name="missile"></param>
        /// <param name="Image"></param>
        /// <returns>Vrai si il y a eu collision, faux sinon</returns>
        public override bool Collision(Missile missile, Bitmap Image)
        {
            return base.Collision(missile, imageMissile);
        }

    }
}
