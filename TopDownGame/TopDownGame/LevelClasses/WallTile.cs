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
    class WallTile : Sprite
    {
        public static int width = 50;
        public static int height = 50;

        public WallTile()
        {
            debugModeActive = false;
        }
    }
}
