using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpaceInvaders
{
    internal class Joueur : Vaisseau
    {
        Bitmap image = SpaceInvaders.Properties.Resources.ship1;
        //int latence = 0;
        Bonus boost = null;

        /// <summary>
        /// Constructeur de joueur
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Joueur(float x, float y) : base(x, y, 3)
        {
        }

        /// <summary>
        /// Get de l'image du joueur
        /// </summary>
        public Bitmap Image { get { return image; } }

        /// <summary>
        /// Dessine le joueur
        /// </summary>
        /// <param name="gameInstance"></param>
        /// <param name="graphics"></param>
        public override void Draw(Game gameInstance, Graphics graphics)
        {
            
            graphics.DrawImage(image, X, Y, image.Width, image.Height);

            Font consolasFont = new Font("Consolas", 18, FontStyle.Regular);
            string mVie = "Nombre de vie : "+ this.Vie;
            graphics.DrawString(mVie, consolasFont, Brushes.Green, 0, 550); 
        }

        /// <summary>
        /// Collision joueur/missile
        /// </summary>
        /// <param name="missile"></param>
        /// <param name="Image"></param>
        /// <returns>Vrai si il y a eu collision, faux sinon</returns>
        public override bool Collision(Missile missile, Bitmap Image)
        {
            return base.Collision(missile, image);
        }

        /// <summary>
        /// Bruitage de tir de missile
        /// </summary>
        /// <param name="ressourceSon"></param>
        private void JouerSon(UnmanagedMemoryStream ressourceSon)
        {

            using (MemoryStream stream = new MemoryStream())
            {
                // Copie les données de UnmanagedMemoryStream vers MemoryStream
                ressourceSon.CopyTo(stream);
                stream.Seek(0, SeekOrigin.Begin); // Assure que le flux est positionné au débit

                // Joue le son avec SoundPlayer
                using (SoundPlayer soundPlayer = new SoundPlayer(stream))
                {
                    soundPlayer.Play();
                }
            }


        }
        
        /// <summary>
        /// Tir du joueur
        /// </summary>
        /// <param name="gameInstance"></param>
        private void shoot(Game gameInstance)
        {
            if (gameInstance.keyPressed.Contains(Keys.Space) && /*latence <= 0 &&*/ tir == null)
            {

                Missile m = new Missile(X + 15, Y - 18, 0);
                gameInstance.AddNewGameObject(m);
                gameInstance.Missiles.Add(m);
                tir = m;
                JouerSon(SpaceInvaders.Properties.Resources.shoot);
                gameInstance.ReleaseKey(Keys.Space);

            }
        }

        /// <summary>
        /// Update de joueur
        /// </summary>
        /// <param name="gameInstance"></param>
        /// <param name="deltaT"></param>
        public override void Update(Game gameInstance, double deltaT)
        {
            if (gameInstance.keyPressed.Contains(Keys.Left))
            {
                if (X > 0)
                {
                    X -= 1;
                }

            }
            if (gameInstance.keyPressed.Contains(Keys.Right))
            {
                if (X < gameInstance.gameSize.Width - image.Width)
                {
                    X += 1;
                }
            }
            if (tir != null && !tir.IsAlive()) tir = null;
            shoot(gameInstance);

        }
    }
}
