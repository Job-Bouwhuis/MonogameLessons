﻿using Microsoft.Xna.Framework;
using SnowLibrary;
using SnowLibrary.Monogame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharpAdvanced.Assignment1
{
    public class Sword : GameObject, IMyInteractable
    {
        public Sword(string objectName) : base("new Sword", new Transform())
        {

        }

        public void CheckColision(params GameObject[] others)
        {
            others.Where(x => Collision.IsCollidingOnSides(hitbox, x.hitbox)).Foreach(x => OnColisionEnter(x));
        }
        
        public void OnColisionEnter(GameObject other)
        {
            
        }
    }
}
