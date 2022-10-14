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
using System.ComponentModel.DataAnnotations;
using CSharpAdvanced.Assignment1;
using SnowLibrary.Monogame.SceneManagement;

namespace CSharpAdvanced.Assignment2
{
    public class Player : GameObject
    {
        State playerState;

        public State PlayerState
        {
            get => playerState;
            set
            {
                playerState = value;

                textureIndex = (int)value;

                PlayerPrefs.SetValue("PlayerState", textureIndex.ToString());
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

        public override void Awake()
        {
            RetainPlayersState();
            EliminateEquipedItems();
        }
        
        private void RetainPlayersState()
        {
            // set the player state from the previous scene so the weapons and shield remain, if the player had equiped them
            PlayerState = (Player.State)TypeWorker.CastPrimitive<int>(PlayerPrefs.GetValue("PlayerState"));
        }
        
        private void EliminateEquipedItems()
        {
            switch (PlayerState)
            {
                case Player.State.Weapon:
                    Destroy(FindGameObjectWithName("Sword1"));
                    break;
                case Player.State.Shield:
                    Destroy(FindGameObjectWithName("Shield1"));
                    break;
                case Player.State.WeaponShield:
                    Destroy(FindGameObjectWithName("Sword1"));
                    Destroy(FindGameObjectWithName("Shield1"));
                    break;
            }
        }
    }



}

