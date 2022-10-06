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
    public class Sword : GameObject, IMyCollidable
    {
        /// <summary>
        /// Creates a new instance of the Sword class with the given paramteres
        /// </summary>
        public Sword(string objectName, params Sprite[] sprites) : base(objectName, new Transform())
        {
            sprites.Foreach(x => textures.Add(x));
        }
        /// <summary>
        /// Creates a new instance of the Sword class with the given paramteres
        /// </summary>
        public Sword(string objectName, Transform transform, params Sprite[] sprites) : base(objectName, transform)
        {
            sprites.Foreach(x => textures.Add(x));
        }
        /// <summary>
        /// Creates a new instance of the Sword class with the given paramteres
        /// </summary>
        public Sword(string objectName, Vector2 position, params Sprite[] sprites) : base(objectName, new Transform())
        {
            sprites.Foreach(x => textures.Add(x));
            transform.position = position;
        }
        /// <summary>
        /// Creates a new empty instance of the sword class
        /// </summary>
        public Sword() : base("new Sword", new Transform())
        {
            enabled = false;
        }

        public void CheckColision(params GameObject[] others)
        {
            others.Where(x => Collision.IsColliding(hitbox, x.hitbox)).Foreach(x => OnColisionEnter(x));
        }
        public void OnColisionEnter(GameObject other)
        {
            if(other is Player p)
            {
                if (p.textureIndex == 0)
                    p.textureIndex = 1;
                else if (p.textureIndex == 2)
                    p.textureIndex = 3;
                
                enabled = false;
            }
        }

        public override void Draw(SpriteBatch batch)
        {
            //draw hitbox for debug
            //batch.Begin();
            //batch.Draw(MonoUtils.DefaultWhiteTexture, hitbox, Color.Red);
            //batch.End();
            
            //draw texture
            base.Draw(batch);
        }
    }
}
