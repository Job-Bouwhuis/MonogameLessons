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
        /// <summary>
        /// how fast the player moves
        /// </summary>
        public int walkSpeed;
        public Player(string objectName, int walkSpeed, params Sprite[] sprites) : base("new Object", new Transform(), sprites)
        {
            this.walkSpeed = walkSpeed;
            this.objectName = objectName;

            sprites.Foreach(x => textures.Add(x));
        }
        /// <summary>
        /// Creates a new empty instance of a Player
        /// </summary>
        public Player() : base("new Player", new Transform())
        {
            walkSpeed = 5;
        }

        public override void Update(GameTime time)
        {
            //input vector. values stay between -1 and 1
            Vector2 input = Input.GetNormalizedInputVector();
            
            //applying the input vector to the transform
            transform.position = new Vector2(transform.position.X + input.X * walkSpeed, transform.position.Y + input.Y * walkSpeed);
        }
        
        public override void Draw(SpriteBatch batch)
        {
            batch.Begin();
            batch.Draw(MonoUtils.DefaultWhiteTexture, hitbox, Color.Red);
            batch.End();
            base.Draw(batch);
        }
    }
}
