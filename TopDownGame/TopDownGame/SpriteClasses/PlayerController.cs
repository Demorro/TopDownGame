using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Diagnostics;

namespace TopDownGame.SpriteClasses
{
    class PlayerController
    {
        public float hardSpeedCap = 6;
        float derivedSpeedCap; //the acceleration used, cant just use "acceleration" cause it needs to be changed if you are going diagonal.
        public float acceleration = 1.5f;
        float derivedAcceleration; //the acceleration used, cant just use "acceleration" cause it needs to be changed if you are going diagonal.
        public float slowDownFactor = 0.75f; //should be from 0 - 1, the lower the factor, the quicker you stop (somewhere around 0.6-0.95 is usually the best)
        public Vector2 velocity;


        protected KeyboardState keyboardState;

        public Vector2 movementController(Vector2 currentPosition, Vector2 currentVelocity)
        {
            Vector2 position = currentPosition; //this is the vector 2 that will be returned from this method, so that it can be set to the actual player positions
            velocity = currentVelocity; //this is the velocity, adjusted during this method.
            derivedAcceleration = acceleration;
            derivedSpeedCap = hardSpeedCap;
            float diagonalSpeedCap = (float)(hardSpeedCap * Math.Sin(45 * Math.PI / 180)); //radians is the reason for the weird conversion.
            float diagonalMovement = (float)(acceleration * Math.Sin(45 * Math.PI / 180)); //radians is the reason for the weird conversion.

            //Accomodate for diagonal movement
            if ((keyboardState.IsKeyDown(Keys.W)) && ((keyboardState.IsKeyDown(Keys.A)) || (keyboardState.IsKeyDown(Keys.D)))) //if the player is going diagonal
            {
                derivedAcceleration = diagonalMovement;
                derivedSpeedCap = diagonalSpeedCap;
            }

            if ((keyboardState.IsKeyDown(Keys.S)) && ((keyboardState.IsKeyDown(Keys.A)) || (keyboardState.IsKeyDown(Keys.D)))) //if the player is going diagonal
            {
                derivedAcceleration = diagonalMovement;
                derivedSpeedCap = diagonalSpeedCap;
            }

            //If we are going too fast, stop that
            if (velocity.X > derivedSpeedCap)
            {
                velocity.X = derivedSpeedCap;
            }

            if (velocity.X < -derivedSpeedCap)
            {
                velocity.X = -derivedSpeedCap;
            }

            if (velocity.Y > derivedSpeedCap)
            {
                velocity.Y = derivedSpeedCap;
            }

            if (velocity.Y < -derivedSpeedCap)
            {
                velocity.Y = -derivedSpeedCap;
            }

            //just general tightening of the movement
            if ((keyboardState.IsKeyDown(Keys.W)) && (keyboardState.IsKeyDown(Keys.S)))
            {
                velocity.Y = 0;
            }

            if ((keyboardState.IsKeyDown(Keys.A)) && (keyboardState.IsKeyDown(Keys.D)))
            {
                velocity.X = 0;
            }


            if (keyboardState.IsKeyDown(Keys.W)) //W key pushed down, moving up
            {

                if (velocity.Y > -derivedSpeedCap) //speed limiter
                {
                    velocity.Y = velocity.Y - derivedAcceleration;
                }
            }

            if (keyboardState.IsKeyDown(Keys.S)) //S key pushed down, moving down
            {

                if (velocity.Y < derivedSpeedCap)//speed limiter
                {
                    velocity.Y = velocity.Y + derivedAcceleration;
                }
            }

            if (keyboardState.IsKeyDown(Keys.A))//A key pushed left, moving up
            {

                if (velocity.X > -derivedSpeedCap)//speed limiter
                {
                    velocity.X = velocity.X - derivedAcceleration;
                }
            }

            if (keyboardState.IsKeyDown(Keys.D))//D key pushed down, moving right
            {

                if (velocity.X < derivedSpeedCap) //speed limiter
                {
                    velocity.X = velocity.X + derivedAcceleration;
                }
            }
            
            //Debug Code
            Console.Write("Velocity.X : ");
            Console.Write(velocity.X);
            Console.WriteLine();

            Console.Write("Velocity.Y : ");
            Console.Write(velocity.Y);
            Console.WriteLine();

            Console.Write("Position : ");
            Console.Write(position);
            Console.WriteLine();
            

            keyboardState = Keyboard.GetState(); //gets the keyboard state for input
            applyFriction();

            position = position + velocity;
            return position;
        }

        private void applyFriction()
        {

            //if friction is too high this tell you
            if ((slowDownFactor > 1) || (slowDownFactor < 0))
            {
                Console.WriteLine("Friction is too high, must be lower than 1 and not negative");
            }

            //round the decceleration off

            if((velocity.X < 0.01) && (velocity.X > 0))
            {
                velocity.X = 0;
            }

            if ((velocity.X > -0.01) && (velocity.X < 0))
            {
                velocity.X = 0;
            }

            if ((velocity.Y < 0.01) && (velocity.Y > 0))
            {
                velocity.Y = 0;
            }

            if ((velocity.Y > -0.01) && (velocity.Y < 0))
            {
                velocity.Y = 0;
            }

            //decceleration from Upwards
            if (keyboardState.IsKeyUp(Keys.W))
            {
                if (velocity.Y < 0)
                {
                    velocity.Y = velocity.Y * slowDownFactor;
                }
            }

            //decceleration from Downwards
            if (keyboardState.IsKeyUp(Keys.S))
            {
                if (velocity.Y > 0)
                {
                    velocity.Y = velocity.Y * slowDownFactor;
                }
            }

            //decceleration from Leftwards
            if (keyboardState.IsKeyUp(Keys.A))
            {
                if (velocity.X < 0)
                {
                    velocity.X = velocity.X * slowDownFactor;
                }
            }

            //decceleration from Rightwards
            if (keyboardState.IsKeyUp(Keys.D))
            {
                if (velocity.X > 0)
                {
                    velocity.X = velocity.X * slowDownFactor;
                }
            }

        }
    }
}
