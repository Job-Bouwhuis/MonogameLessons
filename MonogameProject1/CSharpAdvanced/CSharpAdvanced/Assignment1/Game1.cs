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
        List<GameObject> gameObjects = new List<GameObject>();
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
            
            player = new Player(4);
            gameObjects.Add(player);
            gameObjects.Add(new Sword("Sword"));
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            player.textures.Add(Content.Load<Texture2D>("Assets/Knight"));
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            Input.UpdateState();
            player.Update();
            
            foreach (var obj in gameObjects)
            {
                if (obj is IMyInteractable interactable)
                {
                    interactable.CheckColision(gameObjects.ToArray());
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
