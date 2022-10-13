using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SnowLibrary;
using SnowLibrary.Monogame;
using SnowLibrary.Monogame.SceneManagement;
using System;
using System.Linq;

namespace CSharpAdvanced.Assignment2
{
    public class Gate : GameObject
    {
        /// <summary>
        /// Creates a new instance of the shield class with the given parameters
        /// </summary>
        public Gate(string objectName, Vector2 position, params Sprite[] sprites) 
            : base(objectName, new Transform(), sprites)
        {
            transform.position = position;
        }
        /// <summary>
        /// Creates a new empty instance of the Gate class. this sets the game object to disabled by default
        /// </summary>
        public Gate() 
            : base("new Gate", new Transform())
        {
            Enabled = false;
        }

        public override void OnCollisionEnter(GameObject other)
        {
            if (SceneManager.CurrentScene.sceneName == "Level1")
            {
                SceneManager.LoadScene("Level2");

                Scene scene = SceneManager.CurrentScene;
                
                Player player = (Player)scene.FindObjectWithName("Player1");

                string? prefsValue = PlayerPrefs.GetValue("PlayerState");

                player.PlayerState = (Player.State)TypeWorker.CastPrimitive<int>(prefsValue ?? "0");
            }
            else
            {
                // load level 1
                SceneManager.LoadScene("Level1");

                // assign it in a local variable for easy access
                Scene scene = SceneManager.CurrentScene;

                // fetch the player
                Player player = (Player)scene.FindObjectWithName("Player1");

                // set the player state from the previous scene so the weapons and shield remain, if the player had equiped them
                player.PlayerState = (Player.State)TypeWorker.CastPrimitive<int>(PlayerPrefs.GetValue("PlayerState"));

                // find the position of the gate in level 1
                Vector2 gatePos = scene.FindObjectWithName("Gate1").transform.position;

                // set the player's position near the gate so it looks like the player exited out of that gate
                player.transform.position = new Vector2(gatePos.X - player.texture.width * 2.5f, 0);


                switch (player.PlayerState)
                {
                    case Player.State.Weapon:
                        Destroy(scene.FindObjectWithName("Sword1"));
                        break;
                    case Player.State.Shield:
                        Destroy(scene.FindObjectWithName("Shield1"));
                        break;
                    case Player.State.WeaponShield:
                        Destroy(scene.FindObjectWithName("Sword1"));
                        Destroy(scene.FindObjectWithName("Shield1"));
                        break;
                }
            }
        }
    }
}
