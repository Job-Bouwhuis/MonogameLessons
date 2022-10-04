using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SnowLibrary.Monogame;
using SnowLibrary.Monogame.Debugging;
using System.Collections.Generic;
using System.Linq;
using SnowLibrary;

namespace CSharpAdvanced.Assignment1
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        // a collection of all game objects for if 
        Dictionary<string, GameObject> gameObjects = new Dictionary<string, GameObject>();
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // let the utility class initialize with all the values it requires
            MonoUtils.Initialize(graphics.GraphicsDevice, spriteBatch, Content.Load<SpriteFont>("Font"), Content);
            //debug class initializing. this creates the debug menu that can be opened with Debug.Show(); or closed with Debug.Hide();
            Debug.Initialize();

            //create all the game objects and add them to the collection
            gameObjects.Add("player1", 
                new Player("Player1", 
                4,
                Content.Load<Texture2D>("Assets/Knight"),
                Content.Load<Texture2D>("Assets/KnightWeapon"),
                Content.Load<Texture2D>("Assets/KnightShield"),
                Content.Load<Texture2D>("Assets/KnightWeaponShield"))
            );
            
            gameObjects.Add("sword1", new Sword("Sword1", 
                MonoUtils.screenCenter, 
                Content.Load<Texture2D>("Assets/Weapon")));
            
            gameObjects.Add("shield1", new Shield("Shield1", 
                new Vector2(MonoUtils.screenCenter.X + 80, MonoUtils.screenCenter.Y), 
                Content.Load<Texture2D>("Assets/Shield")));
            
            var gateTexTemp = Content.Load<Texture2D>("Assets/Gate");
            gameObjects.Add("gate1", new Gate("Gate1", 
                new Vector2(MonoUtils.screenSize.X - gateTexTemp.Width, 0), 
                gateTexTemp));
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            Input.UpdateState();

            foreach (GameObject obj in gameObjects.Values.Where(x => x.enabled == true))
                obj.Update();

            foreach (var obj in gameObjects.Values.Where(x => x.enabled == true))
            {
                if (obj is IMyInteractable interactable)
                {
                    interactable.CheckColision(gameObjects.Values.ToArray());
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Gray);

            foreach (GameObject obj in gameObjects.Values.Where(x => x.enabled == true))
            {
                obj.Draw(spriteBatch);
            }
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
