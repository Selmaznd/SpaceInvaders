using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace SpaceInvaders 
{
    internal class Enemy : Vaisseau
    {

        private int type;
        private bool peutTirer = true;
        Bitmap enemyImage;

        /// <summary>
        /// Constructeur de Enemy
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="type"></param>
        public Enemy(float x, float y, int type) : base(x, y, 1)
        {
            this.type = type;
        }
        /// <summary>
        /// Get et Set de EnemyImage
        /// </summary>
        public Bitmap EnemyImage
        {
            get { return enemyImage; }
            set { enemyImage = value; }
        }

        /// <summary>
        /// Update de Enemy
        /// </summary>
        /// <param name="gameInstance"></param>
        /// <param name="deltaT"></param>
        public override void Update(Game gameInstance, double deltaT)
        {
            if (Y > gameInstance.gameSize.Height)
            {
                Vie = 0;
            }
            if (tir!=null && !tir.IsAlive()) tir = null;
            
        }

        /// <summary>
        /// L'ennemie tir un missile
        /// </summary>
        /// <param name="gameInstance"></param>
        public void Shoot(Game gameInstance)
        {
            
            if (peutTirer && tir==null)
            {
                Missile m = new Missile(X+15, Y+15, 1);
                gameInstance.AddNewGameObject(m);
                gameInstance.Missiles.Add(m);

                peutTirer = false;
                tir = m;
                // latence pour éviter double tir
                Task.Delay(1000).ContinueWith(t => peutTirer = true);
                
            }
            
        }

        /// <summary>
        /// Détermine si un bonus peut apparaître. Un bonus peut apparaître si un ennemie meurt
        /// La première case du tableau vaut 1 si le bonus peut appraître, 0 sinon
        /// Les deux autres cases du tableau sont les positions respectives X et Y de la position
        /// de création du bonus
        /// </summary>
        /// <returns>Tableau contenant :si un bonus peut apparaître ou non et les positions où il pourrait appraître</returns>
        public float[] boostPossible()
        {
            float[] tab = new float[3];
            if (!IsAlive())
            {
                tab[0] = 1; //on peut crée un boost
                tab[1] = X;
                tab[2] = Y;
            }
            else
            {
                tab[0] = 0; //on ne peut pas crée de boost
            }
            return tab;
        }
        /// <summary>
        /// Collision Enemy/missile
        /// </summary>
        /// <param name="missile"></param>
        /// <param name="Image"></param>
        /// <returns>Vrai si il y a eu une collision, faux sinon</returns>
        public override bool Collision(Missile missile, Bitmap Image)
        {
            return base.Collision(missile, enemyImage);
        }

        /// <summary>
        /// Dessine l'ennemie
        /// </summary>
        /// <param name="gameInstance"></param>
        /// <param name="graphics"></param>
        public override void Draw(Game gameInstance, Graphics graphics)
        {
            //Bitmap enemyImage;

            switch (type)
            {
                case 2:
                    enemyImage = SpaceInvaders.Properties.Resources.ship2;
                    break;
                case 3:
                    enemyImage = SpaceInvaders.Properties.Resources.ship3;
                    break;
                case 4:
                    enemyImage = SpaceInvaders.Properties.Resources.ship4;
                    break;
                case 5:
                    enemyImage = SpaceInvaders.Properties.Resources.ship5;
                    break;
                case 6:
                    enemyImage = SpaceInvaders.Properties.Resources.ship6;
                    break;
                case 7:
                    enemyImage = SpaceInvaders.Properties.Resources.ship7;
                    break;
                case 8:
                    enemyImage = SpaceInvaders.Properties.Resources.ship8;
                    break;
                default:
                    enemyImage = SpaceInvaders.Properties.Resources.ship9;
                    break;
            }
            
            graphics.DrawImage(enemyImage, X, Y, enemyImage.Width, enemyImage.Height);

        }

    }
    
}
