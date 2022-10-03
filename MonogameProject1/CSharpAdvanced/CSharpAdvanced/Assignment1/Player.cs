using System;
using System.Collections.Generic;
using System.Text;
using SnowLibrary.Monogame;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace CSharpAdvanced.Assignment1
{
    internal class Player : GameObject
    {
        private int walkSpeed;
        public Player(int walkSpeed) : base("new Object", new Transform())
        {
            this.walkSpeed = walkSpeed;   
        }

        public override void Update()
        {
            Transform lastPos = transform;
            if (Input.GetKeyDown(Keys.W))
                transform.position.Y -= walkSpeed;
            if (Input.GetKeyDown(Keys.A))
                transform.position.X -= walkSpeed;
            if (Input.GetKeyDown(Keys.D))
                transform.position.X += walkSpeed;
            if (Input.GetKeyDown(Keys.S))
                transform.position.Y += walkSpeed;

           
            if (!MonoUtils.AreYouOnScreen(transform.position, texture))
            {
                transform = lastPos;
            }
        }
    }
}
