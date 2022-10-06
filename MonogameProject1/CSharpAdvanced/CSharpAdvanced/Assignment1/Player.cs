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
<<<<<<< HEAD
=======

        public override void Update(GameTime time)
        {
            //input vector. values stay between -1 and 1
            Vector2 input = Input.GetNormalizedInputVector();




            //applying the input vector to the transform
            transform.position += input * walkSpeed;
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
>>>>>>> parent of 87b38c3 (WIP on Assignment1: c16e993 library updates)
    }
}
