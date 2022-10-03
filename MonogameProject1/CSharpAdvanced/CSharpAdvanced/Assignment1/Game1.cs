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

        Player player;
        Sword sword;
        Dictionary<string, GameObject> gameObjects = new Dictionary<string, GameObject>();
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            MonoUtils.Initialize(graphics.GraphicsDevice, spriteBatch, Content.Load<SpriteFont>("Font"), Content);
            Debug.Initialize();
            Debug.Show();


            string stuff = "Hello World 8.444 times";
            float num = getFloatFromString(stuff);
            Debug.Log(num);
            
            float getFloatFromString(string source)
            {
                string letters = "abcdefghijklmnopqrstuvwxyz ";
                string floatAsString = "";
                foreach (char c in source.ToLower())
                {
                    if (!letters.Contains(c))
                    {
                        floatAsString += c == '.' ? ',' : c;
                    }
                }
                
                float result = float.Parse(floatAsString);
                return result;

            }

            player = new Player("Player1", 4, Content.Load<Texture2D>("Assets/Knight"));
            sword = new Sword("Sword1", Content.Load<Texture2D>("Assets/Weapon"));

            gameObjects.Add(player.objectName, player);
            gameObjects.Add(sword.objectName, sword);
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
            player.Update();
            
            foreach (var obj in gameObjects.Values)
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

            player.Draw(spriteBatch);
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
