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
    
    internal class BlocEnemy : GameObject
    {

        //static int o; //pour le random
        //static int p;
        static bool gameOver = false;
        bool alive;
        static int n = 8; //nombre de colonnes d'ennemies
        static int m = 5; //nombre de lignes d'ennemies



        Enemy[,] enemies = new Enemy[n,m];
        

        double speed = 100;

        int direction = 1;

        

        public static Random random = new Random();

        /*public int O
        {
            get { return o; }
            
        }
        public int P
        {
            get { return p; }
            
        }*/
        public bool GameOver()
        {
            
            return gameOver; 
        }


        public static int GenererNombreAleatoire(int min, int max)
        {
            return random.Next(min, max);
        }

        public BlocEnemy() : base(0,0)
        {

            int type = 2;
            for (int j = 0; j < m; j++)
            {
                type = GenererNombreAleatoire(1, 8);
                
                for (int i = 0; i < n; i++)
                {

                    enemies[i, j] = new Enemy(X, Y, type);
                    X += 50;
                }
                Y += 30;
                X = 0;
            }
            gameOver = false;
            alive = true;
           
        }


        /*public Enemy Elts(int i, int j)
        {
            return enemies[i, j];
        }*/      

        public IEnumerator<Enemy> GetEnumerator()
        {
            foreach (Enemy enemy in enemies)
            {
                yield return enemy;
            }
        }
        public void descend()
        {
            for (int j = 0; j < m; j++)
            {
                for (int i = 0; i < n; i++)
                {
                    if(enemies[i,j]!=null)enemies[i, j].Y += 30;
                    
                }
            }
            speed += 20;
        }

        public int lastX()
        {
            for (int i = n - 1; i >= 0; i--)
            {
                for (int j = 0; j < m; j++)
                {
                    if (enemies[i, j] != null && enemies[i, j].IsAlive()) return i;
                }
            }
            alive = false;
            return 0;
        }
        public int lastY()
        {
            for (int j = m - 1; j >= 0; j--)
            {
                for (int i = 0; i < n; i++)
                {
                    if (enemies[i, j] != null && enemies[i, j].IsAlive()) return j;
                }
            }
            alive = false;
            return 0;
        }
        public int premierX()
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    if (enemies[i,j]!=null && enemies[i, j].IsAlive()) return i;
                }
            }
            alive = false;
            return 0;
        }
        public int premierY()
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    if (enemies[i,j]!=null && enemies[i, j].IsAlive()) return j;
                }
            }
            alive = false;
            return 0;
        }
        
        public override void Update(Game gameInstance, double deltaT)
        {
            //removewhere?
            Random rand = new Random();
            foreach (Enemy enmy in enemies)
            {
                
                Double r = rand.NextDouble();
                if (enmy != null)
                {
                    
                    if (r < deltaT * 0.05 * speed * 0.01)
                    {
                        enmy.Shoot(gameInstance);                       

                    }
                    if (enmy.Y >= 500)
                    {
                        gameOver = true;
                        GameOver();
                    }
                }
                
            }

            //deplacements
            if (enemies[premierX(),premierY()]!=null && enemies[premierX(), premierY()].X <= 0)
            {

                direction = 1;
                descend();
            }
            if (enemies[lastX(),premierY()]!=null && enemies[lastX(), premierY()].X >= gameInstance.gameSize.Width - 50)
            {
                direction = -1;
                descend();
            }

            for (int j = 0; j < m; j++)
            {
                for (int i = 0; i < n; i++)
                {
                    if(enemies[i,j]!=null)enemies[i, j].X +=(direction)* (float)(speed * deltaT) * (float)0.50;

                    if (enemies[i, j] == null || !enemies[i, j].IsAlive())
                    {
                        //gameInstance.Remove(enemies[i,j]);
                        if (enemies[i, j] != null)
                        {
                            enemies[i, j].Erase();
                            enemies[i, j] = null;
                        }
                        //gameObjects.RemoveWhere(gameObject => !gameObject.IsAlive());
                    }
                    
                }
            }
                                                
        }

        public override void Draw(Game gameInstance, Graphics graphics)
        {
            for(int j = 0; j < m; j++)
            {
                for (int i = 0; i < n; i++)
                {
                    if (enemies[i,j]!=null) enemies[i,j].Draw(gameInstance, graphics);


                }

            }

        }


        public override bool IsAlive()
        {
            return alive;
        }

        public int N
        {
            get { return n; }
        }
        public int M
        {
            get { return m; }
        }
        public override bool Collision(Missile missile, Bitmap Image)
        {

            /*foreach (Enemy enmy in enemies)
            {
                /*if (enmy != null)
                {
                    bool c = base.Collision(missile, enmy.EnemyImage);
                    if (c)
                    {
                        enmy.Erase();
                        enmy.Vie--;

                    }
                }
                int width = enmy.EnemyImage.Width;
                int height = enmy.EnemyImage.Height;
                if (enmy.X < missile.X && enmy.X + width > missile.X && Y < missile.Y && Y + height > missile.Y) //test des rectangles englobants
                {
                    enmy.Vie--;
                    enmy.Erase();
                    return true;
                }
                

            }*/
              
            //déterminer l'ennemie en fonction de la position du missile

            float mX = missile.X;
            float mY = missile.Y;
            for (int j = m - 1; j >= 0; j--)                
            {
                for (int i = 0; i < n; i++)
                {
                    if (enemies[i, j] != null)
                    {
                        //bool c = base.Collision(missile, enemies[i,j].EnemyImage);
                        int width = enemies[i, j].EnemyImage.Width;
                        int height = enemies[i, j].EnemyImage.Height;

                        //if(c)
                        if (enemies[i, j].X < mX && enemies[i, j].X + width > mX && Y < mY && Y + height > mY) //test des rectangles englobants
                        {
                            //enemies[i, j].Vie--;
                            // enemies[i, j].Erase();
                            enemies[i, j] = null;
                            missile.Alive = false;
                            return true;
                        }
                    }
                }
            }
            return false;
        }

    }
}