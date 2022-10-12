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
using SnowLibrary.Monogame.UI.Editor;
using System;

namespace CSharpAdvanced.Assignment2
{
    public class ButtonClicks
    {
        public void ExitGame(object? o, EventArgs e)
        {
            Environment.Exit(-1);
        }
        public void LoadLevel1(object? o, EventArgs e)
        {
            SceneManager.LoadScene("Level1");
        }
    }

    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private Scene currentScene;

        private UIEditor editor;

        public static bool paused = false;

        public void ExitGame(object? o, EventArgs e)
        {
            Exit();
        }

        public void LoadLevel(object? o, EventArgs e)
        {
            SceneManager.LoadScene("Level1");
        }




        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            // let the utility class initialize with all the values it requires
            MonoUtils.Initialize(graphics.GraphicsDevice, spriteBatch, Content.Load<SpriteFont>("Font"), Content);

            //debug class initializing. this creates the debug menu that can be opened with Debug.Show(); or closed with Debug.Hide();
            Debug.Initialize();
            //Debug.Show();

            SceneManager.OnNewSceneLoaded += OnSceneLoaded;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Redundant code from creating the scene itself. keeping this here for the lovely teacher <3
            {
                //Scene scene = SceneManager.CurrentScene = new Scene("Level1");

                //// create player object
                //Player player = new Player("Player1",
                //    Content.Load<Texture2D>("Assets/Knight"),
                //    Content.Load<Texture2D>("Assets/KnightWeapon"),
                //    Content.Load<Texture2D>("Assets/KnightShield"),
                //    Content.Load<Texture2D>("Assets/KnightWeaponShield"));

                //// attatch all the components.
                //var controller = player.AttatchComponent<TopDownPlayerController>();
                //controller.walkSpeed = 5;
                //controller.smoothness = 80;
                //player.AttatchComponent<BoxCollider>();
                //player.AttatchComponent<SpriteRenderer>();
                //player.AttatchComponent<ScreenClamper>();
                //// add player to the scene.
                //scene += player;


                //// create sword object
                //Sword sword = new Sword("Sword1",
                //    MonoUtils.ScreenCenter,
                //    Content.Load<Texture2D>("Assets/Weapon"));

                //// attatch all the components
                //sword.AttatchComponent<SpriteRenderer>();
                //sword.AttatchComponent<BoxCollider>().isTrigger = true;

                //// add the sword to the scene
                //scene += sword;

                //// Create shield object
                //Shield shield = new Shield("Shield1",
                //    new Vector2(MonoUtils.ScreenCenter.X + 80, MonoUtils.ScreenCenter.Y),
                //    Content.Load<Texture2D>("Assets/Shield"));

                //// attatch all the components
                //shield.AttatchComponent<BoxCollider>().isTrigger = true;
                //shield.AttatchComponent<SpriteRenderer>();

                //// add the shield to the scene
                //scene += shield;


                ////create the gate
                //var gateTexTemp = Content.Load<Texture2D>("Assets/Gate");
                //Gate gate = new Gate("Gate1",
                //    new Vector2(MonoUtils.ScreenSize.X - gateTexTemp.Width, 0),
                //    gateTexTemp);

                //// add its components
                //gate.AttatchComponent<BoxCollider>().isTrigger = true;
                //gate.AttatchComponent<SpriteRenderer>();

                ////add the gate to the scene
                //scene += gate;

                //// save the scene
                //// after the scene is saved, all the code above to create the scene can be deleted from the project. the entire scene is then stored in a text file and can be loaded with the SceneManager.LoadScene() method.
                //scene.Save();
            }
            
            // redundant UI stuff. this bool is set to true when a new UI is being created. this is to prevent the game from running normally while the UI is being created.
            if (false)
            {
                editor = new UIEditor(new UserInterface(), GraphicsDevice);

                editor.EditUI();
            }
            else
            {
                currentScene = new Scene("MainMenu2");
                // load the first scene
                //SceneManager.LoadScene("MainMenu");
                currentScene.sceneUI = UIEditor.Load("MainMenu");

                currentScene.Save();

                SceneManager.LoadScene("MainMenu");
            }
        }

        protected override void Update(GameTime time)
        {
            if (!IsActive)
                paused = true;
                
            if (editor != null && editor.isEditing)
            {
                editor.Update(time);
                editor.Draw();
                return;
            }

            //refresh the input state each update loop
            Input.UpdateState();

            //update each game object that is enabled
            currentScene.Update(time);
            currentScene.sceneUI.UpdateUI(time);

            base.Update(time);
        }

        protected override void Draw(GameTime gameTime)
        {
            if (editor != null && editor.isEditing)
                return;

            //clear the screen and draw the screen with the color Gray
            GraphicsDevice.Clear(Color.Gray);

            //foreach object that is enabled draw it to the screen using the default spriteBatch
            currentScene.Draw(spriteBatch);
            currentScene.sceneUI.Draw(spriteBatch);

            base.Draw(gameTime);
        }

        public void OnSceneLoaded()
        {
            // set the current scene to the scene that was loaded
            currentScene = SceneManager.CurrentScene;
            //load the UI for the scene
            //currentScene.sceneUI = UIEditor.Load("MainMenu");

            //set the window title screen to the name of the scene
            Window.Title = currentScene.sceneName;
        }
    }
}
