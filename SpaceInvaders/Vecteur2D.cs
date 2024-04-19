using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders
{
    internal class Vecteur2D
    {
        private double x, y;

        /// <summary>
        /// Constructeur de Vecteurs
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Vecteur2D(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        /// <summary>
        /// Constructeur par défaut de vecteur
        /// </summary>
        public Vecteur2D() : this(0,0) { }
        public double Norme
        {
            get { return Math.Sqrt(x * x + y * y); }
           
        }

        /// <summary>
        /// Modifie l'opération + pour qu'elle fasse la somme de deux vecteurs
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns>Somme de deux vecteurs</returns>
        public static Vecteur2D operator+(Vecteur2D v1, Vecteur2D v2)
        {
            return new Vecteur2D(v1.x + v2.x, v1.y + v2.y);
        }

        /// <summary>
        /// Redéfinie l'opérateur - pour qu'il effectue la soustraction de deux vecteurs
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns>Soustraction des vecteurs v1 et v2</returns>
        public static Vecteur2D operator -(Vecteur2D v1, Vecteur2D v2)
        {
            return new Vecteur2D(v1.x - v2.x, v1.y - v2.y);
        }

        /// <summary>
        /// Effectue le produit du vecteur par -1
        /// </summary>
        /// <param name="v1"></param>
        /// <returns>L'inverse d'un vecteur</returns>
        public static Vecteur2D operator -(Vecteur2D v1)
        {
            return new Vecteur2D(v1.x * (-1), v1.y * (-1));
        }

        /// <summary>
        /// Effectue le produti d'un vecteur avec un double
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="k"></param>
        /// <returns>Le produit d'un vecteur avec un double</returns>
        public static Vecteur2D operator *(Vecteur2D v1, double k)
        {
            return new Vecteur2D(v1.x * k, v1.y - k);
        }

        /// <summary>
        /// Effectue le produit d'un double avec un vecteur
        /// </summary>
        /// <param name="k"></param>
        /// <param name="v1"></param>
        /// <returns>Le produit d'un double avec un vecteur</returns>
        public static Vecteur2D operator *(double k,Vecteur2D v1)
        {
            return new Vecteur2D(v1.x * k, v1.y - k);
        }

        /// <summary>
        /// Effectue la division d'un vecteur par un double
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="k"></param>
        /// <returns>La division d'un vecteur par un double</returns>
        public static Vecteur2D operator /(Vecteur2D v1, double k)
        {
            return new Vecteur2D(v1.x / k, v1.y / k);
        }

    }
}
