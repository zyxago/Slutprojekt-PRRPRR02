using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;

namespace Slutprojekt
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        public static GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont font;
        KeyboardState KeyState, prevKeyState;
        MouseState mouseState, prevMouseState;
        Rectangle mouseBox;
        List<Map> mapList = new List<Map>();
        List<Tower> towerList = new List<Tower>();
        List<Enemy> enemyList = new List<Enemy>();
        public static Texture2D ErrorTex;
        string[] towersToLoad;
        string[] enemiesToLoad;
        string[] mapsToLoad;
        enum GameState
        {
            Menu,
            InGame
        }
        GameState State;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 640;
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            IsMouseVisible = true;
            State = GameState.Menu;
            Menu.location = Menu.Location.MainMenu;
            towersToLoad = Directory.GetFiles($"{Environment.CurrentDirectory}\\Towers", "*.xml");
            enemiesToLoad = Directory.GetFiles($"{Environment.CurrentDirectory}\\Enemies", "*.xml");
            mapsToLoad = Directory.GetFiles($"{Environment.CurrentDirectory}\\Maps", "*.xml");
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            font = Content.Load<SpriteFont>("font");
            ErrorTex = Content.Load<Texture2D>("ErrorTexture");
            Menu.Load();
            mapList = LoadData.LoadMaps(mapsToLoad);
            towerList = LoadData.Load<Tower>(towersToLoad);
            enemyList = LoadData.Load<Enemy>(enemiesToLoad);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            KeyState = Keyboard.GetState();
            mouseState = Mouse.GetState();
            mouseBox = new Rectangle(Mouse.GetState().Position, new Point(10));
            if(State == GameState.Menu)
            {
                if (Menu.location == Menu.Location.MainMenu)
                {
                    if(mouseState.LeftButton != prevMouseState.LeftButton && mouseBox.Intersects(Menu.MenuBoxes["start"]))
                    {
                        Menu.location = Menu.Location.MapSelector;
                    }
                }
                else if (Menu.location == Menu.Location.Options)
                {

                }
                else if (Menu.location == Menu.Location.MapSelector)
                {

                }
                else if (Menu.location == Menu.Location.TowerSelector)
                {

                }
            }
            else if(State == GameState.InGame)
            {

            }
            // TODO: Add your update logic here
            prevKeyState = Keyboard.GetState();
            prevMouseState = Mouse.GetState();
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            
            if (State == GameState.Menu)
            {
                Menu.Draw(spriteBatch);
            }
            else if (State == GameState.InGame)
            {

            }
            spriteBatch.DrawString(font, $"{mouseState.Position}", new Vector2(50, 50), Color.Black);
            spriteBatch.End();
            // TODO: Add your drawing code here
            base.Draw(gameTime);
        }
    }
}
