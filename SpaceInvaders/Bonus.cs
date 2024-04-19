using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Reflection;

namespace SpaceInvaders
{
    internal class Bonus : GameObject
    {
        Bitmap image = SpaceInvaders.Properties.Resources.bonus;

        /// <summary>
        /// Constructeur de Bonus
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Bonus(float x, float y) : base(x, y, 1) { }

        /// <summary>
        /// Update de Bonus, update les bonus au cours du jeu
        /// </summary>
        /// <param name="gameInstance"></param>
        /// <param name="deltaT"></param>
        public override void Update(Game gameInstance, double deltaT)
        {
            Y += 1;
            if (Y > gameInstance.gameSize.Height)
            {
                Vie=0;
            }
        }

        /// <summary>
        /// Dessine les bonus
        /// </summary>
        /// <param name="gameInstance"></param>
        /// <param name="graphics"></param>
        public override void Draw(Game gameInstance, Graphics graphics)
        {
            graphics.DrawImage(image, X, Y, image.Width, image.Height);
        }


        /// <summary>
        /// Vérifie la collision entre le joueur et le bonus. Si il y a collision, le joueur récupère une vie et le bonus "meurt"
        /// </summary>
        /// <param name="player"></param>
        public void CollisionJoueur(Joueur player)
        {
            if (player.X -20 < X && X < (player.X + player.Image.Width+20) && Y > player.Y && Y < (player.Y + player.Image.Height))
            {
                player.Vie++;
                Vie=0;
            }
        }

        /// <summary>
        /// Collision entre bonus et missile. Si il y a collision, supprime le missile et le bonus
        /// </summary>
        /// <param name="missile"></param>
        /// <param name="Image"></param>
        /// <param name="gameInstance"></param>
        /// <returns>Retourne vrai si il y a collision, faux sinon</returns>
        public override bool Collision(Missile missile, Bitmap Image)
        {
            return base.Collision(missile, image);
        }

    }
}
