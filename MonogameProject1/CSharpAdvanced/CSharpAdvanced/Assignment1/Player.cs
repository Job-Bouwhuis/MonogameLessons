using WinterRose.Monogame;
using WinterRose;

namespace CSharpAdvanced.Assignment1
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

                textureIndex = value switch
                {
                    State.Normal => 0,
                    State.Weapon => 1,
                    State.Shield => 2,
                    State.WeaponShield => 3,
                    _ => 0
                };
            }
        }

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

        public enum State
        {
            Normal,
            Weapon,
            Shield,
            WeaponShield
        }
    }
}

