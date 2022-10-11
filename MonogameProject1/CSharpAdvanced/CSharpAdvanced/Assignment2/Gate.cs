using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SnowLibrary;
using SnowLibrary.Monogame;
using System;
using System.Linq;

namespace CSharpAdvanced.Assignment2
{
    public class Gate : GameObject
    {
        /// <summary>
        /// Creates a new instance of the shield class with the given parameters
        /// </summary>
        public Gate(string objectName, Vector2 position, params Sprite[] sprites) : base(objectName, new Transform())
        {
            sprites.Foreach(x => this.sprites.Add(x));
            transform.position = position;
        }
        /// <summary>
        /// Creates a new empty instance of the Gate class
        /// </summary>
        public Gate() : base("new Gate", new Transform())
        {
            Enabled = false;
        }

        public override void OnCollisionEnter(GameObject other)
        {
            Environment.Exit(69);
        }
    }
}
