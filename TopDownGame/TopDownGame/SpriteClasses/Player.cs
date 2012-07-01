using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace TopDownGame.SpriteClasses
{
    class Player : Sprite
    {
        PlayerController playerController = new PlayerController();
        CollisionController collisionController = new CollisionController();
        AnimationController animationController = new AnimationController();

        static public Rectangle playerSize = new Rectangle(0, 0, 45, 70); //important for the collision

        //Y collider offset
        private int yOffset = 30;   //Y offset for the colliders, because the player if from a weird front facing perspective, it works better if only the bottom part of the player collides

        //SubColliders
        Rectangle topCollider;
        Rectangle bottomCollider;
        Rectangle leftCollider;
        Rectangle rightCollider;
        //debug code
        DebugRectangle topColliderRectangle = new DebugRectangle();
        DebugRectangle bottomColliderRectangle = new DebugRectangle();
        DebugRectangle leftColliderRectangle = new DebugRectangle();
        DebugRectangle rightColliderRectangle = new DebugRectangle();

        public Vector2 velocity = new Vector2(0, 0);

        //safety bounds
        int safetyBounds;

        public Player()
        {
            debugModeActive = true;
        }

        public void Update(List<Rectangle> levelColliders)
        {
            safetyBounds = (int)playerController.hardSpeedCap + 5;

            //movement
            position = playerController.movementController(position,velocity);
            velocity = playerController.velocity;

            //collision
            if(collisionController.HandleCollision(levelColliders,fullCollider,topCollider,bottomCollider,leftCollider,rightCollider,velocity,position) == true)
            {
                position = collisionController.returnedPosition;
                velocity = collisionController.returnedVelocity;
            }
            

            //update Colliders
            //the hardspeedcap is used here so that when detecting against a moving object, you never get it clipping too far in, eg it never clips far enough to detect the top collider while moving right
            fullCollider = UpdateRectangleToObject(fullCollider,0,yOffset,-yOffset,0);
            topCollider = UpdateRectangleToObject(fullCollider, safetyBounds, yOffset, (-fullCollider.Height / 2) - yOffset, -safetyBounds * 2);
            bottomCollider = UpdateRectangleToObject(fullCollider, safetyBounds, (fullCollider.Height/2) + yOffset, (-fullCollider.Height / 2) - yOffset, -safetyBounds * 2);
            leftCollider = UpdateRectangleToObject(fullCollider, 0, safetyBounds + yOffset, (-safetyBounds * 2) -yOffset, -fullCollider.Width / 2);
            rightCollider = UpdateRectangleToObject(fullCollider, fullCollider.Width / 2, safetyBounds + yOffset, (-safetyBounds * 2) -yOffset, -fullCollider.Width / 2);

            //animate
            animationController.animationHandler();

            //Set the current animation frame and sprite effect
            sourceRect = animationController.currentAnimationRectangle;
            spriteEffect = animationController.spriteEffect;
        }

        public new void LoadContent(ContentManager theContentManager, string theAssetName, float x, float y, float Scale, bool generateCollider, GraphicsDevice graphicsDevice)
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
                leftColliderRectangle.LoadContent(graphicsDevice);
                rightColliderRectangle.LoadContent(graphicsDevice);
                topColliderRectangle.LoadContent(graphicsDevice);
                bottomColliderRectangle.LoadContent(graphicsDevice);
            }
        }

        public new void LoadContent(ContentManager theContentManager, string theAssetName, float x, float y, float Scale, bool generateCollider, int spritewidth, int spriteheight, int spritex, int spritey, GraphicsDevice graphicsDevice)
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
                leftColliderRectangle.LoadContent(graphicsDevice);
                rightColliderRectangle.LoadContent(graphicsDevice);
                topColliderRectangle.LoadContent(graphicsDevice);
                bottomColliderRectangle.LoadContent(graphicsDevice);
            }
        }
        
        //Draw the sprite to the screen
        public new void Draw(SpriteBatch theSpriteBatch)
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
                topColliderRectangle.DrawRectangle(topCollider, Color.Red, theSpriteBatch);
                bottomColliderRectangle.DrawRectangle(bottomCollider, Color.Red, theSpriteBatch);
                leftColliderRectangle.DrawRectangle(leftCollider, Color.Blue, theSpriteBatch);
                rightColliderRectangle.DrawRectangle(rightCollider, Color.Blue, theSpriteBatch);
            }
        }
    }
}
