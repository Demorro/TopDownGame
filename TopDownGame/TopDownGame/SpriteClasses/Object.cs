﻿/*
//
// Authors: Elliot Morris, Mark Thompson.
//
// Details: Base class for all game objects.
//
*/

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
    public class Object
    {
        //************************
        // PROPERTIES
        //************************
        public Vector2 position = Vector2.Zero; //The position of the Object
        public float rotation = 0f;             //The rotation of the Object 
        public float scale = 1f;                //The scale of the Object
        public bool visible = true;             //If the object is visible or not
    }
}