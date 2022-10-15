using Microsoft.Xna.Framework;
using WinterRose;
using WinterRose.Monogame;


namespace CSharpAdvanced.Assignment2
{
    public class Sword : GameObject
    {
        /// <summary>
        /// Creates a new instance of the Sword class with the given paramteres
        /// </summary>
        public Sword(string objectName, Vector2 position, params Sprite[] sprites) : base(objectName, new Transform())
        {
            sprites.Foreach(x => this.sprites.Add(x));
            transform.position = position;
        }
        /// <summary>
        /// Creates a new empty instance of the sword class
        /// </summary>
        public Sword() : base("new Sword", new Transform())
        {
            Enabled = false;
        }

        public override void OnCollisionEnter(GameObject other)
        {
            if (other is Player p)
            {
                if (p.PlayerState == Player.State.Normal)
                    p.PlayerState = Player.State.Weapon;
                else if (p.PlayerState == Player.State.Shield)
                    p.PlayerState = Player.State.WeaponShield;

                Enabled = false;
            }
        }
    }
}
