using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Reflection;
using System.Text;

namespace SpaceInvaders
{
    /// <summary>
    /// This is the generic abstact base class for any entity in the game
    /// </summary>
    abstract class GameObject
    {
        float x;
        float y;
        int vie;

        /// <summary>
        /// Constructeur d'un GameObject
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="vie"></param>
        public GameObject(float x, float y, int vie)
        {
            this.x= x; 
            this.y = y;
            this.vie = vie;

        }

        /// <summary>
        /// Get et Set de la position X du GameObject
        /// </summary>
        public float X
        {
            get { return x; }
            set { x = value; }
        }

        /// <summary>
        /// Get et Set de la position Y du GameObject
        /// </summary>
        public float Y
        {
            get { return y; }
            set { y = value; }
        }

        /// <summary>
        /// Get et Set du nombre de vie d'un GameIbject
        /// </summary>
        public int Vie
        {
            get { return vie; }
            set { vie = value; }
        }

        /// <summary>
        /// Update the state of a game objet
        /// </summary>
        /// <param name="gameInstance">instance of the current game</param>
        /// <param name="deltaT">time ellapsed in seconds since last call to Update</param>
        public abstract void Update(Game gameInstance, double deltaT);

        /// <summary>
        /// Render the game object
        /// </summary>
        /// <param name="gameInstance">instance of the current game</param>
        /// <param name="graphics">graphic object where to perform rendering</param>
        public abstract void Draw(Game gameInstance, Graphics graphics);

        /// <summary>
        /// Determines if object is alive. If false, the object will be removed automatically.
        /// </summary>
        /// <returns>Am I alive ?</returns>
        public virtual bool IsAlive()
        {
            return vie > 0;
        }
       
        /// <summary>
        /// Gère les collision entre un missile et un GameObject
        /// </summary>
        /// <param name="missile"></param>
        /// <param name="Image"></param>
        /// <returns>Vrai si il y a eu collision, faux sinon</returns>
        public virtual bool Collision(Missile missile, Bitmap Image)
        {
            if(Image==null) return false;
            int width = Image.Width;
            int height = Image.Height;

            if (x < missile.X && x + width > missile.X && y < missile.Y && y + height > missile.Y) //test des rectangles englobants
            {
                int widthM = missile.ImageMissile.Width;
                int heightM = missile.ImageMissile.Height;

                Color pixelImage = Image.GetPixel(0, 0);

                Color pixelMissile = missile.ImageMissile.GetPixel(0, 0);                


                for (int i=1; i<widthM; i++)
                {
                    for(int j=1; j<heightM; j++)
                    {                       
                        
                        //Repère missile vers écran:
                        int positionX = (int)missile.X+i;
                        int positionY = (int)missile.Y+j;

                        //Repère écran vers object
                        positionX = positionX - (int)x;
                        positionY = positionY - (int)y;

                        //verif pixels dans l'image
                        if (0 < positionX && Image.Width > positionX && 0 < positionY && positionY< Image.Height)
                        {
                            pixelMissile = missile.ImageMissile.GetPixel(i, j);
                            pixelImage = Image.GetPixel(positionX, positionY);

                            if (pixelMissile.A != 0 && pixelImage.A !=0) //Test des pixels
                            {
                                missile.Vie = 0;
                                

                                for (int epaisseurX = -1; epaisseurX < 2; epaisseurX++)
                                {
                                    for (int epaisseurY = -15; epaisseurY < 15; epaisseurY++)
                                    {
                                        
                                        if (positionY - epaisseurY >= 0 && positionX + epaisseurX < Image.Width 
                                            && positionX + epaisseurX >= 0 && positionY - epaisseurY < Image.Height)
                                        {
                                            Image.SetPixel(positionX + epaisseurX, positionY - epaisseurY, Color.Transparent);
                                        }
                                    }
                                }
                                this.Vie--;
                                return true;
                            }
                        }
                    }
                }
                                
            }
            return false;
        }

    }
}
