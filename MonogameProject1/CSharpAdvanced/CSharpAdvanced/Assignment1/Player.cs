using System;
using System.Collections.Generic;
using System.Text;
using SnowLibrary.Monogame;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using SnowLibrary.Monogame.Debugging;
using SnowLibrary;
using SnowLibrary.Serialization;

namespace CSharpAdvanced.Assignment1
{
    public class Player : GameObject
    {
        public Player(string objectName, int walkSpeed, params Sprite[] sprites) : base("new Object", new Transform(), sprites)
        {
            this.objectName = objectName;
            sprites.Foreach(x => textures.Add(x));
        }
        /// <summary>
        /// Creates a new empty instance of a Player
        /// </summary>
        public Player() : base("new Player", new Transform())
        {
            enabled = false;
        }
    }
}
