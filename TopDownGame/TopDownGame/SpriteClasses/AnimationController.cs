using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics; //For Stopwatch and others
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace TopDownGame.SpriteClasses
{
    class AnimationController
    {
        //Animation
        Stopwatch animationTimer = new Stopwatch();             //The timer for the animations
        public int animationCounter = 0;                        //The counter that stores what frame of the animation were on 
        public int animationSpeed = PlayerController.animationSpeed;
        
        //keyboard state for determining which animation to play
        protected KeyboardState keyboardState;

        //The sourceRect
        public Rectangle currentAnimationRectangle;             //The rectangle that stores the current frame, assign this to the player in player.cs

        //the sprite effect for flipping(and any other effects)
        public SpriteEffects spriteEffect = SpriteEffects.None;   //SpriteEffects, for things flipping the sprite and blending and stuff.

        //The animations
        private Rectangle defaultRect = new Rectangle(479, 356, 45, 66);
        private Rectangle defaultSideRect = new Rectangle(20, 291, 51, 93);
        private Rectangle[] runDown = new Rectangle[13];
        private Rectangle[] runUp = new Rectangle[13];
        private Rectangle[] runLeft = new Rectangle[11];

        public AnimationController()
        {

            //set Animations
            currentAnimationRectangle = defaultRect;

            //Downwards running
            runDown[0] = new Rectangle(479, 356, 45, 70); 
            runDown[1] = new Rectangle(415, 356, 45, 70);
            runDown[2] = new Rectangle(348, 356, 45, 70);
            runDown[3] = new Rectangle(283, 356, 45, 70);
            runDown[4] = new Rectangle(348, 356, 45, 70);
            runDown[5] = new Rectangle(415, 356, 45, 70);
            runDown[6] = new Rectangle(479, 356, 45, 70);
            runDown[7] = new Rectangle(544, 356, 45, 70);
            runDown[8] = new Rectangle(611, 356, 45, 70);
            runDown[9] = new Rectangle(675, 356, 45, 70);
            runDown[10] = new Rectangle(611, 356, 45, 70);
            runDown[11] = new Rectangle(544, 356, 45, 70);
            runDown[12] = new Rectangle(479, 356, 45, 70);

            //Upwards running 994
            runUp[0] = new Rectangle(1473, 356, 45, 70);
            runUp[1] = new Rectangle(1409, 356, 45, 70);
            runUp[2] = new Rectangle(1342, 356, 45, 70);
            runUp[3] = new Rectangle(1277, 356, 45, 70);
            runUp[4] = new Rectangle(1342, 356, 45, 70);
            runUp[5] = new Rectangle(1409, 356, 45, 70);
            runUp[6] = new Rectangle(1473, 356, 45, 70);
            runUp[7] = new Rectangle(1538, 356, 45, 70);
            runUp[8] = new Rectangle(1605, 356, 45, 70);
            runUp[9] = new Rectangle(1669, 356, 45, 70);
            runUp[10] = new Rectangle(1605, 356, 45, 70);
            runUp[11] = new Rectangle(1538, 356, 45, 70);
            runUp[12] = new Rectangle(1473, 356, 45, 70);

            //Sidewards running
            runLeft[0] = new Rectangle(2398, 354, 47, 70);
            runLeft[1] = new Rectangle(2465, 354, 47, 70);
            runLeft[2] = new Rectangle(2534, 354, 47, 70);
            runLeft[3] = new Rectangle(2596, 354, 47, 70);
            runLeft[4] = new Rectangle(2655, 354, 47, 70);
            runLeft[5] = new Rectangle(2398, 354, 47, 70);
            runLeft[6] = new Rectangle(2337, 354, 47, 70);
            runLeft[7] = new Rectangle(2267, 354, 47, 70);
            runLeft[8] = new Rectangle(2202, 354, 47, 70);
            runLeft[9] = new Rectangle(2143, 354, 47, 70);
            runLeft[10] = new Rectangle(2398, 354, 47, 70);


        }

        public void animationHandler()
        {
            
            if (keyboardState.IsKeyDown(Keys.W))
            {
                spriteEffect = SpriteEffects.None;
                Animate(runUp, animationSpeed);
            }
            else if (keyboardState.IsKeyDown(Keys.S))
            {
                spriteEffect = SpriteEffects.None;
                Animate(runDown, animationSpeed);
            }   
            else if (keyboardState.IsKeyDown(Keys.D))
            {
                spriteEffect = SpriteEffects.None;
                Animate(runLeft, animationSpeed);
            }        
            else if (keyboardState.IsKeyDown(Keys.A))
            {
                spriteEffect = SpriteEffects.FlipHorizontally;
                Animate(runLeft, animationSpeed);
            }


            keyboardState = Keyboard.GetState(); //gets the keyboard state for input
        }


        private void Animate(Rectangle[] sourcerects, int animationspeed)
        {
            if ((animationCounter == 0) || ((animationCounter == sourcerects.Length) && (animationTimer.ElapsedMilliseconds > (205 - (animationspeed * 5))))) //If the animation is at the very start or (at the very end and with the time after the final frame elapsed)
            {
                animationCounter = 0; //Set animation counter to 0 in case that this is coming from the end of the loop
                currentAnimationRectangle = sourcerects[animationCounter]; //Sprite source rectangle set to inputted sourcerect[0]
                animationCounter++;
                animationTimer.Start(); //starts the timer in order to get delays between frame switching
            }

            if (animationCounter > sourcerects.Length) //Array overflow failsafe
            {
                animationCounter = 0;
            }

            else if (animationTimer.ElapsedMilliseconds > (205 - (animationspeed * 5)))
            {
                currentAnimationRectangle = sourcerects[animationCounter]; //set Sourcerect to the next animation frame of the frames passed in.
                animationTimer.Restart(); //restarts the timer so this if statement can trigger again, unless of course it needs to be relooped.
                animationCounter++;
            }



        }


    }
}
