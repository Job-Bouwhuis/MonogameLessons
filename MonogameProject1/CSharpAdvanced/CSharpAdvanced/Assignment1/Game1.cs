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

        SceneManager scenes = new SceneManager();
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            scenes += new Scene("DefaultScene");
        }

        protected override void Initialize()
        {
            // let the utility class initialize with all the values it requires
            MonoUtils.Initialize(graphics.GraphicsDevice, spriteBatch, Content.Load<SpriteFont>("Font"), Content);
            //debug class initializing. this creates the debug menu that can be opened with Debug.Show(); or closed with Debug.Hide();
            Debug.Initialize();
            Debug.Show();

            //create all the game objects and add them to the collection
            scenes.ActiveScene += new Player("Player1", 
                4,
                Content.Load<Texture2D>("Assets/Knight"),
                Content.Load<Texture2D>("Assets/KnightWeapon"),
                Content.Load<Texture2D>("Assets/KnightShield"),
                Content.Load<Texture2D>("Assets/KnightWeaponShield"));
            
            scenes.ActiveScene += new Sword("Sword1", 
                MonoUtils.ScreenCenter, 
                Content.Load<Texture2D>("Assets/Weapon"));
            
            scenes.ActiveScene += new Shield("Shield1", 
                new Vector2(MonoUtils.ScreenCenter.X + 80, MonoUtils.ScreenCenter.Y), 
                Content.Load<Texture2D>("Assets/Shield"));
            
            var gateTexTemp = Content.Load<Texture2D>("Assets/Gate");
            scenes.ActiveScene += new Gate("Gate1", 
                new Vector2(MonoUtils.ScreenSize.X - gateTexTemp.Width, 0), 
                gateTexTemp);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime time)
        {
            //refresh the input state each update loop
            Input.UpdateState();

            //update each game object that is enabled
            scenes.ActiveScene.Update(time);

            base.Update(time);
        }

        protected override void Draw(GameTime gameTime)
        {
            //clear the screen and draw the screen with the color Gray
            GraphicsDevice.Clear(Color.Gray);

            //foreach object that is enabled draw it to the screen using spriteBatch
            scenes.ActiveScene.Draw(spriteBatch);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
