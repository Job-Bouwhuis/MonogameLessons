using Microsoft.Xna.Framework;
using WinterRose;
using WinterRose.Monogame;

namespace CSharpAdvanced.Assignment2
{
    internal class Shield : GameObject
    {
        /// <summary>
        /// Creates a new instance of the shield class with the given parameters
        /// </summary>
        public Shield(string objectName, Vector2 position, params Sprite[] sprites) : base(objectName, new Transform())
        {
            sprites.Foreach(x => this.sprites.Add(x));
            transform.position = position;
        }
        public Shield() : base("new Shield", new Transform())
        {
            Enabled = false;
        }
        public override void OnCollisionEnter(GameObject other)
        {
            if (other is Player p)
            {
                if (p.PlayerState == Player.State.Normal)
                    p.PlayerState = Player.State.Shield;
                else if (p.PlayerState == Player.State.Weapon)
                    p.PlayerState = Player.State.WeaponShield;
                
                Enabled = false;
            }
        }
    }
}