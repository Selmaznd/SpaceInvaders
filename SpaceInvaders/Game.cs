using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using System.Reflection;
using System.Media;
using System.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace SpaceInvaders
{
    /// <summary>
    /// This class represents the entire game, it implements the singleton pattern
    /// </summary>
    class Game
    {


        #region GameObjects management
        /// <summary>
        /// Set of all game objects currently in the game 
        /// </summary>
        public HashSet<GameObject> gameObjects = new HashSet<GameObject>();

        /// <summary>
        /// Set of new game objects scheduled for addition to the game
        /// </summary>
        private HashSet<GameObject> pendingNewGameObjects = new HashSet<GameObject>();


        enum Gamestate {Attente,Play,Pause,Perdu,Win };
        Gamestate state ;

        private bool gameOverGame = false;
        private int niveauUnlock = 1;
        BlocEnemies newVague;
        Joueur player;
        List<Missile> missiles = new List<Missile>();
        List<Bonus> boosts = new List<Bonus>();

        /// <summary>
        /// Schedule a new object for addition in the game.
        /// The new object will be added at the beginning of the next update loop
        /// </summary>
        /// <param name="gameObject">object to add</param>
        public void AddNewGameObject(GameObject gameObject)
        {
            pendingNewGameObjects.Add(gameObject);
        }
        #endregion

        #region game technical elements
        /// <summary>
        /// Size of the game area
        /// </summary>
        public Size gameSize;

        /// <summary>
        /// State of the keyboard
        /// </summary>
        public HashSet<Keys> keyPressed = new HashSet<Keys>();

        #endregion

        #region static fields (helpers)

        /// <summary>
        /// Singleton for easy access
        /// </summary>
        public static Game game { get; private set; }

        /// <summary>
        /// A shared black brush
        /// </summary>
        private static Brush greenBrush = new SolidBrush(Color.Green);

        /// <summary>
        /// A shared simple font
        /// </summary>
        private static Font defaultFont = new Font("Times New Roman", 24, FontStyle.Bold, GraphicsUnit.Pixel);
        #endregion


        #region constructors
        /// <summary>
        /// Singleton constructor
        /// </summary>
        /// <param name="gameSize">Size of the game area</param>
        /// 
        /// <returns></returns>
        public static Game CreateGame(Size gameSize)
        {
            if (game == null)
            {
                game = new Game(gameSize);
                game.state = Gamestate.Attente;
            }
            return game;
        }

        /// <summary>
        /// Private constructor
        /// </summary>
        /// <param name="gameSize">Size of the game area</param>
        private Game(Size gameSize)
        {
            this.gameSize = gameSize;
            
        }

        #endregion

        #region methods

        /// <summary>
        /// Force a given key to be ignored in following updates until the user
        /// explicitily retype it or the system autofires it again.
        /// </summary>
        /// <param name="key">key to ignore</param>
        public void ReleaseKey(Keys key)
        {
            keyPressed.Remove(key);
        }

        /// <summary>
        /// Get et set de la liste de missiles
        /// </summary>
        public List<Missile> Missiles
        {
            get { return missiles; }
            set { missiles = value; }
        }

        /// <summary>
        /// Get et set de la liste de bonus
        /// </summary>
        public List<Bonus> Boosts
        {
            get { return boosts; }
            set { boosts = value; }
        }
        /// <summary>
        /// Draw the whole game
        /// </summary>
        /// <param name="g">Graphics to draw in</param>
        public void Draw(Graphics g)
        {
            if(state == Gamestate.Attente)
            {

                Bitmap imageMenu = SpaceInvaders.Properties.Resources.menu;

                
                g.DrawImage(imageMenu, -120, 0, imageMenu.Width - 30, imageMenu.Height - 30);
                string mString = "                                      Niveau : " + this.niveauUnlock;
                g.DrawString(mString, defaultFont, Brushes.Green, 0, 200);
                if (niveauUnlock == 3)
                {
                    string mNiveau = "                                Niveau 3 unlocked \n                Appuyez sur espace pour commencer";
                    g.DrawString(mNiveau, defaultFont, Brushes.Green, 0, gameSize.Height / 2);
                }
                else if (niveauUnlock == 2)
                {
                    string mNiveau = "                                Niveau 2 unlocked \n                Appuyez sur espace pour commencer";
                    g.DrawString(mNiveau, defaultFont, Brushes.Green, 0, gameSize.Height / 2 );
                }
                else if(niveauUnlock == 1)
                {
                    string mNiveau = "                                       Bienvenue \n         Appuyez sur espace pour commencer le niveau 1";
                    g.DrawString(mNiveau, defaultFont, Brushes.Green, 0, gameSize.Height / 2 );
                }
                else
                {
                    string mNiveau = "                               La vitesse augmente ! \n                Appuyez sur espace pour commencer";
                    g.DrawString(mNiveau, defaultFont, Brushes.Green, 0, gameSize.Height / 2);

                }
                // g.DrawString(mGameMenu, consolasFont, Brushes.White, 0, gameSize.Height / 2 + 100);

            }
            if (state == Gamestate.Pause)
            {
                Bitmap imagePause = SpaceInvaders.Properties.Resources.pause;
                string mPause = "                     Le jeu est actuellement en Pause \n                   Appuyer sur P pour revenir au jeu";
                g.DrawString(mPause, defaultFont, greenBrush, 0, gameSize.Height / 2 + 40);
                g.DrawImage(imagePause, 70, 180, imagePause.Width, imagePause.Height);
                
                
            }
            if (state == Gamestate.Perdu)
            {
                Bitmap imageOver = SpaceInvaders.Properties.Resources.gameover;
                string mGameOver = "                          Perdu ! \n                   Appuyer sur espace pour recommencer";
                g.DrawString(mGameOver, defaultFont, greenBrush, 0, gameSize.Height / 2 + 50);
                g.DrawImage(imageOver, 150, 120, imageOver.Width, imageOver.Height);

            }
            if(state == Gamestate.Win)
            {
                Bitmap imageWin = SpaceInvaders.Properties.Resources.win;
                g.DrawImage(imageWin, 60, 120, imageWin.Width, imageWin.Height);

            }
            if(state == Gamestate.Play)
            {
                foreach (GameObject gameObject in gameObjects)
                {
                    gameObject.Draw(this, g);
                }
                              
            }
            else
            {
                foreach (GameObject gameObject in gameObjects)
                    gameObject.Draw(this, g);
            }
            
        }

        /// <summary>
        /// Effectue les actions nécessaires de fin de partie
        /// </summary>
        public void partieFini()
        {
            gameObjects.RemoveWhere(gameObject => true);
            if (keyPressed.Contains(Keys.Space))
            {
                state = Gamestate.Attente;
            }
            ReleaseKey(Keys.Space);
        }
    


        /// <summary>
        /// Verifie si un bonus peut être crée
        /// </summary>
        /// <param name="nV"></param>
        /// <returns>Vrai si les conditions sont réunis pour crée le bonus, non sinon</returns>
        public bool PeutCreeBonus(BlocEnemies nV)
        {
            if (newVague.BonusPossible() != null)
            {
                if (nV.PeutCreeBonus)
                {
                    Random rand = new Random();
                    Double r = rand.NextDouble();
                    if (r < nV.ProbaBonus)
                    {
                        return true;
                    }
                    nV.PeutCreeBonus = false;

                }
            }

            return false;
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="deltaT"></param>
        public void Update(double deltaT)
        {

            gameObjects.UnionWith(pendingNewGameObjects);
            pendingNewGameObjects.Clear();

            //### ATTENTE ###
            if (state == Gamestate.Attente)
            {             
                
                Bunkerr bunker = new Bunkerr(0, 0, 0);
                Bunkerr bunkermilieu = new Bunkerr(gameSize.Width / 2 - (bunker.ImageBunker.Width / 2), 420, 3);
                Bunkerr bunkergauche = new Bunkerr(gameSize.Width / 5 - (bunker.ImageBunker.Width / 2), 420, 3);
                Bunkerr bunkerdroite = new Bunkerr(gameSize.Width * 4 / 5 - (bunker.ImageBunker.Width / 2), 420, 3);
                
                if (niveauUnlock == 1)
                {
                    newVague = new BlocEnemies(1);
                }
                else if (niveauUnlock == 2 )
                {
                    newVague = new BlocEnemies(2);
                }
                else if (niveauUnlock == 3)
                {
                    newVague = new BlocEnemies(3);
                }
                else if (niveauUnlock >= 4)
                {
                    newVague = new BlocEnemies(4);
                }

                player = new Joueur(232, 500);
                
                
                if (keyPressed.Contains(Keys.Space))
                {

                    AddNewGameObject(newVague);
                    AddNewGameObject(player);
                    AddNewGameObject(bunkermilieu);
                    AddNewGameObject(bunkergauche);
                    AddNewGameObject(bunkerdroite);

                    

                    //gamestart = true;
                    state = Gamestate.Play;

                    // release key space (no autofire)
                    ReleaseKey(Keys.Space);


                }
            }

            //### PERDU OU WIN ###
            if (state == Gamestate.Perdu || state== Gamestate.Win)
            {
                partieFini();
            }

            //### PAUSE ###
            if(state == Gamestate.Pause)
            {
                if(keyPressed.Contains(Keys.P)) { 
                    state = Gamestate.Play;
                    ReleaseKey(Keys.P);
                }
            }

            //### PLAY ###
            if (state == Gamestate.Play)
            {
                if (keyPressed.Contains(Keys.P))
                {
                    state = Gamestate.Pause;
                    ReleaseKey(Keys.P);
                }

                if (newVague.GameOver())
                {
                    gameOverGame = true;
                }
                // update each game object
                foreach (GameObject gameObject in gameObjects)
                {
                    gameObject.Update(this, deltaT);

                }

                foreach (GameObject gO in gameObjects)
                {
                    foreach (Missile m in missiles)
                    {
                        if (gO.Collision(m, null))
                        {
                                if (PeutCreeBonus(newVague))
                                {
                                    float bx = newVague.BonusPossible()[1];
                                    float by = newVague.BonusPossible()[2];
                                    Bonus b = new Bonus(bx, by);
                                    boosts.Add(b);
                                    AddNewGameObject(b);

                                }

                            break;
                        }


                    }
                    missiles.RemoveAll(m => !m.IsAlive());
                    boosts.RemoveAll(b => !b.IsAlive());
                }
                foreach(Bonus bonus in boosts)
                {
                    bonus.CollisionJoueur(player);
                }




                //Joueur perd
                if (newVague.GameOver())
                {
                    state = Gamestate.Perdu;

                }
                if (!player.IsAlive())
                {
                    state = Gamestate.Perdu;
                }

                //Joueur gagne
                if (!newVague.IsAlive())
                {
                    niveauUnlock ++;
                    state = Gamestate.Win;
                }

                gameObjects.RemoveWhere(gameObject => !gameObject.IsAlive());



            }
            
            #endregion
        }
        
    }
}
