using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics; //For Stopwatch and others
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using TopDownGame.SpriteClasses;

//Authors: Elliot Morris, Mark Thompson.

namespace TopDownGame.SpriteClasses
{
    class Sprite : Object
    {
        //Drawing
        public Texture2D spriteTexture;                        //The texture object used when drawing the sprite
        public SpriteEffects spriteEffect = SpriteEffects.None;   //SpriteEffects, for things flipping the sprite and blending and stuff.
        public Rectangle sourceRect;                            //The rectangle that defines what is displayed on the spritesheet(size and position)
        public Vector2 origin = Vector2.Zero;                   //The origin (manipulation center) of the sprite NOT YET IMPLEMENTED

        //Animation
        Stopwatch animationTimer = new Stopwatch();             //The timer for the animations
        public int animationCounter = 0;                        //The counter that stores what frame of the animation were on 

        //Collision
        public Rectangle fullCollider;                          //The collider that is automatically generated

        //Debug Code
        public bool debugModeActive = true;
        public DebugRectangle fullColliderRectangle = new DebugRectangle(); //A debug rectangle that follows the collider
        public Color debugColour = Color.LimeGreen;



        //************************
        // LOADCONTENT OVERLOADS
        //************************
        //Loadcontent overload for sprites that are just 1 image.
        public void LoadContent(ContentManager theContentManager, string theAssetName, float x, float y, float Scale, bool generateCollider, GraphicsDevice graphicsDevice)
        {
            //Drawing
            spriteTexture = theContentManager.Load<Texture2D>(theAssetName);
            this.position.X = x;
            this.position.Y = y;
            this.scale = Scale;
            sourceRect.Width = spriteTexture.Width;
            sourceRect.Height = spriteTexture.Height;
            sourceRect.X = 0;
            sourceRect.Y = 0;

            //Standard Colliders
            if (generateCollider == true)
            {
                fullCollider = new Rectangle((int)position.X, (int)position.Y, sourceRect.Width, sourceRect.Height);
            }

            //Debug Code
            if (debugModeActive == true)
            {
                fullColliderRectangle.LoadContent(graphicsDevice);
            }
        }

        //Full LoadContent Overload with spritesheet animations, as it deals with Sourcerect.
        public void LoadContent(ContentManager theContentManager, string theAssetName, float x, float y, float Scale,bool generateCollider, int spritewidth, int spriteheight, int spritex, int spritey,GraphicsDevice graphicsDevice)
        {
            //Drawing
            spriteTexture = theContentManager.Load<Texture2D>(theAssetName);
            this.position.X = x;
            this.position.Y = y;
            this.scale = Scale;
            sourceRect.Width = spritewidth;
            sourceRect.Height = spriteheight;
            sourceRect.X = spritex;
            sourceRect.Y = spritey;

            //Standard Colliders
            if (generateCollider == true)
            {
                fullCollider = new Rectangle((int)position.X, (int)position.Y, sourceRect.Width, sourceRect.Height);
            }

            //Debug Code
            if (debugModeActive == true)
            {
                fullColliderRectangle.LoadContent(graphicsDevice);
            }
        }
        //************************
        // LOADCONTENT OVERLOADS DONE
        //************************


        //Draw the sprite to the screen
        public void Draw(SpriteBatch theSpriteBatch)
        {
            if (sourceRect != null)
            {
                theSpriteBatch.Draw(spriteTexture, position,
                    sourceRect, Color.White,
                    0.0f, Vector2.Zero, scale, spriteEffect, 0);
            }

            //Debug Code
            if (debugModeActive == true)
            {
                fullColliderRectangle.DrawRectangle(fullCollider, debugColour, theSpriteBatch);
            }
        }
        
        //This is a generic animation function, and will work good for any looping animation, takes in a predefined array of rectangles(the rectangle positions on the spritesheet,) and the desired speed of the animaton
        public void Animate(Rectangle[] sourcerects, int animationspeed)
        {
            if ((animationCounter == 0) || ((animationCounter == sourcerects.Length) && (animationTimer.ElapsedMilliseconds > (205 - (animationspeed*5))))) //If the animation is at the very start or (at the very end and with the time after the final frame elapsed)
            {
                animationCounter = 0; //Set animation counter to 0 in case that this is coming from the end of the loop
                sourceRect = sourcerects[animationCounter]; //Sprite source rectangle set to inputted sourcerect[0]
                animationCounter++;
                animationTimer.Start(); //starts the timer in order to get delays between frame switching
            }

            else if (animationTimer.ElapsedMilliseconds > (205 - (animationspeed * 5)))
            {
                sourceRect = sourcerects[animationCounter]; //set Sourcerect to the next animation fram of the frames passed in.
                animationTimer.Restart(); //restarts the timer so this if statement can trigger again, unless of course it needs to be relooped.
                animationCounter++;   
            }
        }

        //Updates any rectangle to the same specs as the current object, mostly used for keeping colliders following the player. etc.
        public Rectangle UpdateRectangleToObject(Rectangle rectangle)
        {
            rectangle.X = (int)position.X;
            rectangle.Y = (int)position.Y;
            rectangle.Width = sourceRect.Width;
            rectangle.Height = sourceRect.Height;

            return rectangle;
        }

        //Updates any rectangle to the same specs as the current object, mostly used for keeping colliders following the player. etc. Overload with Offsets.
        public Rectangle UpdateRectangleToObject(Rectangle rectangle, int xOffset, int yOffset, int heightOffset, int widthOffset)
        {
            rectangle.X = (int)position.X + xOffset;
            rectangle.Y = (int)position.Y + yOffset;
            rectangle.Height = sourceRect.Height + heightOffset;
            rectangle.Width = sourceRect.Width + widthOffset;

            return rectangle;
        }
    }
}
