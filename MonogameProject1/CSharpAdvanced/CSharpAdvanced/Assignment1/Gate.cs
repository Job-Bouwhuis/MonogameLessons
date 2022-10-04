using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SnowLibrary;
using SnowLibrary.Monogame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharpAdvanced.Assignment1
{
    public class Gate : GameObject, IMyInteractable
    {
        /// <summary>
        /// Creates a new instance of the shield class with the given parameters
        /// </summary>
        public Gate(string objectName, params Sprite[] sprites) : base(objectName, new Transform())
        {
            sprites.Foreach(x => textures.Add(x));
        }
        /// <summary>
        /// Creates a new instance of the shield class with the given parameters
        /// </summary>
        public Gate(string objectName, Transform transform, params Sprite[] sprites) : base(objectName, transform)
        {
            sprites.Foreach(x => textures.Add(x));
        }
        /// <summary>
        /// Creates a new instance of the shield class with the given parameters
        /// </summary>
        public Gate(string objectName, Vector2 position, params Sprite[] sprites) : base(objectName, new Transform())
        {
            sprites.Foreach(x => textures.Add(x));
            transform.position = position;
        }
        public Gate() : base("new Gate", new Transform()) { }

        public void CheckColision(params GameObject[] others)
        {
            others.Where(x => Collision.IsCollidingOnSides(hitbox, x.hitbox)).Foreach(x => OnColisionEnter(x));
        }

        public void OnColisionEnter(GameObject other)
        {
            if (other is Player)
                Environment.Exit(-1);
        }
    }
}
