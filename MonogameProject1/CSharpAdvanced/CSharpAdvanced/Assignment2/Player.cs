﻿using System;
using System.Collections.Generic;
using System.Text;
using SnowLibrary.Monogame;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using SnowLibrary.Monogame.Debugging;
using SnowLibrary;
using SnowLibrary.Serialization;
using System.ComponentModel.DataAnnotations;

namespace CSharpAdvanced.Assignment2
{
    public class Player : GameObject
    {
        State playerState = State.Normal;

        public State PlayerState
        {
            get => playerState;
            set
            {
                playerState = value;

                textureIndex = (int)value;
            }
        }

        /// <summary>
        /// Creates a new instance of the player class with the given parameters
        /// </summary>
        /// <param name="objectName">the name this object will be saved as</param>
        /// <param name="sprites">the sprites that are available for this game object</param>
        public Player(string objectName, params Sprite[] sprites) : base("new Object", new Transform(), sprites)
        {
            this.objectName = objectName;
            sprites.Foreach(x => this.sprites.Add(x));
        }
        
        /// <summary>
        /// Creates a new empty instance of a Player
        /// </summary>
        public Player() : base("new Player", new Transform())
        {
            Enabled = false;
        }

        /// <summary>
        /// Provides states for the player, wether he is holding a weapon, a shield, both, or none
        /// </summary>
        public enum State
        {
            /// <summary>
            /// the player is holding nothing
            /// </summary>
            Normal,
            /// <summary>
            /// the player is holding a weapon
            /// </summary>
            Weapon,
            /// <summary>
            /// the player is holding a shield
            /// </summary>
            Shield,
            /// <summary>
            /// the player is holding a weapon and a shield
            /// </summary>
            WeaponShield
        }
    }
}

