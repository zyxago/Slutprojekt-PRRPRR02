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
        public static SpriteFont font;
        KeyboardState KeyState, prevKeyState;
        public static MouseState mouseState, prevMouseState;
        List<Map> mapList = new List<Map>();
        List<Tower> towerList = new List<Tower>();
        List<Enemy> enemyList = new List<Enemy>();
        List<Tower> towers = new List<Tower>();
        List<Enemy> enemies = new List<Enemy>();
        public static Texture2D ErrorTex;
        Texture2D hotbarTex;
        string[] towersToLoad;
        string[] enemiesToLoad;
        string[] mapsToLoad;
        public static int MapPlaying { get; private set; }
        bool waveOngoing = false;
        bool spawning = false;

        public enum GameState
        {
            Menu,
            InGame,
            Quit
        }

        public static GameState State;
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
            Spell.LoadSpells();
            try
            {
                towersToLoad = Directory.GetFiles($"{Environment.CurrentDirectory}\\Towers", "*.xml");
                enemiesToLoad = Directory.GetFiles($"{Environment.CurrentDirectory}\\Enemies", "*.xml");
                mapsToLoad = Directory.GetFiles($"{Environment.CurrentDirectory}\\Maps", "*.xml");
            }
            catch(DirectoryNotFoundException e)
            {
                throw e;
            }
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
            hotbarTex = Content.Load<Texture2D>("TowerBar");
            Menu.Load();
            mapList = LoadData.Load(mapsToLoad);
            towerList = LoadData.Load<Tower>(towersToLoad);
            enemyList = LoadData.Load<Enemy>(enemiesToLoad);
            Hud.hotbarTex = hotbarTex;
            Hud.towerList = towerList;
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
            if (State == GameState.Menu)
            {
                Menu.Update();
            }
            else if (State == GameState.InGame)
            {
                if(mouseState.LeftButton != prevMouseState.LeftButton && Hud.NextWave.Contains(mouseState.Position) && waveOngoing == false)
                {
                    waveOngoing = true;
                    spawning = true;
                    EntitySpawner.NextWave(enemyList);
                }
                if (waveOngoing)
                {
                    if (spawning == true)
                    {
                        if (!EntitySpawner.EnemiesToSpawn.IsEmpty())
                            EntitySpawner.SpawnNextEnemy(enemyList);
                        else
                            spawning = false;
                    }
                    if (enemyList.Count == 0 && spawning == false)
                    {
                        waveOngoing = false;
                    }
                }
            }
            else if (State == GameState.Quit)
            {
                Exit();
            }
            prevKeyState = Keyboard.GetState();
            prevMouseState = Mouse.GetState();
            base.Update(gameTime);
        }

        public static void StartGame(int mapIndex)
        {
            MapPlaying = mapIndex-1;
            State = GameState.InGame;
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
                Menu.Draw(spriteBatch, mapList);
            }
            else if (State == GameState.InGame)
            {
                mapList[MapPlaying].Draw(spriteBatch);
                Hud.Draw(spriteBatch);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
