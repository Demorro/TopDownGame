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

        public Vector2 velocity = new Vector2(0, 0);

        public void Update()
        {
            //movement
            position = playerController.movementController(position,velocity);
            velocity = playerController.velocity;

            //update Collider
            fullCollider = UpdateRectangleToObject(fullCollider);
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
            }
        }
    }
}
