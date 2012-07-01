using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using TopDownGame.SpriteClasses;

namespace TopDownGame.LevelClasses
{
    //In order to have levels, these things must be done.
    //
    // 1. levelArray must be made big enough. E.g, for a 2x2 world, the array would be [2,2]. for a 14x3, (14 in x, 3 in y), it would of course be [14,3]
    // 2. The levels must be declared, e.g (Level1 level1 = new Level1();) This must be done for each level screen
    // 3. Another else if statement must be added in the handleLevels function, to update the colliders for when the player goes onto the screen
    // 4. Another loadContent statement must be added to the LoadContent method so that we can load in the content for that particular level
    // 5. The Level must be assigned a number in the levelArray, this is done just below the loadcontents and is in the LoadContent Method. Try to keep it consistant, with 1 being top left, 2 being directly to the right of that, and so on and so forth untill you drop a y level and continue on.
    // 6. Another else if statement must be added to the draw Method, so that the right level is being draw.
    // 7. And thats it! to be honest this should all be obscuficated away, but what the hell this is just a learning prototype
    // ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    class LevelHandler
    {

        //Active Level Counter
        private int activeLevel = 1; //Start on level 1, top left

        ////////////ACTIVE LEVELS/////////////////////////////////
        // 1 = level1, top left, 0,0
        // 2 = level2, top right, 0,1
        // 3 = level3, bottom left, 1,0
        // 4 = level4, bottom right, 1,1
        //////////////////////////////////////////////////////////

        //Levels
        Level1 level1 = new Level1();
        Level2 level2 = new Level2();
        Level3 level3 = new Level3();
        Level4 level4 = new Level4();

        //level array
        private int[,] levelArray = new int[2, 2];
        int currentLevelXCounter = 0;
        int currentLevelYCounter = 0;

        //Screen
        int screenwidth = 0;
        int screenheight = 0;

        //player
        public Vector2 playersPosition; //used for changing the players position after a level change
        public bool playerShouldUpdate = false;

        //the colliders for the current level
        public List<Rectangle> colliders = new List<Rectangle>();

        public void HandleLevels(Vector2 playerPositon, Rectangle playerCollider, Rectangle playerSize)
        {
            playerShouldUpdate = false;

            //set the playerposition in here to what it is
            playersPosition = playerPositon;

            //set the colliders depending on what level we are on
            if (activeLevel == 1)
            {
                colliders = level1.colliders;
            }
            else if (activeLevel == 2)
            {
                colliders = level2.colliders;
            }
            else if (activeLevel == 3)
            {
                colliders = level3.colliders;
            }
            else if (activeLevel == 4)
            {
                colliders = level4.colliders;
            }

            //change the active level depending on the player position.
            //the player position is updated directly from this class in game1.cs
            //moving right
            if (playerCollider.X > screenwidth - playerCollider.Width)
            {
                currentLevelXCounter = currentLevelXCounter + 1;
                playersPosition.X = 0;
                playerShouldUpdate = true;
            }
            //moving left
            if (playerCollider.X < 0)
            {
                currentLevelXCounter = currentLevelXCounter - 1;
                playersPosition.X = screenwidth - playerCollider.Width;
                playerShouldUpdate = true;
            }
            //moving down
            if (playerCollider.Y > screenheight - playerCollider.Height)
            {
                currentLevelYCounter = currentLevelYCounter + 1;
                playersPosition.Y = 0;
                playerShouldUpdate = true;
            }
            //moving up
            if (playerCollider.Y < 0)
            {
                currentLevelYCounter = currentLevelYCounter - 1;
                playersPosition.Y = screenheight - playerSize.Height;
                playerShouldUpdate = true;
            }

            //Console.WriteLine(currentLevelXCounter);
            //Console.WriteLine(currentLevelYCounter);
            Console.WriteLine(playerCollider.Y);
            
            activeLevel = levelArray[currentLevelXCounter, currentLevelYCounter]; //change the activelevel to what it should be.

        }

        public void LoadContent(ContentManager contentManager, GraphicsDevice graphicsDevice)
        {
            level1.LoadContent(contentManager, graphicsDevice);
            level2.LoadContent(contentManager, graphicsDevice);
            level3.LoadContent(contentManager, graphicsDevice);
            level4.LoadContent(contentManager, graphicsDevice);

            screenheight = graphicsDevice.Viewport.Height;
            screenwidth = graphicsDevice.Viewport.Width;

            //set levels
            levelArray[0, 0] = 1;
            levelArray[1, 0] = 2;
            levelArray[0, 1] = 3;
            levelArray[1, 1] = 4;
        }

        public void Draw(SpriteBatch spritebatch)
        {
            //draw the level we are on
            if (activeLevel == 1)
            {
                level1.Draw(spritebatch);
            }
            else if (activeLevel == 2)
            {
                level2.Draw(spritebatch);
            }
            else if (activeLevel == 3)
            {
                level3.Draw(spritebatch);
            }
            else if (activeLevel == 4)
            {
                level4.Draw(spritebatch);
            }

        }

    }
}
