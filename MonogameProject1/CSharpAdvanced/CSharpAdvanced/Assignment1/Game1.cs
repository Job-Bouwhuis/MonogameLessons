using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;

using SnowLibrary;
using SnowLibrary.Monogame.SceneManagement;
using SnowLibrary.Monogame.UI;
using SnowLibrary.Monogame;
using SnowLibrary.Monogame.Debugging;

namespace CSharpAdvanced.Assignment1
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        SceneManager levelManager = new SceneManager();
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            levelManager += new Scene("Level1");
        }

        protected override void Initialize()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            // let the utility class initialize with all the values it requires
            MonoUtils.Initialize(graphics.GraphicsDevice, spriteBatch, Content.Load<SpriteFont>("Font"), Content);
            
            //debug class initializing. this creates the debug menu that can be opened with Debug.Show(); or closed with Debug.Hide();
            Debug.Initialize();
            //Debug.Show();

            // load the first scene
            levelManager.LoadScene("Level1");

            base.Initialize();
        }

        protected override void LoadContent()
        {
            
        }

        protected override void Update(GameTime time)
        {
            //refresh the input state each update loop
            Input.UpdateState();

            //update each game object that is enabled
            levelManager.ActiveScene.Update(time);

            base.Update(time);
        }

        protected override void Draw(GameTime gameTime)
        {
            //clear the screen and draw the screen with the color Gray
            GraphicsDevice.Clear(Color.Gray);

            //foreach object that is enabled draw it to the screen using spriteBatch
            levelManager.ActiveScene.Draw(spriteBatch);

            base.Draw(gameTime);
        }
    }
}
