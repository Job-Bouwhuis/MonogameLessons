using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SnowLibrary;
using SnowLibrary.Monogame;
using System;
using System.Linq;

namespace CSharpAdvanced.Assignment1
{
    public class Gate : GameObject, IMyCollidable
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
        /// <summary>
        /// Creates a new empty instance of the Gate class
        /// </summary>
        public Gate() : base("new Gate", new Transform())
        {
            enabled = false;
        }

        public void CheckColision(params GameObject[] others)
        {
            //call the OnColitionEnter method for each object in the list where our hitbox is colliding with the other object's hitbox
            others.Where(x => Collision.IsColliding(hitbox, x.hitbox)).Foreach(x => OnColisionEnter(x));
        }
        public void OnColisionEnter(GameObject other)
        {
            //if the colliding object is a player, then close the game
            if (other is Player)
                Environment.Exit(-1);
        }
        public override void Draw(SpriteBatch batch)
        {
            //draw hitbox for debug
            //batch.Begin();
            //batch.Draw(MonoUtils.DefaultWhiteTexture, hitbox, Color.Red);
            //batch.End();
            
            base.Draw(batch);
        }
    }
}
