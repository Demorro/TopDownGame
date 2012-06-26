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
    class Level2
    {
        int numberOfVerticalTiles = 0;
        int numberOfHorizontalTiles = 0;
        FloorTile[,] floorArray;

        //collision
        List<WallTile> walls = new List<WallTile>();
        public List<Rectangle> colliders = new List<Rectangle>();

        //walls
        int wallTileNumber = 0; //used for keeping track of what walltile we're on.

        public void LoadContent(ContentManager contentmanager, GraphicsDevice graphicsdevice)
        {
            setFloorTileRectangle(0, 0, graphicsdevice.Viewport.Width, graphicsdevice.Viewport.Height, contentmanager, graphicsdevice); //sets FloorTiles to be all over the background
            setWalls(contentmanager, graphicsdevice);
        }

        public void Draw(SpriteBatch spritebatch)
        {
            //rendering the whole background of FloorTiles
            for (int v = 0; v < numberOfVerticalTiles; v++)
            {
                for (int h = 0; h < numberOfHorizontalTiles; h++)
                {
                    floorArray[h, v].Draw(spritebatch);
                }
            }

            //render walls
            for (int i = 0; i < walls.Count; i++)
            {
                walls[i].Draw(spritebatch);
            }
        }

        private void setFloorTileRectangle(int xStart, int yStart, int width, int height, ContentManager contentmanager, GraphicsDevice graphicsdevice)
        {

            numberOfHorizontalTiles = (int)(width / FloorTile.width) + 1;
            numberOfVerticalTiles = (int)(height / FloorTile.height) + 1;

            floorArray = new FloorTile[numberOfHorizontalTiles, numberOfVerticalTiles];

            for (int v = 0; v < numberOfVerticalTiles; v++)
            {
                for (int h = 0; h < numberOfHorizontalTiles; h++)
                {
                    floorArray[h, v] = new FloorTile();
                    floorArray[h, v].LoadContent(contentmanager, "FloorTile", (xStart + (h * FloorTile.width)), (yStart + (v * FloorTile.height)), 1, false, graphicsdevice);
                }
            }
        }

        private void setWalls(ContentManager contentmanager, GraphicsDevice graphicsdevice)
        {
            drawVerticalWall(0, 0, 7, contentmanager, graphicsdevice); //left wall top
            drawVerticalWall(0, 450, 7, contentmanager, graphicsdevice); //left wall bottom
            drawHorizontalWall(50, 0, 22, contentmanager, graphicsdevice); //top wall
            drawHorizontalWall(50, 750, 10, contentmanager, graphicsdevice); //bottom wall left
            drawHorizontalWall(650, 750, 10, contentmanager, graphicsdevice); //bottom wall right
            drawVerticalWall(1150, 0, 16, contentmanager, graphicsdevice); //right wall
        }

        private void drawHorizontalWall(int startX, int startY, int numberOfBlocks, ContentManager contentmanager, GraphicsDevice graphicsdevice)
        {
            for (int i = 0; i < numberOfBlocks; i++)
            {
                walls.Add(new WallTile());
                walls[wallTileNumber].LoadContent(contentmanager, "WallTile", (i * WallTile.width) + startX, startY, 1, true, graphicsdevice);
                colliders.Add(walls[wallTileNumber].fullCollider);
                wallTileNumber++;
            }
        }

        private void drawVerticalWall(int startX, int startY, int numberOfBlocks, ContentManager contentmanager, GraphicsDevice graphicsdevice)
        {
            for (int i = 0; i < numberOfBlocks; i++)
            {
                walls.Add(new WallTile());
                walls[wallTileNumber].LoadContent(contentmanager, "WallTile", startX, (i * WallTile.height) + startY, 1, true, graphicsdevice);
                colliders.Add(walls[wallTileNumber].fullCollider);
                wallTileNumber++;
            }
        }

        private void drawDiagonalWall(int startX, int startY, int numberOfBlocks, ContentManager contentmanager, GraphicsDevice graphicsdevice)
        {
            for (int i = 0; i < numberOfBlocks; i++)
            {
                walls.Add(new WallTile());
                walls[wallTileNumber].LoadContent(contentmanager, "WallTile", (i * WallTile.width) + startX, (i * WallTile.height) + startY, 1, true, graphicsdevice);
                colliders.Add(walls[wallTileNumber].fullCollider);
                wallTileNumber++;
            }
        }

    }
}
