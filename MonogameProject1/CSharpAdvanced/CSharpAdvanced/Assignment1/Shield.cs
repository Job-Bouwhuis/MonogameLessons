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
    internal class Shield : GameObject
    {
        /// <summary>
        /// Creates a new instance of the shield class with the given parameters
        /// </summary>
        public Shield(string objectName, Vector2 position, params Sprite[] sprites) : base(objectName, new Transform())
        {
            sprites.Foreach(x => textures.Add(x));
            transform.position = position;
        }
        public Shield() : base("new Shield", new Transform())
        {
            enabled = false;
        }
        public override void OnCollisionEnter(GameObject other)
        {
            if (other is Player p)
            {
                if (p.textureIndex == 0)
                    p.textureIndex = 2;
                else if (p.textureIndex == 1)
                    p.textureIndex = 3;

                enabled = false;
            }
        }
    }
}
