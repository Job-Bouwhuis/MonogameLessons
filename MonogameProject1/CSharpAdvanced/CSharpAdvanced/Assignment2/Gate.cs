using CSharpAdvanced.Assignment1;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WinterRose;
using WinterRose.Monogame;
using WinterRose.Monogame.SceneManagement;
using WinterRose.Serialization;
using System;
using System.Linq;

namespace CSharpAdvanced.Assignment2
{
    [IncludePrivateFields]
    public class Gate : GameObject
    {
        public string sceneNameToLoad;

        /// <summary>
        /// Creates a new instance of the shield class with the given parameters
        /// </summary>
        public Gate(string objectName, Vector2 position, params Sprite[] sprites) 
            : base(objectName, new Transform(), sprites)
        {
            transform.position = position;
        }
        /// <summary>
        /// Creates a new instance of the shield class with the given parameters
        /// </summary>
        public Gate(string objectName, Vector2 position, string sceneNameToLoad, params Sprite[] sprites)
            : base(objectName, new Transform(), sprites)
        {
            transform.position = position;
            this.sceneNameToLoad = sceneNameToLoad;
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
            //een lijst aan scenes die al eens geladen zijn, als ze geladen zijn dan pak je ze uit de lijst, anders laad ze vanuit de file

            SceneManager.LoadScene(sceneNameToLoad);

            // heb deze code hier gelaten zodat de player zijn positie alleen word gezet naar de gate wanneer de player terug naar level 1 gaat
            if (SceneManager.CurrentScene.sceneName == "Level1")
            {
                Vector2 gatePos = SceneManager.CurrentScene.FindGameObjectWithName("Gate1").transform.position;

                // set the player's position near the gate so it looks like the player exited out of that gate
                Player player = (Player)SceneManager.CurrentScene.FindGameObjectWithName("Player1");
                player.transform.position = new Vector2(gatePos.X - player.texture.width * 1.5f, 0);
            }
        }
    }
}
