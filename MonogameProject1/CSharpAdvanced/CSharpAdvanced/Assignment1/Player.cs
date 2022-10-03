using System;
using System.Collections.Generic;
using System.Text;
using SnowLibrary.Monogame;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using SnowLibrary.Monogame.Debugging;
using SnowLibrary;

namespace CSharpAdvanced.Assignment1
{
    public class Player : GameObject
    {
        private int walkSpeed;
        public Player(string objectName, int walkSpeed, params Sprite[] sprites) : base("new Object", new Transform(), sprites)
        {
            this.walkSpeed = walkSpeed;
            this.objectName = objectName;

            sprites.Foreach(x => textures.Add(x));
        }


        public override void Update()
        {
            //input vector. values stay between -1 and 1
            Vector2 input = new Vector2(0, 0);
            
            //getting the input itself
            if (Input.GetKey(Keys.W))
                input.Y--;
            if (Input.GetKey(Keys.A))
                input.X--;
            if (Input.GetKey(Keys.D))
                input.X++;
            if (Input.GetKey(Keys.S))
                input.Y++;

            //normalizing the input vector
            if (input != Vector2.Zero)
                input.Normalize();

            //applying the input vector to the transform
            transform.position = new Vector2(transform.position.X + input.X * walkSpeed, transform.position.Y + input.Y * walkSpeed);
        }
    }
}
