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
    internal class BlocEnemies : GameObject
    {
        public HashSet<Enemy> enemies = new HashSet<Enemy>();
        static bool gameOver = false; 
        double speed = 60;
        int direction = 1;
        public static Random random = new Random();
        double probaBonus;
        bool peutCreeBonus;


        static int a =4; //nb de lignes d'un monstre d'un certain type
        static int b =2; //nb de lignes d'un monstre d'un autre type

        /// <summary>
        /// Constructeur bloc enemies
        /// </summary>
        /// <param name="niveau"></param>
        public BlocEnemies(int niveau) : base(0, 0, 1)
        {
            
            probaBonus = 0.25;
            int type = 1;
            a = 5;
            b = 3;

            if (niveau == 1)
            {
                a = 4;
                b = 2;
            }
            
            if (niveau == 3)
            {                
                speed = 90;
            }
            else if (niveau >= 4) 
            {
                probaBonus += niveau * 0.015;
                speed += niveau * 15;
            }
            type = GenererNombreAleatoire(1, 8);
            for (int j = 0; j < a; j++)
            {
                if (j < b)
                {
                    for (int i = 0; i < 8; i++)
                    {
                        enemies.Add(new Enemy(X, Y - 180, type));
                        X += 50;
                    }
                    Y += 30;
                    X = 0;
                }
                else
                {
                    type = GenererNombreAleatoire(1, 8);
                    for (int i = 0; i < 8; i++)
                    {
                        enemies.Add(new Enemy(X, Y - 180, type));
                        X += 50;
                    }
                    Y += 30;
                    X = 0;
                }


            }
            gameOver = false;
        }

        /// <summary>
        /// Get and set de PeutCreeBonus
        /// </summary>
        public bool PeutCreeBonus
        {
            get { return peutCreeBonus; }
            set { peutCreeBonus = value; }
        }
        /// <summary>
        /// Retourne un nombre aléatoire entre min et max
        /// <param name="max"></param>
        /// <returns>Retourne le nombre aléatoire généré</returns>
        public static int GenererNombreAleatoire(int min, int max)
        {
            return random.Next(min, max);
        }

        /// <summary>
        /// Retourne vrai si le joueur a perdu, sinon retourne faux
        /// </summary>
        /// <returns>retourne la valeur de vérité de gameOver</returns>
        public bool GameOver()
        {

            return gameOver;
        }
        
        /// <summary>
        /// Réalise les update de Blocenemies
        /// </summary>
        /// <param name="gameInstance"></param>
        /// <param name="deltaT"></param>
        public override void Update(Game gameInstance, double deltaT)
        {
            Random rand = new Random();
            foreach (Enemy enmy in enemies)
            {

                Double r = rand.NextDouble();
                if (!enmy.IsAlive())
                {
                    enemies.Remove(enmy);
                    break;
                }
                if (r < deltaT * 0.05 * speed * 0.1)
                {
                    enmy.Shoot(gameInstance);
                }

                if (enmy.Y >= 500)
                {
                    gameOver = true;
                    GameOver();
                }
                if (enmy.X <= 0)
                {
                    direction = 1;
                    descend();
                }
                if (enmy.X >= gameInstance.gameSize.Width-50)
                {
                    direction = -1;
                    descend();
                }
                enmy.X += (direction) * (float)(speed * deltaT) * (float)0.50;
                
            }
        }
        /// <summary>
        /// Les ennemies descendent
        /// </summary>
        public void descend()
        {
            foreach(Enemy enemy in enemies)
            {
                enemy.Y += 10;
            }
            speed += 5;
        }

        /// <summary>
        /// Dessines les ennemies
        /// </summary>
        /// <param name="gameInstance"></param>
        /// <param name="graphics"></param>
        public override void Draw(Game gameInstance, Graphics graphics)
        {
            foreach(Enemy enemy in enemies)
            {
                enemy.Draw(gameInstance, graphics);
            }
        }

        /// <summary>
        /// Retourne vrai si il reste au moins un ennemies dans le bloc, faux si les ennemies sont tous morts
        /// </summary>
        /// <returns>Retourne la valeur de vérité de si l'ennemie est vivant</returns>
        public override bool IsAlive()
        {
            if (enemies.Count == 0) return false;
            return true;
        }
        
        /// <summary>
        /// Get et Set de ProbaBonus
        /// </summary>
        public Double ProbaBonus
        {
            get { return probaBonus; }
            set { probaBonus = value; }
        }

        /// <summary>
        /// Vérifie si un bonus peut être crée en appelant la méthode BonusPossible() de Enemy pour chaque ennemie.
        /// Si le bonus peut être crée, la première case du tableau vaut 1, les deux autres cases sont les positions respectives X et
        /// Y de où le bonus doit être crée
        /// Si le bonus ne peut pas être crée, la première case du tableau vaut 0.
        /// </summary>
        /// <returns>Tableau de positions du boost si il peut être crée ou null si le bonus ne peut pas être crée</returns>
        public float[] BonusPossible()
        {
            foreach (Enemy enemy in enemies)
            {
                if (enemy.boostPossible()[0] == 1)
                {
                    return enemy.boostPossible();
                }

            }
            return null;
        }

        /// <summary>
        /// Vérifie les collisions entre un missile et un ennemie. Si il y a collision, détruit le missile et décrémente la vie de 
        /// l'ennemie
        /// </summary>
        /// <param name="missile"></param>
        /// <param name="Image"></param>
        /// <param name="gameInstance"></param>
        /// <returns>retourne vrai si il y a eu une collision, faux si il n'y en a pas eu</returns>
        public override bool Collision(Missile missile, Bitmap Image)
        {
            if (missile.Type == 1) return false;

            foreach (Enemy enemy in enemies)
            {
                if (enemy.Collision(missile, Image))
                {
                    peutCreeBonus = true;
                    return true;
                }
            }
            return false;
        }
    }
}