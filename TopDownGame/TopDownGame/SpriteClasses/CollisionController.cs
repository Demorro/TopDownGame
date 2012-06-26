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
    class CollisionController
    {
        public Vector2 returnedPosition;
        public Vector2 returnedVelocity;

        //this function returns true is there is a collision, meaning we check if it is true before doing any collision related computation
        public bool HandleCollision(List<Rectangle> colliders, Rectangle fullCollider, Rectangle topCollider, Rectangle bottomCollider, Rectangle leftCollider, Rectangle rightCollider, Vector2 velocity, Vector2 position)
        {
            bool checkCollision = false;

            returnedPosition = position;
            returnedVelocity = velocity;

            // add velocity
            fullCollider.X = fullCollider.X + (int)velocity.X;
            fullCollider.Y = fullCollider.Y + (int)velocity.Y;
            topCollider.X = topCollider.X + (int)velocity.X;
            topCollider.Y = topCollider.Y + (int)velocity.Y;
            bottomCollider.X = bottomCollider.X + (int)velocity.X;
            bottomCollider.Y = bottomCollider.Y + (int)velocity.Y;
            leftCollider.X = leftCollider.X + (int)velocity.X;
            leftCollider.Y = leftCollider.Y + (int)velocity.Y;
            rightCollider.X = rightCollider.X + (int)velocity.X;
            rightCollider.Y = rightCollider.Y + (int)velocity.Y;


            for (int i = 0; i < colliders.Count; i++)
            {
                if (fullCollider.Intersects(colliders[i])) //only checks each subcollider if the fullcollider is intersecting, saving on computation
                {
                    //then deal with the subcolliders
                    //upwards movement
                    if (topCollider.Intersects(colliders[i]))
                    {
                        returnedPosition.Y = colliders[i].Y + colliders[i].Height;
                        returnedVelocity.Y = 0;
                        checkCollision = true;
                    }
                    //downwards movement
                    if (bottomCollider.Intersects(colliders[i]))
                    {
                        returnedPosition.Y = colliders[i].Y - fullCollider.Height;
                        returnedVelocity.Y = 0;
                        checkCollision = true;
                    }
                    //leftwards movement
                    if (leftCollider.Intersects(colliders[i]))
                    {
                        returnedPosition.X = colliders[i].X + fullCollider.Width;
                        returnedVelocity.X = 0;
                        checkCollision = true;
                    }
                    //rightwards movement
                    if (rightCollider.Intersects(colliders[i]))
                    {
                        returnedPosition.X = colliders[i].X - fullCollider.Width;
                        returnedVelocity.X = 0;
                        checkCollision = true;
                    }

                    //deal with diagonal spazzing
                    //these functions just offset it a bit so that it dosent spaz out, and it alway preferrs a horizontal offset
                    if ((topCollider.Intersects(colliders[i])) && (rightCollider.Intersects(colliders[i])))
                    {
                        returnedPosition.X = returnedPosition.X + (fullCollider.Width - topCollider.Width) / 2;
                    }

                    if ((topCollider.Intersects(colliders[i])) && (leftCollider.Intersects(colliders[i])))
                    {
                        returnedPosition.X = returnedPosition.X - (fullCollider.Width - topCollider.Width) / 2;
                    }

                    if ((bottomCollider.Intersects(colliders[i])) && (rightCollider.Intersects(colliders[i])))
                    {
                        returnedPosition.X = returnedPosition.X + (fullCollider.Width - topCollider.Width) / 2;
                    }

                    if ((bottomCollider.Intersects(colliders[i])) && (leftCollider.Intersects(colliders[i])))
                    {
                        returnedPosition.X = returnedPosition.X - (fullCollider.Width - topCollider.Width) / 2;
                    }
                }
            }
            
            return checkCollision;
        }
    }
}
