using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using TopDownGame.SpriteClasses;

namespace TopDownGame.SpriteClasses
{
    class DebugRectangle
    {
        PrimitiveLine debugrectangle;

        Vector2 topleftcorner = new Vector2();
        Vector2 toprightcorner = new Vector2();
        Vector2 bottomrightcorner = new Vector2();
        Vector2 bottomleftcorner = new Vector2();
        Vector2 topleftcorner2 = new Vector2();

        public void LoadContent(GraphicsDevice graphicsDevice)
        {
            debugrectangle = new PrimitiveLine(graphicsDevice);

        }

        public void DrawRectangle(Rectangle rectangle, Color colour, SpriteBatch spritebatch)
        {
            //Get the corners of the rectangle in vector2 format (ACTUALLY NEEDS 5 CORNERS, THE TOP ONE ONCE AT THE BEGGINING AND ONCE AT THE END, IN ORDER TO CLOSE THE RECTANGLE
            topleftcorner.X = rectangle.X;
            topleftcorner.Y = rectangle.Y;

            toprightcorner.X = rectangle.Right;
            toprightcorner.Y = rectangle.Y;

            bottomrightcorner.X = rectangle.Right;
            bottomrightcorner.Y = rectangle.Bottom;

            bottomleftcorner.X = rectangle.X;
            bottomleftcorner.Y = rectangle.Bottom;

            topleftcorner2.X = rectangle.X;
            topleftcorner2.Y = rectangle.Y;

            //Adds the vectors to debugrectangle
            debugrectangle.AddVector(topleftcorner);
            debugrectangle.AddVector(toprightcorner);
            debugrectangle.AddVector(bottomrightcorner);
            debugrectangle.AddVector(bottomleftcorner);
            debugrectangle.AddVector(topleftcorner2);

            //Changes the colour
            debugrectangle.ChangeColour(colour);

            //Draws the rectangle
            debugrectangle.Render(spritebatch);

            //After the rectangle has been drawn, removes the vectors so it dosent get the weird "smearing" effect
            debugrectangle.RemoveVector(topleftcorner);
            debugrectangle.RemoveVector(toprightcorner);
            debugrectangle.RemoveVector(bottomrightcorner);
            debugrectangle.RemoveVector(bottomleftcorner);
            debugrectangle.RemoveVector(topleftcorner2);
        }

    }
}
