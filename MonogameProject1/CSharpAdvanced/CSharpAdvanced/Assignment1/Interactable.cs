using Microsoft.Xna.Framework;
using SnowLibrary.Monogame;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpAdvanced.Assignment1
{
    public interface IMyInteractable
    {
        /// <summary>
        ///  
        /// </summary>
        /// <param name="others">an array of all game objects that can be collided with</param>
        public void CheckColision(params GameObject[] others);
        /// <summary>
        /// Called when colision check is return true. <paramref name="other"/> is the object that was collided with
        /// </summary>
        /// <param name="other"></param>
        public void OnColisionEnter(GameObject other);
    }
}
